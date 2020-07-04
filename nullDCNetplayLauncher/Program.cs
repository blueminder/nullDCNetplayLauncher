using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace nullDCNetplayLauncher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public static void ShowConsoleWindow()
        {
            var handle = GetConsoleWindow();

            if (handle == IntPtr.Zero)
            {
                AllocConsole();
            }
            else
            {
                ShowWindow(handle, SW_SHOW);
            }
        }

        public static void HideConsoleWindow()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
        }

        public static void LoadInteractive(bool tray = false)
        {
            HideConsoleWindow();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NetplayLaunchForm(tray));
        }

        [STAThread]
        static void Main(string[] args)
        {
            Launcher.RestoreLauncherCfg();

            if (args.Length > 0 && args[0].StartsWith("--"))
            {
                ShowConsoleWindow();
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                bool hosting = false;
                bool offline = false;
                string hostCode;
                Launcher.HostInfo hostInfo = new Launcher.HostInfo();
                hostInfo.Method = "0";
                string romPath = "";

                if (args.Length == 1 && args[0] == "--help")
                {
                    Array.Resize(ref args, args.Length + 1);
                    args[1] = "1";
                }

                for (int index = 0; index < args.Length - 1; index += 2)
                {
                    string arg = args[index].Replace("--", "");
                    if (arg == "help")
                    {
                        arguments[arg] = "1";
                    } 
                    else
                    {
                        arguments[arg] = args[index + 1];
                    }
                    
                }

                if (arguments.ContainsKey("help"))
                {
                    Console.WriteLine("--root-dir <distribution root directory>");
                    Console.WriteLine("--lst-path <path to lst file>");
                    Console.WriteLine("--gameid <game id according to games.json>");
                    Console.WriteLine("");
                    Console.WriteLine("--host-fps <max fps, 0 for uncapped>");
                    Console.WriteLine("--guest-fps <max fps, 0 for uncapped>");
                    Console.WriteLine("");
                    Console.WriteLine("--offline <0/1>");
                    Console.WriteLine("--delay <frame delay>");
                    Console.WriteLine("--host-code <code>");
                    Console.WriteLine("--hosting <0/1>");
                    Console.WriteLine("--ip <ip address>");
                    Console.WriteLine("--port <port number>");
                    Console.WriteLine("");
                    Console.WriteLine("--audio-sync (only for host)");
                    Console.WriteLine("--frame-limit (only for host)");
                    return;
                }

                if (arguments.ContainsKey("root-dir"))
                {
                    Launcher.rootDir = Regex.Replace(arguments["root-dir"], @"\s+", string.Empty) + "\\";
                }

                Launcher.RestoreFiles();

                if (!arguments.ContainsKey("offline")
                    && !arguments.ContainsKey("ip")
                    && !arguments.ContainsKey("host-code"))
                {
                    LoadInteractive();
                    return;
                }

                if (arguments.ContainsKey("lst-path"))
                {
                    romPath = arguments["lst-path"];
                }
                else if (arguments.ContainsKey("gameid"))
                {
                    if (File.Exists(Launcher.rootDir + "games.json"))
                    {
                        // disables usa bios for fightcade launching to ensure same region on both sides
                        // will add region command line option soon
                        var us_bios_path = Path.Combine(Launcher.rootDir, "nulldc-1-0-4-en-win", "data", "naomi_boot.bin");
                        if (File.Exists(us_bios_path))
                        {
                            File.Move(us_bios_path, $"{us_bios_path}.inactive");
                        }

                        romPath = Launcher.GetRomPathFromGameId(arguments["gameid"]);
                        if (romPath == null)
                        {
                            Console.WriteLine("Game not found. Please check your ROM directory, and try again.");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please provide a valid games.json file to use the launcher from command line.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid LST path (--lst-path) or Game ID (--gameid) to continue.");
                    return;
                }

                if (arguments.ContainsKey("host-fps") || arguments.ContainsKey("guest-fps"))
                {
                    string launcherCfgPath = Launcher.rootDir + "launcher.cfg";
                    var launcherCfgLines = File.ReadAllLines(launcherCfgPath);
                    
                    var host_fps_old = launcherCfgLines.Where(s => s.Contains("host_fps=")).ToList().First();
                    var guest_fps_old = launcherCfgLines.Where(s => s.Contains("guest_fps=")).ToList().First();

                    var hostFpsEntry = host_fps_old.Split('=')[1];
                    var guestFpsEntry = guest_fps_old.Split('=')[1];

                    int host_fps = Convert.ToInt32(hostFpsEntry);
                    int guest_fps = Convert.ToInt32(guestFpsEntry);
                    if (arguments.ContainsKey("host-fps"))
                    {
                        host_fps = Convert.ToInt32(arguments["host-fps"]);
                    }

                    if (arguments.ContainsKey("guest-fps"))
                    {
                        guest_fps = Convert.ToInt32(arguments["guest-fps"]);
                    }
                    Launcher.SaveFpsSettings(host_fps, guest_fps);
                }

                if (arguments.ContainsKey("offline"))
                {
                    offline = arguments["offline"] == "1";
                    hosting = false;

                    Launcher.CleanMalformedQjcFiles();

                    Launcher.UpdateCFGFile(
                        netplayEnabled: !offline,
                        isHost: hosting);

                    Launcher.LaunchNullDC(
                        RomPath: romPath,
                        isHost: hosting);
                }
                else
                {
                    if (arguments.ContainsKey("hosting"))
                    {
                        hosting = arguments["hosting"] == "1";
                    }

                    if (arguments.ContainsKey("audio-sync"))
                    {
                        hostInfo.Method = "1";
                        Console.WriteLine("audio sync");
                    }

                    if (arguments.ContainsKey("guess-ip"))
                    {
                        hostInfo.Delay = Launcher.GuessDelay(arguments["guess-ip"]).ToString();
                        Console.WriteLine($"Delay is set to {hostInfo.Delay}");
                    }
                    else if (arguments.ContainsKey("delay"))
                    {
                        hostInfo.Delay = arguments["delay"];
                    }
                    else if (arguments.ContainsKey("host-code"))
                    {
                        hostCode = arguments["host-code"];
                        hostInfo = Launcher.DecodeHostCode(hostCode);
                        Console.WriteLine($"Delay is set to {hostInfo.Delay}");
                    }
                    else
                    {
                        Console.WriteLine("No delay entered.");
                        return;
                    }

                    if (arguments.ContainsKey("ip"))
                        hostInfo.IP = arguments["ip"];
                    if (arguments.ContainsKey("port"))
                        hostInfo.Port = arguments["port"];

                    if (hosting)
                    {
                        var genHost = Launcher.GenerateHostCode(hostInfo.IP, hostInfo.Port, hostInfo.Delay, hostInfo.Method);
                        Console.WriteLine($"Generated Host Code: {genHost}");
                    }

                    Launcher.CleanMalformedQjcFiles();

                    Launcher.UpdateCFGFile(
                        netplayEnabled: !offline,
                        isHost: hosting,
                        hostAddress: hostInfo.IP,
                        hostPort: hostInfo.Port,
                        frameDelay: hostInfo.Delay,
                        frameMethod: hostInfo.Method);

                    Launcher.LaunchNullDC(
                        RomPath: romPath,
                        isHost: hosting);
                }

                LoadInteractive(tray: true);
            }
            else
            {
                LoadInteractive();
            }
        }
    }
}
