using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        public Launcher()
        {
            MethodOptions["Frame Limit"] = 0;
            MethodOptions["Audio Sync"] = 1;
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

        public static string GetRomPathFromGameId(string gameid)
        {
            string RomDir = "nulldc-1-0-4-en-win\\roms\\";
            string GameJsonPath = rootDir + "games.json";

            JArray games = JArray.Parse(File.ReadAllText(GameJsonPath));

            var path = games.FirstOrDefault(x => x.Value<string>("gameid") == $"nulldc_{gameid}").Value<string>("path");
            path = RomDir + path;

            return path;
        }

        public static void KillAntiMicro()
        {
            var amInstances = Process.GetProcessesByName("antimicro");
            if (amInstances.Length > 0)
            {
                foreach (Process amInstance in amInstances)
                {
                    amInstance.Kill();
                    amInstance.WaitForExit();
                    amInstance.Dispose();
                }
            }
        }

        public static void LaunchAntiMicro(bool hidden=false)
        {
            string hiddenArg = "";
            KillAntiMicro();
            if (hidden)
            {
                hiddenArg = " --hidden";
            }
            Process.Start(Launcher.rootDir + "antimicro\\antimicro.exe", $"{hiddenArg} --profile {Launcher.rootDir}\\antimicro\\profiles\\nulldc.gamecontroller.amgp");
        }

        public static void LaunchNullDC(string RomPath, bool isHost = false)
        {
            string WorkDir = rootDir + "nulldc_rom_launcher\\";

            string launchArgs = "\"" + WorkDir + "nulldc_rom_launcher.ahk" + "\" " + "\"" + rootDir + RomPath + "\"";
            if (isHost == true)
                launchArgs += " host";

            Process.Start("\"" + WorkDir + "AutoHotkeyU32.exe" + "\"", launchArgs);
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
            string LauncherCfgPath = rootDir + "nullDCNetplayLauncher\\launcher.cfg";
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
            var LauncherPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            return Directory.GetParent(LauncherPath).ToString();
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

        public static void SaveFpsSettings(int host_fps, int guest_fps)
        {
            string launcherCfgPath = Launcher.rootDir + "nullDCNetplayLauncher\\launcher.cfg";
            string lcText = File.ReadAllText(launcherCfgPath);
            string result = "";
            var host_regex = new Regex(@"^.*host_fps=.*$", RegexOptions.Multiline);
            result = host_regex.Replace(lcText, $"host_fps={host_fps}");

            var guest_regex = new Regex(@"^.*guest_fps=.*$", RegexOptions.Multiline);
            result = guest_regex.Replace(result, $"guest_fps={guest_fps}");

            File.WriteAllText(launcherCfgPath, result);
        }

    }
}
