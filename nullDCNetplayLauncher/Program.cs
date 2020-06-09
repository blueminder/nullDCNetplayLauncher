using System;
using System.Collections.Generic;
using System.IO;
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

        public static void LoadInteractive()
        {
            HideConsoleWindow();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NetplayLaunchForm());
        }

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0].StartsWith("--"))
            {
                ShowConsoleWindow();
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                bool hosting = false;
                bool offline = false;
                string hostCode;
                Launcher.HostInfo hostInfo = new Launcher.HostInfo();
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
                    Console.WriteLine("--offline <0/1>");
                    Console.WriteLine("--delay <frame delay>");
                    Console.WriteLine("--host-code <code>");
                    Console.WriteLine("--hosting <0/1>");
                    Console.WriteLine("--ip <ip address>");
                    Console.WriteLine("--port <port number>");
                    return;
                }

                if (arguments.ContainsKey("root-dir"))
                {
                    Launcher.rootDir = Regex.Replace(arguments["root-dir"], @"\s+", string.Empty) + "\\";
                }

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
                    romPath = Launcher.GetRomPathFromGameId(arguments["gameid"]);
                }
                else
                {
                    Console.WriteLine("Please enter a valid LST path (--lst-path) or Game ID (--gameid) to continue.");
                    return;
                }

                if (arguments.ContainsKey("offline"))
                {
                    offline = arguments["offline"] == "1";
                    hosting = false;

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

                    if (arguments.ContainsKey("guess-ip"))
                    {
                        hostInfo.Delay = Launcher.GuessDelay(arguments["guess-ip"]).ToString();
                        Console.WriteLine($"Delay is set to {hostInfo.Delay}");
                    }
                    else if (arguments.ContainsKey("delay"))
                    {
                        hostInfo.Delay = arguments["delay"];
                    }
                    else
                    {
                        Console.WriteLine("No delay entered.");
                        return;
                    }

                    if (arguments.ContainsKey("host-code"))
                    {
                        hostCode = arguments["host-code"];
                        hostInfo = Launcher.DecodeHostCode(hostCode);
                    }
                    else
                    {
                        hostInfo.IP = arguments["ip"];
                        hostInfo.Port = arguments["port"];
                        if (hosting)
                        {
                            var genHost = Launcher.GenerateHostCode(hostInfo.IP, hostInfo.Port, hostInfo.Delay);
                            Console.WriteLine($"Generated Host Code: {genHost}");
                        }
                    }

                    Launcher.UpdateCFGFile(
                        netplayEnabled: !offline,
                        isHost: hosting,
                        hostAddress: hostInfo.IP,
                        hostPort: hostInfo.Port,
                        frameDelay: hostInfo.Delay);

                    Launcher.LaunchNullDC(
                        RomPath: romPath,
                        isHost: hosting);
                }

                
            }
            else
            {
                LoadInteractive();
            }
        }
    }
}
