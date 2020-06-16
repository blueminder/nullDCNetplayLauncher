using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nullDCNetplayLauncher
{
    public partial class SettingsControl : UserControl
    {

        public GamePadMappingList mappings;

        public SettingsControl()
        {
            InitializeComponent();
        }

        string[] cfgLines;

        string player1_old;
        string backup_old;
        string player2_old;

        string p1Entry;
        string backupEntry;
        string p2Entry;

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            cfgLines = File.ReadAllLines(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
            player1_old = cfgLines.Where(s => s.Contains("player1=")).ToList().First();
            backup_old = cfgLines.Where(s => s.Contains("backup=")).ToList().First();
            player2_old = cfgLines.Where(s => s.Contains("player2=")).ToList().First();

            Dictionary<string, string> InputOptions = new Dictionary<string, string>();
            InputOptions[""] = "";
            InputOptions["Keyboard"] = "keyboard";
            InputOptions["Joystick 1"] = "joy1";
            InputOptions["Joystick 2"] = "joy2";

            cboPlayer1.DataSource = new BindingSource(InputOptions, null);
            cboPlayer1.DisplayMember = "Key";
            cboPlayer1.ValueMember = "Value";

            cboBackup.DataSource = new BindingSource(InputOptions, null);
            cboBackup.DisplayMember = "Key";
            cboBackup.ValueMember = "Value";

            cboPlayer2.DataSource = new BindingSource(InputOptions, null);
            cboPlayer2.DisplayMember = "Key";
            cboPlayer2.ValueMember = "Value";

            p1Entry = player1_old.Split('=')[1];
            backupEntry = backup_old.Split('=')[1];
            p2Entry = player2_old.Split('=')[1];

            cboPlayer1.SelectedValue = p1Entry;
            cboBackup.SelectedValue = backupEntry;
            cboPlayer2.SelectedValue = p2Entry;

            var windowSettings = Launcher.LoadWindowSettings();
            if (windowSettings[3] == 1)
            {
                rdoStartMax.Checked = true;
            }
            else if(windowSettings[0] == 1)
            {
                rdoCustomSize.Checked = true;
            }
            else
            {
                rdoDefault.Checked = true;
            }

            txtWindowX.Text = windowSettings[1].ToString();
            txtWindowY.Text = windowSettings[2].ToString();

            string launcherText = File.ReadAllText(Launcher.rootDir + "launcher.cfg");
            if (launcherText.Contains("enable_mapper=1"))
            {
                chkEnableMapper.Checked = true;
            }
            else
            {
                chkEnableMapper.Checked = false;
            }

            string launcherCfgPath = Launcher.rootDir + "launcher.cfg";
            var launcherCfgLines = File.ReadAllLines(launcherCfgPath);
            var host_fps_old = launcherCfgLines.Where(s => s.Contains("host_fps=")).ToList().First();
            var guest_fps_old = launcherCfgLines.Where(s => s.Contains("guest_fps=")).ToList().First();

            var hostFpsEntry = host_fps_old.Split('=')[1];
            var guestFpsEntry = guest_fps_old.Split('=')[1];

            numHostFPS.Value = Convert.ToInt32(hostFpsEntry);

            mappings = Launcher.mappings;
            cboGamePadMappings.DataSource = mappings.GamePadMappings;
            cboGamePadMappings.DisplayMember = Launcher.ActiveGamePadMapping.Name;
        }

        private void btnEditCFG_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
        }

        private void btnOpenQKO_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Launcher.rootDir + "nulldc-1-0-4-en-win\\qkoJAMMA");
        }

        private void btnSaveInput_Click(object sender, EventArgs e)
        {
            string launcherText = File.ReadAllText(Launcher.rootDir + "launcher.cfg");
            if (chkEnableMapper.Checked)
            {
                launcherText = launcherText.Replace("enable_mapper=0", "enable_mapper=1");
            }
            else
            {
                launcherText = launcherText.Replace("enable_mapper=1", "enable_mapper=0");
            }
            File.WriteAllText(Launcher.rootDir + "launcher.cfg", launcherText);

            string cfgText = File.ReadAllText(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
            String p1_val = $"{cboPlayer1.SelectedValue}";
            String bak_val = $"{cboBackup.SelectedValue}";
            String p2_val = $"{cboPlayer2.SelectedValue}";

            if (p1_val.Length == 0)
                p1_val = "NULL";
            if (bak_val.Length == 0)
                bak_val = "NULL";
            if (p2_val.Length == 0)
                p2_val = "NULL";

            cfgText = cfgText.Replace(player1_old, "player1=" + p1_val);
            cfgText = cfgText.Replace(backup_old, "backup=" + bak_val);
            cfgText = cfgText.Replace(player2_old, "player2=" + p2_val);
            File.WriteAllText(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg", cfgText);

            // reload from file
            cfgLines = File.ReadAllLines(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
            player1_old = cfgLines.Where(s => s.Contains("player1=")).ToList().First();
            backup_old = cfgLines.Where(s => s.Contains("backup=")).ToList().First();
            player2_old = cfgLines.Where(s => s.Contains("player2=")).ToList().First();

            MessageBox.Show("Input Settings Successfully Saved");
        }

        private void chkEnableMapper_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableMapper.Checked)
            {
                cboPlayer1.Text = "Keyboard";
                cboPlayer1.Enabled = false;
                cboGamePadMappings.Enabled = true;
                if (Launcher.mappings.GamePadMappings.Count > 1)
                    btnDeleteMapping.Enabled = true;
                NetplayLaunchForm.EnableMapper = true;
            }
            else
            {
                cboPlayer1.Enabled = true;
                cboGamePadMappings.Enabled = false;
                btnDeleteMapping.Enabled = false;
                NetplayLaunchForm.EnableMapper = false;
            }
        }

        private void btnJoyCpl_Click(object sender, EventArgs e)
        {
            Process.Start("joy.cpl");
        }

        private void btnSaveWindow_Click(object sender, EventArgs e)
        {
            var width = Convert.ToInt32(txtWindowX.Text);
            var height = Convert.ToInt32(txtWindowY.Text);

            if (rdoStartMax.Checked)
                Launcher.SaveWindowSettings(0, width, height, 1);
            else if (rdoCustomSize.Checked)
                Launcher.SaveWindowSettings(1, width, height);
            else
                Launcher.SaveWindowSettings(0, width, height);
            MessageBox.Show("Window Settings Successfully Saved");
        }

        private void btnGrabWindowSize_Click(object sender, EventArgs e)
        {
            rdoCustomSize.Checked = true;
            Point ndcWin = Launcher.NullDCWindowDimensions();
            txtWindowX.Text = ndcWin.X.ToString();
            txtWindowY.Text = ndcWin.Y.ToString();
        }

        private void btnSaveFPS_Click(object sender, EventArgs e)
        {
            Launcher.SaveFpsSettings(Convert.ToInt32(numHostFPS.Value), 90);
            MessageBox.Show("FPS Limits Successfully Saved");
        }

        private void txtWindowX_GotFocus(object sender, EventArgs e)
        {
            rdoCustomSize.Checked = true;
        }

        private void txtWindowY_GotFocus(object sender, EventArgs e)
        {
            rdoCustomSize.Checked = true;
        }

        private void btnDeleteMapping_Click(object sender, EventArgs e)
        {
            DeleteMapping(cboGamePadMappings.Text);
        }

        public void DeleteMapping(string mappingName)
        {
            mappings = Launcher.mappings;
            if (mappings.GamePadMappings.Count > 1)
            {
                var toDelete = mappings.GamePadMappings.FirstOrDefault(p => p.Name == mappingName);
                mappings.GamePadMappings.Remove(toDelete);

                var path = Launcher.rootDir + "GamePadMappingList.xml";
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(GamePadMappingList));
                StreamWriter writer = new StreamWriter(path);
                serializer.Serialize(writer.BaseStream, mappings);
                writer.Close();
                mappings = GamePadMapping.ReadMappingsFile();
                Launcher.mappings = GamePadMapping.ReadMappingsFile();
                cboGamePadMappings.DataSource = Launcher.mappings.GamePadMappings;
                cboGamePadMappings.SelectedIndex = 0;
                if (mappings.GamePadMappings.Count == 1)
                {
                    btnDeleteMapping.Enabled = false;
                }
            }
            Launcher.mappings = this.mappings;
        }
    }
}
