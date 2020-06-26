using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace nullDCNetplayLauncher
{
    public class Launcher
    {
        public static string rootDir = GetDistributionRootDirectoryName() + "\\";
        public static string SelectedGame;

        public static Dictionary<string, int> MethodOptions = new Dictionary<string, int>();

        public static GamePadMappingList mappings;
        public static GamePadMapping ActiveGamePadMapping;

        public static bool FilesRestored = false;

        public static NetworkQuery NetQuery;

        public static List<Game> GamesJson;

        public Launcher()
        {
            MethodOptions["Frame Limit"] = 0;
            MethodOptions["Audio Sync"] = 1;

            mappings = GamePadMapping.ReadMappingsFile(); ;
            AssignActiveMapping();

            NetQuery = new NetworkQuery();

            GamesJson = null;

            try
            {
                string GameJsonPath = Launcher.rootDir + "games.json";
                var GamesJsonTxt = File.ReadAllText(GameJsonPath);
                GamesJson = JsonConvert.DeserializeObject<List<Launcher.Game>>(GamesJsonTxt);
            }
            catch (Exception) { };
            }

        // the qkoJAMMA plugin sometimes generates blank QJC files and rewrites malformed file
        // upon exit for joysticks that are detected, but unused. this causes issues at startup
        public static void CleanMalformedQjcFiles()
        {
            var qjcFiles = Directory.GetFiles(Path.Combine(Launcher.rootDir, @"nulldc-1-0-4-en-win\qkoJAMMA"), "*.qjc");
            foreach (string qjcFile in qjcFiles)
            {
                var qjcLines = File.ReadAllLines(qjcFile).Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();

                var qjcEntries = new Dictionary<string, string>();
                foreach (string qjcLine in qjcLines)
                {
                    var lineSplit = qjcLine.Split('=');
                    qjcEntries[lineSplit[0]] = lineSplit[1];
                }

                var numLinesInitial = qjcEntries.Count;
                qjcEntries.Remove("");

                var numLinesAfter = qjcEntries.Count;

                bool modifiedQjc = false;
                if (numLinesInitial != numLinesAfter)
                    modifiedQjc = true;

                if (qjcEntries.Keys.Contains("none") && qjcEntries.Keys.Count == 1)
                {
                    File.Delete(qjcFile);
                    continue;
                }

                if (qjcEntries.Count < 12)
                {
                    File.Delete(qjcFile);
                    continue;
                }
                else
                {
                    if (modifiedQjc)
                    {
                        File.WriteAllLines(qjcFile,
                            qjcEntries.Select(x => $"{x.Key}={x.Value}"));
                    }
                }
            }
        }

        public void AssignActiveMapping()
        {
            try
            {
                ActiveGamePadMapping = mappings.GamePadMappings.Where(g => g.Default == true).ToList().First();
                System.Diagnostics.Debug.WriteLine("assigned " + ActiveGamePadMapping);
            }
            catch
            {
                ActiveGamePadMapping = mappings.GamePadMappings.First();
            }
        }

        public static int GuessDelay(string IP)
        {
            int delay = -1;
            long avgResponseTime;

            List<long> responseTimes = new List<long>();
            for (int i = 0; i < 10; i++) {
                try
                {
                    Ping ping = new Ping();
                    PingReply pingReply = ping.Send(IP, 1000);
                    if (pingReply.Status == 0)
                    {
                        long responseTime = pingReply.RoundtripTime;
                        responseTimes.Add(responseTime);
                        Console.WriteLine(responseTime);
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }

            if (responseTimes.Count > 0)
            {
                avgResponseTime = (long)responseTimes.Average();

                var calcDelay = Math.Ceiling((double)avgResponseTime / 32.66);
                delay = Convert.ToInt32(calcDelay);
            }
            return delay;
        }

        public static String GetRomPathFromGameId(string gameid)
        {
            string DistroDir = "nulldc-1-0-4-en-win\\";
            var path = "";

            try
            {
                string GameJsonPath = Launcher.rootDir + "games.json";
                var GamesJsonTxt = File.ReadAllText(GameJsonPath);

                var GamesJson = JsonConvert.DeserializeObject<List<Launcher.Game>>(GamesJsonTxt);
                var games = GamesJson.Where(g => g.ID == $"nulldc_{gameid}");

                if (games.Count() > 0)
                {
                    var lst = games.First().Assets.Where(a => a.LocalName().EndsWith(".lst")).First();
                    path = Path.Combine(DistroDir, games.First().Root, games.First().Name, lst.LocalName());
                }
            }
            catch (Exception) { };

            return path;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

        [DllImport("user32.dll")]
        static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, string lParam);
        public const uint WM_SETTEXT = 0x000C;

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        private static int WM_COMMAND = 0x111;
        private static int NORMALBOOT_ID = 0x17;
        private static int WINDOW_POPUP = 6; //get the official name for this one
        private static int PATHNAME_ENTRY_ID = 0x47C;

        public static void LaunchNullDC(string RomPath, bool isHost = false)
        {
            //Credit to Labryz (GitHub: labreezy) for initial work
            //replacing the AHK rom launcher in favor of user32 P/Invoke Methods
            Process[] ndc = Process.GetProcessesByName("nullDC_Win32_Release-NoTrace");

            if (ndc.Length > 0)
            {
                foreach (Process nProc in ndc)
                {
                    nProc.Kill();
                    nProc.WaitForExit();
                    nProc.Dispose();
                }
            }
            string ndcPath = "\"" + Path.Combine(Launcher.rootDir, @"nulldc-1-0-4-en-win\nullDC_Win32_Release-NoTrace.exe") + "\"";
            string FullRomPath = "\"" + Path.Combine(Launcher.rootDir, RomPath) + "\"";
            ProcessStartInfo ndcStartInfo = new ProcessStartInfo(ndcPath);
            List<string> arglist = new List<string>();
            arglist.Add(ndcPath);
            ndcStartInfo.Arguments = string.Join(" ", arglist);
            _ = Process.Start(ndcStartInfo);
            bool ndcStart = false;
            for (var i = 0; i < 5; i++)
            {
                System.Threading.Thread.Sleep(1000); //wait a bit, 2s total, 5 checks
                ndc = Process.GetProcessesByName("nullDC_Win32_Release-NoTrace");
                if (ndc.Length > 0)
                {
                    ndcStart = true;
                    break;
                }
            }

            if (!ndcStart)
            {
                throw new Exception("nullDC failed to start from rom launcher."); //It might happen...
            }
            Process p = ndc[0];
            IntPtr hWndPtr = p.MainWindowHandle;
            
            var windowSettings = LoadWindowSettings();
            System.Threading.Thread.Sleep(1000);
            if (windowSettings[0] == 1)
            {
                MoveWindow(hWndPtr,
                       NullDCWindowPosition().X,
                       NullDCWindowPosition().Y,
                       windowSettings[1],
                       windowSettings[2],
                       true);
            }
            else if (windowSettings[3] == 1)
            {
                ShowWindow(hWndPtr, 3);
            }

            PostMessage(hWndPtr, WM_COMMAND, NORMALBOOT_ID, 0); //Equivalent to File -> Normal Boot
            System.Threading.Thread.Sleep(2000); //safety measure
            IntPtr hPopup = GetWindow(hWndPtr, WINDOW_POPUP);
            IntPtr hEdit = GetDlgItem(hPopup, PATHNAME_ENTRY_ID);
            HandleRef hrefHWndTarget = new HandleRef(null, hEdit);
            System.Threading.Thread.Sleep(500);
            SendMessage(hrefHWndTarget, WM_SETTEXT, IntPtr.Zero, FullRomPath);

            SetFocus(hEdit);
            const byte VK_RETURN = 0x0D;
            const uint KEYEVENTF_KEYUP = 0x0002;
            keybd_event(VK_RETURN, 0, 0, 0);
            keybd_event(VK_RETURN, 0, KEYEVENTF_KEYUP, 0);
        }

        // written by MarioBrotha
        // this fork mostly revolves around abusing this method
        public static void UpdateCFGFile(bool netplayEnabled = true,
            bool isHost = false,
            string hostAddress = "127.0.0.1",
            string hostPort = "27886",
            string frameDelay = "1",
            string frameMethod = "0")
        {
            string EmuDir = rootDir + "nulldc-1-0-4-en-win\\";
            string LauncherCfgPath = rootDir + "launcher.cfg";
            string CfgPath = EmuDir + "nullDC.cfg";
            string AntilagPath = EmuDir + "antilag.cfg";

            string enabled = "Enabled=" + (netplayEnabled ? 1 : 0).ToString();
            string hosting = "Hosting=" + (isHost ? 1 : 0).ToString();
            string hostip = "Host=" + hostAddress;
            string portcfg = "Port=" + hostPort;
            string delaycfg = "Delay=" + frameDelay;

            //for nullDC cfg
            string limitfpscfg = "LimitFPS=0";
            if (isHost)
            {
                //audio sync
                if (frameMethod == "1")
                {
                    limitfpscfg = "LimitFPS=2";
                }
            }

            string[] launchercfglines = File.ReadAllLines(LauncherCfgPath);
            string[] fpslines = File.ReadAllLines(AntilagPath);
            string[] lines = File.ReadAllLines(CfgPath);

            var host_fps_line = launchercfglines.Where(s => s.Contains("host_fps=")).ToList().First();
            var guest_fps_line = launchercfglines.Where(s => s.Contains("guest_fps=")).ToList().First();

            var hostFpsEntry = host_fps_line.Split('=')[1];
            var guestFpsEntry = guest_fps_line.Split('=')[1];

            string fpslimitcfg = "FPSlimit=" + (isHost || !netplayEnabled ? hostFpsEntry : guestFpsEntry).ToString();
            Console.WriteLine(fpslimitcfg);

            // write to frame limiter
            using (StreamWriter writer = new StreamWriter(AntilagPath))
            {
                for (int i = 0; i < fpslines.Length; i++)
                {
                    if (fpslines[i].StartsWith("FPSlimit"))
                    {
                        writer.WriteLine(fpslimitcfg);
                    }
                    else
                    {
                        writer.WriteLine(fpslines[i]);
                    }
                }
            }

            // write to nullDC.cfg
            using (StreamWriter writer = new StreamWriter(CfgPath))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    //section fps limiter
                    if (lines[i].Contains("LimitFPS"))
                    {
                        writer.WriteLine(limitfpscfg);
                        i = i + 1;
                    }
                    //section netplay                   
                    if (lines[i].Contains("[Netplay]"))
                    {
                        writer.WriteLine("[Netplay]");
                        //rewriting all the lines in this section
                        writer.WriteLine(enabled);
                        writer.WriteLine(hosting);
                        writer.WriteLine(hostip);
                        writer.WriteLine(portcfg);
                        writer.WriteLine(delaycfg);
                        i = i + 5;
                    }
                    else
                    {
                        writer.WriteLine(lines[i]);
                    }
                }
            }
        }

        public static string GetDistributionRootDirectoryName()
        {
            var LauncherPath = new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
            var DistroPath = new DirectoryInfo(LauncherPath).Parent.FullName;
            return DistroPath;
        }

        public static string GetApplicationConfigurationDirectoryName()
        {
            return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        }

        public static string ExtractRomNameFromPath(string path)
        {
            List<string> splitPath = path.Split(Path.DirectorySeparatorChar).ToList();
            return splitPath.ElementAt(splitPath.Count - 2);
        }

        public static string ExtractRelativeRomPath(string path)
        {
            List<string> splitPath = path.Split(Path.DirectorySeparatorChar).ToList();
            return String.Join("\\", Enumerable.Reverse(splitPath).Take(4).Reverse().ToList<string>());
        }

        public static string ExtractRelativePath(string path)
        {
            List<string> splitPath = path.Split(Path.DirectorySeparatorChar).ToList();
            return String.Join("\\", Enumerable.Reverse(splitPath).Take(3).Reverse().ToList<string>());
        }

        public static string GenerateHostCode(string ip, string port, string delay, string method="0")
        {
            string combinedHostInfo;
            if (method == "0")
            {
                combinedHostInfo = ip + "|" + port + "|" + delay;
            }
            else
            {
                combinedHostInfo = ip + "|" + port + "|" + delay + "|" + method;
            }
            
            var infoBytes = System.Text.Encoding.UTF8.GetBytes(combinedHostInfo);
            return System.Convert.ToBase64String(infoBytes);
        }

        public struct HostInfo
        {
            public string IP { get; set; }
            public string Port { get; set; }
            public string Delay { get; set; }
            public string Method { get; set; }
        }

        public static HostInfo DecodeHostCode(string hostCode)
        {
            var encodedInfoBytes = System.Convert.FromBase64String(hostCode);
            var infoString = System.Text.Encoding.UTF8.GetString(encodedInfoBytes);
            var hostInfoArray = infoString.Split('|');
            HostInfo decodedHostInfo = new HostInfo();
            if (hostInfoArray.Length == 3)
            {
                decodedHostInfo.IP = hostInfoArray[0];
                decodedHostInfo.Port = hostInfoArray[1];
                decodedHostInfo.Delay = hostInfoArray[2];
                decodedHostInfo.Method = "0";
            }
            if (hostInfoArray.Length == 4)
            {
                decodedHostInfo.IP = hostInfoArray[0];
                decodedHostInfo.Port = hostInfoArray[1];
                decodedHostInfo.Delay = hostInfoArray[2];
                decodedHostInfo.Method = hostInfoArray[3];
            }
            return decodedHostInfo;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        public const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;
        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hWnd, int dwAttribute, out Rect lpRect, int cbAttribute);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        public static IntPtr NullDCWindowHandle()
        {
            Process[] processes = Process.GetProcessesByName("nullDC_Win32_Release-NoTrace");
            Process ndc = processes[0];
            IntPtr ptr = ndc.MainWindowHandle;
            return ptr;
        }

        public static Point NullDCWindowDimensions()
        {
            Rect ndcWindowDim = new Rect();
            if (Environment.OSVersion.Version.Major < 6)
            {
                GetWindowRect(NullDCWindowHandle(), ref ndcWindowDim);
            }
            else
            {
                if ((DwmGetWindowAttribute(NullDCWindowHandle(), DWMWA_EXTENDED_FRAME_BOUNDS, out ndcWindowDim, Marshal.SizeOf(ndcWindowDim)) != 0))
                {
                    //GetWindowRect(Launcher.NullDCWindowHandle(), ref ndcWindowDim);
                }
            }

            var ndcWinX = ndcWindowDim.Right - ndcWindowDim.Left;
            var ndcWinY = ndcWindowDim.Bottom - ndcWindowDim.Top;

            return new Point(ndcWinX, ndcWinY);
        }

        public static Point NullDCWindowPosition()
        {
            Rect ndcWindowDim = new Rect();
            if (Environment.OSVersion.Version.Major < 6)
            {
                GetWindowRect(NullDCWindowHandle(), ref ndcWindowDim);
            }
            else
            {
                if ((DwmGetWindowAttribute(NullDCWindowHandle(), DWMWA_EXTENDED_FRAME_BOUNDS, out ndcWindowDim, Marshal.SizeOf(ndcWindowDim)) != 0))
                {
                    //GetWindowRect(Launcher.NullDCWindowHandle(), ref ndcWindowDim);
                }
            }

            var ndcWinX = ndcWindowDim.Left;
            var ndcWinY = ndcWindowDim.Top;

            return new Point(ndcWinX, ndcWinY);
        }

        public static void SaveFpsSettings(int host_fps, int guest_fps)
        {
            string launcherCfgPath = Launcher.rootDir + "launcher.cfg";
            string lcText = File.ReadAllText(launcherCfgPath);
            string result = "";
            var host_regex = new Regex(@"^.*host_fps=.*$", RegexOptions.Multiline);
            result = host_regex.Replace(lcText, $"host_fps={host_fps}");

            var guest_regex = new Regex(@"^.*guest_fps=.*$", RegexOptions.Multiline);
            result = guest_regex.Replace(result, $"guest_fps={guest_fps}");

            File.WriteAllText(launcherCfgPath, result);
        }

        public static void SaveWindowSettings(int custom_size, int width, int height, int windowMax=0)
        {
            string launcherCfgPath = Launcher.rootDir + "launcher.cfg";
            string lcText = File.ReadAllText(launcherCfgPath);
            string result = "";

            var custom_size_regex = new Regex(@"^.*custom_size=.*$", RegexOptions.Multiline);
            result = custom_size_regex.Replace(lcText, $"custom_size={custom_size}");

            var width_regex = new Regex(@"^.*width=.*$", RegexOptions.Multiline);
            result = width_regex.Replace(result, $"width={width}");

            var height_regex = new Regex(@"^.*height=.*$", RegexOptions.Multiline);
            result = height_regex.Replace(result, $"height={height}");

            var max_regex = new Regex(@"^.*maximized=.*$", RegexOptions.Multiline);
            result = max_regex.Replace(result, $"maximized={windowMax}");

            File.WriteAllText(launcherCfgPath, result);
        }

        public static void LoadInputSettings()
        {
            string launcherCfgPath = Launcher.rootDir + "launcher.cfg";
            var launcherCfgLines = File.ReadAllLines(launcherCfgPath);
            var launcherCfgText = File.ReadAllText(launcherCfgPath);
            
            string player1_actual;
            try
            {
                player1_actual = launcherCfgLines.Where(s => s.Contains("player1=")).ToList().First();
            }
            catch
            {
                if(launcherCfgText.Contains("enable_mapper=1"))
                    player1_actual = "player1=keyboard";
                else
                    player1_actual = "player1=joy1";
                launcherCfgText += $"\n{player1_actual}";
                // strips newlines
                launcherCfgText = Regex.Replace(launcherCfgText, @"^\s*$\n|\r", string.Empty, RegexOptions.Multiline).TrimEnd();
                launcherCfgText += $"\n";
                File.WriteAllText(Launcher.rootDir + "launcher.cfg", launcherCfgText);
            }
            var player1ActualEntry = player1_actual.Split('=')[1];

            string cfgText = File.ReadAllText(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
            cfgText = cfgText.Replace("player1=joy1", "player1=" + player1ActualEntry);
            

            File.WriteAllText(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg", cfgText);
            File.WriteAllText(Launcher.rootDir + "launcher.cfg", launcherCfgText);
        }

        public static int[] LoadWindowSettings()
        {
            int[] windowSettings = new int[4];

            string launcherCfgPath = Launcher.rootDir + "launcher.cfg";
            var launcherCfgLines = File.ReadAllLines(launcherCfgPath);
            var custom_size_cfg = launcherCfgLines.Where(s => s.Contains("custom_size=")).ToList().First();
            var width_cfg = launcherCfgLines.Where(s => s.Contains("width=")).ToList().First();
            var height_cfg = launcherCfgLines.Where(s => s.Contains("height=")).ToList().First();
            var maximized_cfg = launcherCfgLines.Where(s => s.Contains("maximized=")).ToList().First();

            var customSizeEntry = custom_size_cfg.Split('=')[1];
            var widthEntry = width_cfg.Split('=')[1];
            var heightEntry = height_cfg.Split('=')[1];
            var maximizedEntry = maximized_cfg.Split('=')[1];

            windowSettings[0] = Convert.ToInt32(customSizeEntry);
            windowSettings[1] = Convert.ToInt32(widthEntry);
            windowSettings[2] = Convert.ToInt32(heightEntry);
            windowSettings[3] = Convert.ToInt32(maximizedEntry);

            return windowSettings;
        }

        public static void RestoreNvmem()
        {
            var nullDcDataPath = Path.Combine(Launcher.rootDir, @"nulldc-1-0-4-en-win\data");
            var nvmemFile = "naomi_nvmem.bin";
            var nvmemPath = Path.Combine(nullDcDataPath, nvmemFile);

            Console.WriteLine("Restoring NVMEM...");
            if (File.Exists(nvmemPath))
            {
                File.SetAttributes(nvmemPath, FileAttributes.Normal);
            }

            File.WriteAllBytes(nvmemPath,
                               Properties.Resources.naomi_nvmem_bin);

            File.SetAttributes(nvmemPath, FileAttributes.ReadOnly);
            Console.WriteLine("NVMEM Restored");
        }

        public static void RestoreNullDcCfg()
        {
            var nullDcPath = Launcher.rootDir + @"nulldc-1-0-4-en-win\";
            var cfgFile = "nullDC.cfg";
            var cfgPath = Path.Combine(nullDcPath, cfgFile);

            Console.WriteLine("Restoring NullDC Configuration...");
            File.WriteAllBytes(cfgPath,
                               Properties.Resources.nullDC_cfg);
            Console.WriteLine("NullDC Configuration Restored");
        }

        public static void RestoreLauncherCfg(bool force = false)
        {
            var launcherPath = Launcher.rootDir;
            var launcherCfgFile = "launcher.cfg";
            var launcherCfgPath = Path.Combine(launcherPath, launcherCfgFile);

            // to preserve user customizations
            // only restores if launcher.cfg file doesn't already exist
            if (!File.Exists(launcherCfgPath) || force)
            {
                Console.WriteLine("Restoring Launcher Configuration...");
                File.WriteAllBytes(launcherCfgPath,
                                   Properties.Resources.launcher_cfg);
                Console.WriteLine("Launcher Configuration Restored");
            }
        }

        public static void RestoreGamePadMappings(bool force = false)
        {
            var launcherPath = Launcher.rootDir;
            var MappingsFile = "GamePadMappingList.xml";
            var MappingsPath = Path.Combine(launcherPath, MappingsFile);

            if (!File.Exists(MappingsPath))
            {
                Console.WriteLine("Keyboard Mappings Not Found");
            }

            // to preserve user customizations
            // only restores if mappings file doesn't already exist
            if (!File.Exists(MappingsPath) || force)
            {
                Console.WriteLine("Restoring Default Keyboard Mappings...");
                File.WriteAllText(MappingsPath,
                                  Properties.Resources.GamePadMappingList);
                Console.WriteLine("Default Keyboard Mappings Restored");
            }
        }

        public static void RestoreFiles(bool force = false)
        {
            Launcher.RestoreNullDcFresh(force);
            Launcher.RestoreNullDcCfg();
            Launcher.RestoreNvmem();
            Launcher.CleanMalformedQjcFiles();
            Launcher.RestoreLauncherCfg();

            Launcher.LoadInputSettings();

            Launcher.FilesRestored = true;
        }

        public static void DeleteDirectory(string targetDir)
        {
            string[] files = Directory.GetFiles(targetDir);
            string[] dirs = Directory.GetDirectories(targetDir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(targetDir, true);
        }

        public static void RestoreNullDcFresh(bool force = false)
        {
            var NullDcPath = Path.Combine(Launcher.rootDir, @"nulldc-1-0-4-en-win");
            var NullDcZipPath = Path.Combine(Launcher.rootDir, "nulldc-1-0-4-en-win.zip");

            if (!Directory.Exists(NullDcPath))
            {
                Console.WriteLine("NullDC Folder Not Found");
            }

            if (Directory.Exists(NullDcPath) && force)
            {
                Console.WriteLine("Deleting NullDC Folder...");
                DeleteDirectory(NullDcPath);
            }

            if (!Directory.Exists(NullDcPath) || force)
            {
                Console.WriteLine("Restoring NullDC Folder...");
                File.WriteAllBytes(NullDcZipPath,
                                   Properties.Resources.nulldc_1_0_4_en_win_zip);
                ZipFile.ExtractToDirectory(NullDcZipPath, Launcher.rootDir);
            }

            File.Delete(NullDcZipPath);
        }

        public class Game
        {
            [JsonProperty("id")]
            public string ID { get; set; }

            [JsonProperty("path")]
            public string Name { get; set; }

            [JsonProperty("reference_url")]
            public string ReferenceUrl { get; set; }

            [JsonProperty("files")]
            public List<Asset> Assets { get; set; }

            [JsonProperty("root")]
            public string Root { get; set; }
            
        }

        public class Asset
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("renamed")]
            public string Renamed { get; set; }

            public string LocalName()
            {
                if (Renamed == null || Renamed.Length == 0)
                    return Name;
                else
                    return Renamed;
            }

            [JsonProperty("md5")]
            public string Md5Sum { get; set; }

            public static string CalculateMD5(string filename)
            {
                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    using (var stream = File.OpenRead(filename))
                    {
                        var hash = md5.ComputeHash(stream);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            }

            public string VerifyFile(string destinationFile)
            {
                var matchingSum = (Md5Sum.ToLower() == Asset.CalculateMD5(destinationFile));
                if (matchingSum)
                    return $"{LocalName()} Successfully Verified - MD5: {Asset.CalculateMD5(destinationFile)}";
                else
                    return $"{LocalName()} Verification Failed - MD5: {Asset.CalculateMD5(destinationFile)}";
            }

        }

    }
}
