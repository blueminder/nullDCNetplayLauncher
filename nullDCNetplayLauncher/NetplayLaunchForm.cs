using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace nullDCNetplayLauncher
{
    public partial class NetplayLaunchForm : Form
    {
        Launcher launcher;
        Dictionary<String, String> romDict;
        ConnectionPresetList presets;
        UserControl sc = new SettingsControl();

        ControllerEngine controller;
        GamePadMapper gpm;

        public static Boolean EnableMapper = true;

        public NetplayLaunchForm()
        {
            controller = new ControllerEngine();

            launcher = new Launcher();
            romDict = ScanRoms();
            presets = ConnectionPreset.ReadPresetsFile();
            
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

            if (launcherCfgText.Contains("launch_antimicro=1"))
            {
                if (Process.GetProcessesByName("antimicro").Length == 0)
                {
                    Process.Start(Launcher.rootDir + "antimicro\\antimicro.exe", " --hidden --profile " + Launcher.rootDir + "\\antimicro\\profiles\\nulldc.gamecontroller.amgp");
                }
            }

            InitializeComponent();

            if(EnableMapper)
            {
                gpm = new GamePadMapper(controller);
                this.Shown += gpm.initializeController;
            }
            
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
            }
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
        }

        private void cboGameSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGameSelect.SelectedValue != null)
                Launcher.SelectedGame = cboGameSelect.SelectedValue.ToString();
        }

        private void btnDragLoad_Click(object sender, EventArgs e)
        {
            UserControl cc = new DragLoadControl();
            Form window = new Form
            {
                Text = "Drag & Load BIOS && ROMs",
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
            romDict = ScanRoms();

            cboGameSelect.DataSource = new BindingSource(romDict, null);
            cboGameSelect.DisplayMember = "Key";
            cboGameSelect.ValueMember = "Value";
        }
    }
}
