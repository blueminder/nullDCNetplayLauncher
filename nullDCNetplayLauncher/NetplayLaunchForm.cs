﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace nullDCNetplayLauncher
{

    public partial class NetplayLaunchForm : Form
    {
        Launcher launcher;
        Dictionary<String, String> romDict;
        ConnectionPresetList presets;
        
        UserControl sc = new SettingsControl();

        public static ControllerEngine controller;
        public static GamePadMapper gpm;

        List<Game> GamesJson;

        public static Boolean EnableMapper = false;
        public static Boolean StartTray;

        public NetplayLaunchForm(bool tray = false)
        {
            controller = new ControllerEngine();

            launcher = new Launcher();
            presets = ConnectionPreset.ReadPresetsFile();

            StartTray = tray;

            if (!StartTray && !Launcher.FilesRestored)
            {
                Launcher.RestoreFiles();
            }
            
            string launcherCfgText = "";
            try
            {
                var launcherCfgUri = new System.Uri(Path.Combine(Launcher.rootDir, @"launcher.cfg"));
                launcherCfgText = File.ReadAllText(launcherCfgUri.LocalPath);
            }
            catch(System.IO.DirectoryNotFoundException)
            {
                MessageBox.Show("launcher.cfg not found. Please enter a valid root directory.");
                System.Environment.Exit(1);
            }

            if (launcherCfgText.Contains("enable_mapper=1"))
            {
                StartMapper();
            }

            //InitializeComponent(StartTray);
            InitializeComponent();

            if (StartTray)
                this.WindowState = FormWindowState.Minimized;

            ReloadRomList();

            if (StartTray)
            {
                Process[] processes = Process.GetProcessesByName("nullDC_Win32_Release-NoTrace");
                if (processes.Length > 0)
                {
                    Process nulldcProcess = processes[0];
                    nulldcProcess.EnableRaisingEvents = true;

                    nulldcProcess.Exited += (sender, e) =>
                    {
                        Application.Exit();
                    };
                }
            }
            
        }

        private void ReloadRomList()
        {
            //if (File.Exists(Launcher.rootDir + "games.json"))
            romDict = ScanRomsFromJson();
            
            cboGameSelect.DataSource = new BindingSource(romDict, null);
            cboGameSelect.DisplayMember = "Key";
            cboGameSelect.ValueMember = "Value";

            if (romDict.Count == 1 && romDict.First().Key == "")
            {
                btnOffline.Enabled = false;
                btnHost.Enabled = false;
                btnJoin.Enabled = false;
                cboGameSelect.Enabled = false;
            }
            else
            {
                Launcher.SelectedGame = romDict.First().Value;
                btnOffline.Enabled = true;
                btnHost.Enabled = true;
                btnJoin.Enabled = true;
                cboGameSelect.Enabled = true;
            }
        }

        public static void StartMapper()
        {
            EnableMapper = true;
            controller.clock.Start();
            gpm = new GamePadMapper(controller);
            gpm.InitializeController(NetplayLaunchForm.ActiveForm, null);
        }

        public static void StopMapper(bool detach = false)
        {
            EnableMapper = false;
            controller.clock.Stop();
            if (gpm != null)
            {
                if (detach)
                    gpm.DetachController();
                gpm.Dispose();
            }
        }

        private void NetplayLaunchForm_Resize(object sender, EventArgs e)
        {
            if (StartTray && this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }


        private void btnOffline_Click(object sender, EventArgs e)
        {
            Launcher.UpdateCFGFile(
                netplayEnabled: false);
            Launcher.LaunchNullDC(cboGameSelect.SelectedValue.ToString());
        }

        private void btnHost_Click(object sender, EventArgs e)
        {
            UserControl hc = new HostControl();
            Form window = new Form
            {
                Text = "Host Game",
                TopLevel = true,
                FormBorderStyle = FormBorderStyle.Fixed3D,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = hc.Size,
                Icon = nullDCNetplayLauncher.Properties.Resources.round_multiple_stop_black_24dp
            };

            window.MinimumSize = new Size(230, 195);
            window.MaximumSize = new Size(230, 355);
            window.Size = window.MinimumSize;
            window.Controls.Add(hc);
            hc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            window.ShowDialog();
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            UserControl jc = new JoinControl();
            Form window = new Form
            {
                Text = "Join Game",
                TopLevel = true,
                FormBorderStyle = FormBorderStyle.Fixed3D,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = jc.Size,
                Icon = nullDCNetplayLauncher.Properties.Resources.round_multiple_stop_black_24dp
            };

            window.MinimumSize = new Size(230, 195);
            window.MaximumSize = new Size(230, 355);
            window.Size = window.MinimumSize;
            window.Controls.Add(jc);
            jc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            window.ShowDialog();
        }

        private Dictionary<string, string> ScanRoms()
        {
            string RomDir = Launcher.rootDir + "nulldc-1-0-4-en-win\\roms\\";
            Dictionary<string, string> romDict;
            try
            {
                List<string> romPaths = Directory.GetFiles(RomDir, "*?.lst", SearchOption.AllDirectories).Where(item => item.EndsWith(".lst")).ToList();
                romDict = romPaths
                    .ToDictionary(x => Launcher.ExtractRomNameFromPath(x), x => Launcher.ExtractRelativeRomPath(x));
                if (romDict.Count == 0)
                {
                    throw new FileNotFoundException();
                }
            }
            catch (Exception)
            {
                List<string> romPaths = new List<string>();
                romPaths.Add("");
                romDict = romPaths.ToDictionary(x => x, x => x);
            }

            return romDict;
        }

        private Dictionary<string, string> ScanRomsFromJson()
        {
            string NullDir = Launcher.rootDir + "nulldc-1-0-4-en-win\\";
            string RomDir = NullDir + "roms\\";

            string GameJsonPath = Launcher.rootDir + "games.json";

            JArray games = JArray.Parse(File.ReadAllText(GameJsonPath));

            var romDict = new Dictionary<string, string>();
            List<Game> all;
            try
            {
                GamesJson = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(GameJsonPath));
                all = GamesJson.Where(g => g.Root == "roms").ToList();

                foreach(Game game in all)
                {
                    string RomPath = Path.Combine(NullDir, game.Root, game.Name, game.Assets.First().Destination);
                    if (File.Exists(RomPath))
                    {
                        System.Diagnostics.Debug.WriteLine(RomPath);
                        romDict.Add(game.Name, RomPath);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"MISSING: {RomPath}");
                        romDict.Add(game.Name, "");
                    }
                }
            }
            catch (Exception ex)
            {
                romDict = ScanRoms();
            }

            return romDict;
        }

        private string GetReferenceUrlFromGameId(string gameid)
        {
            string NullDir = Launcher.rootDir + "nulldc-1-0-4-en-win\\";
            string RomDir = NullDir + "roms\\";

            string GameJsonPath = Launcher.rootDir + "games.json";

            JArray games = JArray.Parse(File.ReadAllText(GameJsonPath));

            var url = games.FirstOrDefault(x => x.Value<string>("gameid") == $"nulldc_{gameid}").Value<string>("reference_url");
            
            return url;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Form window = new Form
            {
                Text = "Settings",
                TopLevel = true,
                FormBorderStyle = FormBorderStyle.Fixed3D,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = sc.Size,
                Icon = nullDCNetplayLauncher.Properties.Resources.settings
        };

            window.Controls.Add(sc);
            sc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            window.ShowDialog();
        }

        private void btnController_Click(object sender, EventArgs e)
        {
            StopMapper(true);
            UserControl cc = new ControllerControl(controller);
            Form window = new Form
            {
                Text = "Controller Setup",
                TopLevel = true,
                FormBorderStyle = FormBorderStyle.Fixed3D,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = cc.Size,
                Icon = nullDCNetplayLauncher.Properties.Resources.icons8_game_controller_26_ico
            };
            window.Controls.Add(cc);
            cc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            window.ShowDialog();

            if (EnableMapper)
            {
                Launcher.mappings = GamePadMapping.ReadMappingsFile();
                try
                {
                    Launcher.ActiveGamePadMapping = Launcher.mappings.GamePadMappings.Where(g => g.Default == true).ToList().First();

                }
                catch
                {
                    Launcher.ActiveGamePadMapping = Launcher.mappings.GamePadMappings.First();

                }
                StartMapper();
            }
        }

        // https://stackoverflow.com/questions/4667532/colour-individual-items-in-a-winforms-combobox
        private void cboGameSelect_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background 
            e.DrawBackground();
            // Get the item text    
            //string text = ((ComboBox)sender).Items[e.Index].ToString();
            //string text = ((ComboBox)sender).Items[e.Index].ToString().Split(',')[0].Remove(0, 1);
            string displayMemberText = ((ComboBox)sender).GetItemText(((ComboBox)sender).Items[e.Index]);
            var item = (System.Collections.Generic.KeyValuePair<string, string>)((ComboBox)sender).Items[e.Index];

            // Determine the forecolor based on whether or not the item is selected    
            Brush brush;
            if (item.Value.Equals(""))// compare  date with your list.  
            {
                brush = Brushes.LightCoral;
            }
            else
            {
                brush = Brushes.Black;
            }

            // Draw the text    
            e.Graphics.DrawString(displayMemberText, ((Control)sender).Font, brush, e.Bounds.X, e.Bounds.Y);
        }

        private void cboGameSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NullDir = Launcher.rootDir + "nulldc-1-0-4-en-win\\";
            string RomDir = NullDir + "roms\\";

            bool ReferenceFound = false;
            Game SelectedGame = GamesJson.Where(g => g.Name == cboGameSelect.Text).FirstOrDefault();
            if (SelectedGame != null)
                ReferenceFound = (SelectedGame.ReferenceUrl != null);
            if ((string)cboGameSelect.SelectedValue == "" && ReferenceFound)
            {
                DialogResult dialogResult = MessageBox.Show($"{cboGameSelect.Text} not installed.\nWould you like to retrieve it?", cboGameSelect.Text, MessageBoxButtons.YesNo);
                switch(dialogResult)
                {
                    case (DialogResult.Yes):
                        Program.ShowConsoleWindow();
                        Console.Clear();
                        Console.WriteLine($"Downloading {cboGameSelect.Text}...");
                        var di = new DirectoryInfo(RomDir);
                        di.Attributes |= FileAttributes.Normal;
                        var zipPath = RomDir + $"{SelectedGame.ID}.zip";
                        
                        if(!File.Exists(zipPath))
                        {
                            using (WebClient client = new WebClient())
                            {
                                client.DownloadFile(new Uri(SelectedGame.ReferenceUrl),
                                                    zipPath);
                            }
                        }
                        
                        Console.WriteLine($"Download Complete");
                        Console.WriteLine($"Extracting...");

                        var extractPath = RomDir + SelectedGame.Name;
                        using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                        {
                            List<Asset> files = SelectedGame.Assets;
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                var fileEntry = files.Where(f => f.Source == entry.Name).First();
                                if (fileEntry != null)
                                {
                                    var destinationFile = Path.Combine(extractPath, fileEntry.Destination);
                                    System.IO.Directory.CreateDirectory(Path.GetDirectoryName(destinationFile));
                                    entry.ExtractToFile(destinationFile, true);
                                    var matchingSum = (fileEntry.Md5Sum.ToLower() == Asset.CalculateMD5(destinationFile));
                                    Console.WriteLine($"{fileEntry.Md5Sum.ToLower()} {Asset.CalculateMD5(destinationFile)}");
                                    if (matchingSum)
                                        Console.WriteLine($"{fileEntry.Destination} Successfully Verified");
                                    else
                                        Console.WriteLine($"{fileEntry.Destination} Verification Failed");
                                }
                                    
                            }
                        }
                        //File.Delete(zipPath);

                        Console.ReadKey();
                        Program.HideConsoleWindow();
                        var old = cboGameSelect.SelectedIndex;
                        ReloadRomList();
                        cboGameSelect.SelectedIndex = old;
                        break;
                    case (DialogResult.No):
                        break;
                }
            }
            else if (cboGameSelect.SelectedValue != null)
                Launcher.SelectedGame = cboGameSelect.SelectedValue.ToString();


        }

        private void btnDragLoad_Click(object sender, EventArgs e)
        {
            UserControl cc = new DragLoadControl();
            Form window = new Form
            {
                Text = "File Drop",
                TopLevel = true,
                FormBorderStyle = FormBorderStyle.Fixed3D,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = cc.Size,
                Icon = nullDCNetplayLauncher.Properties.Resources.round_publish_black_18dp1
            };

            window.Controls.Add(cc);
            cc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            window.ShowDialog();
            ReloadRomList();
        }

        public void wb_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            e.Cancel = true;
            // open links in default browser
            Process.Start(e.Url.ToString());
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            WebBrowser wb = new WebBrowser();
            string readme_html = Properties.Resources.README_html;
            wb.DocumentText = readme_html;
            wb.Size = new Size(800, 600);

            wb.Navigating += new WebBrowserNavigatingEventHandler(this.wb_Navigating);

            Form window = new Form
            {
                Text = "Help",
                TopLevel = true,
                FormBorderStyle = FormBorderStyle.Fixed3D,
                MaximizeBox = false,
                MinimizeBox = true,
                ClientSize = wb.Size,
                Icon = nullDCNetplayLauncher.Properties.Resources.round_not_listed_location_black_18dp1
            };
            window.Controls.Add(wb);
            wb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            window.Show();
        }

        
    }

    class Game
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

    class Asset
    {
        [JsonProperty("src")]
        public string Source { get; set; }

        [JsonProperty("dst")]
        public string Destination { get; set; }

        [JsonProperty("md5")]
        public string Md5Sum { get; set; }

        public static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }

}
