using Onova;
using Onova.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nullDCNetplayLauncher
{
    public partial class SettingsControl : UserControl
    {
        private readonly IUpdateManager _updateManager = new UpdateManager(
                new GithubPackageResolver("blueminder", "nullDCNetplayLauncher", "*Distribution*"),
                new ZipPackageExtractor());
        
        public SettingsControl()
        {
            InitializeComponent();
        }

        string[] cfgLines;

        //string p1Entry;
        //string backupEntry;
        //string p2Entry;

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            cfgLines = File.ReadAllLines(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
            //player1_old = cfgLines.Where(s => s.Contains("player1=")).ToList().First();
            //backup_old = cfgLines.Where(s => s.Contains("backup=")).ToList().First();
            //player2_old = cfgLines.Where(s => s.Contains("player2=")).ToList().First();
            
            Dictionary<string, string> InputOptions = new Dictionary<string, string>();
            InputOptions[""] = "";
            //InputOptions["Keyboard"] = "keyboard";
            //InputOptions["Joystick 1"] = "joy1";

            //cboPlayer1.DataSource = new BindingSource(InputOptions, null);
            //cboPlayer1.DisplayMember = "Key";
            //cboPlayer1.ValueMember = "Value";

            //p1Entry = player1_old.Split('=')[1];
            //backupEntry = backup_old.Split('=')[1];
            //p2Entry = player2_old.Split('=')[1];

            //cboPlayer1.SelectedValue = p1Entry;

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

            string launcherCfgPath = Launcher.rootDir + "launcher.cfg";
            string launcherText = File.ReadAllText(launcherCfgPath);
            if (launcherText.Contains("enable_mapper=1") || NetplayLaunchForm.EnableMapper == true)
            {
                chkEnableMapper.Checked = true;
            }
            else
            {
                chkEnableMapper.Checked = false;
            }

            //if (launcherText.Contains("custom_cfg=1"))
                //chkCustomCFG.Checked = true;

            Dictionary<string, string> RegionOptions = new Dictionary<string, string>();
            RegionOptions["Japan"] = "japan";
            RegionOptions["USA"] = "usa";

            if (File.Exists(Launcher.rootDir + "nulldc-1-0-4-en-win\\antilag.cfg.bak"))
            {
                chkEnableFrameLimiter.Checked = false;
                lblHostFPS.Enabled = false;
                numHostFPS.Enabled = false;
                btnSaveFPS.Enabled = false;
            }

            if (File.Exists(Launcher.rootDir + "nulldc-1-0-4-en-win\\antilag.cfg"))
            {
                chkEnableFrameLimiter.Checked = true;
                lblHostFPS.Enabled = true;
                numHostFPS.Enabled = true;
                btnSaveFPS.Enabled = true;
            }


            cboRegion.DataSource = new BindingSource(RegionOptions, null);
            cboRegion.DisplayMember = "Key";
            cboRegion.ValueMember = "Value";
            
            var launcherCfgLines = File.ReadAllLines(launcherCfgPath);
            var host_fps_old = launcherCfgLines.Where(s => s.Contains("host_fps=")).ToList().First();
            var guest_fps_old = launcherCfgLines.Where(s => s.Contains("guest_fps=")).ToList().First();
            var region_old = launcherCfgLines.Where(s => s.Contains("region=")).ToList().First();

            var hostFpsEntry = host_fps_old.Split('=')[1];
            var guestFpsEntry = guest_fps_old.Split('=')[1];
            var regionEntry = region_old.Split('=')[1];

            numHostFPS.Value = Convert.ToInt32(hostFpsEntry);

            try
            {
                Launcher.mappings = GamePadMapping.ReadMappingsFile();
                Launcher.ActiveGamePadMapping = Launcher.mappings.GamePadMappings.Where(g => g.Default == true).ToList().First();
            }
            catch
            {
                Launcher.ActiveGamePadMapping = Launcher.mappings.GamePadMappings.First();
            }

            cboGamePadMappings.DataSource = Launcher.mappings.GamePadMappings;
            cboGamePadMappings.DisplayMember = Name;

            cboGamePadMappings.SelectedItem = Launcher.ActiveGamePadMapping;

            cboRegion.SelectedValue = regionEntry;

            var us_bios_path = Path.Combine(Launcher.rootDir, "nulldc-1-0-4-en-win", "data", "naomi_boot.bin");
            if (!File.Exists(us_bios_path) && !File.Exists($"{us_bios_path}.inactive"))
                cboRegion.Enabled = false;

            lblVersion.Text = Application.ProductVersion;
        }

        public void SettingsForm_Closing(object sender, EventArgs e)
        {
            foreach (GamePadMapping mapping in Launcher.mappings.GamePadMappings)
            {
                mapping.Default = false;
            }

            ((GamePadMapping)cboGamePadMappings.SelectedValue).Default = true;

            Launcher.ActiveGamePadMapping = (GamePadMapping)cboGamePadMappings.SelectedValue;

            var path = Launcher.rootDir + "//GamePadMappingList.xml";
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(GamePadMappingList));
            StreamWriter writer = new StreamWriter(path);
            serializer.Serialize(writer.BaseStream, Launcher.mappings);
            writer.Close();

            NetplayLaunchForm.StopMapper(true);
            NetplayLaunchForm.StartMapper();

            //Launcher.LoadRegionSettings();
            Launcher.RestoreFiles();
        }

        private void btnEditCFG_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
        }

        private void btnOpenQKO_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Launcher.rootDir + "nulldc-1-0-4-en-win\\qkoJAMMA");
        }

        /*
        private void btnSaveInput_Click(object sender, EventArgs e)
        {

            foreach (GamePadMapping mapping in Launcher.mappings.GamePadMappings)
            {
                mapping.Default = false;
            }

            ((GamePadMapping)cboGamePadMappings.SelectedValue).Default = true;

            Launcher.ActiveGamePadMapping = (GamePadMapping)cboGamePadMappings.SelectedValue;

            var path = Launcher.rootDir + "//GamePadMappingList.xml";
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(GamePadMappingList));
            StreamWriter writer = new StreamWriter(path);
            serializer.Serialize(writer.BaseStream, Launcher.mappings);
            writer.Close();

            NetplayLaunchForm.StopMapper(true);
            NetplayLaunchForm.StartMapper();

            //Launcher.LoadRegionSettings();
            Launcher.RestoreFiles();

            MessageBox.Show("Main Settings Successfully Saved");
        }
        */
        private void chkEnableMapper_CheckedChanged(object sender, EventArgs e)
        {
            string launcherText = File.ReadAllText(Launcher.rootDir + "launcher.cfg");

            if (chkEnableMapper.Checked)
            {
                launcherText = launcherText.Replace("enable_mapper=0", "enable_mapper=1");
                cboGamePadMappings.Enabled = true;
                if (Launcher.mappings.GamePadMappings.Count > 1)
                    btnDeleteMapping.Enabled = true;
                NetplayLaunchForm.EnableMapper = true;
            }
            else
            {
                launcherText = launcherText.Replace("enable_mapper=1", "enable_mapper=0");
                cboGamePadMappings.Enabled = false;
                btnDeleteMapping.Enabled = false;
                NetplayLaunchForm.EnableMapper = false;
            }

            File.WriteAllText(Launcher.rootDir + "launcher.cfg", launcherText);
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
            if (Launcher.mappings.GamePadMappings.Count > 1)
            {
                var toDelete = Launcher.mappings.GamePadMappings.FirstOrDefault(p => p.Name == mappingName);
                Launcher.mappings.GamePadMappings.Remove(toDelete);

                var path = Launcher.rootDir + "GamePadMappingList.xml";
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(GamePadMappingList));
                StreamWriter writer = new StreamWriter(path);
                serializer.Serialize(writer.BaseStream, Launcher.mappings);
                writer.Close();
                Launcher.mappings = GamePadMapping.ReadMappingsFile();
                cboGamePadMappings.DataSource = Launcher.mappings.GamePadMappings;
                cboGamePadMappings.SelectedIndex = 0;
                if (Launcher.mappings.GamePadMappings.Count == 1)
                {
                    btnDeleteMapping.Enabled = false;
                }
            }
        }

        private void cboGamePadMappings_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(cboGamePadMappings.SelectedValue);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            NetplayLaunchForm.launcher.UpdateLauncher(false);
        }

        private void chkEnableFrameLimiter_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled;

            if (chkEnableFrameLimiter.Checked)
            {
                if (File.Exists(Launcher.rootDir + "nulldc-1-0-4-en-win\\antilag.cfg.bak"))
                    File.Move(Launcher.rootDir + "nulldc-1-0-4-en-win\\antilag.cfg.bak", Launcher.rootDir + "nulldc-1-0-4-en-win\\antilag.cfg");
                enabled = true;
            }
            else
            {
                if(File.Exists(Launcher.rootDir + "nulldc-1-0-4-en-win\\antilag.cfg"))
                    File.Move(Launcher.rootDir + "nulldc-1-0-4-en-win\\antilag.cfg", Launcher.rootDir + "nulldc-1-0-4-en-win\\antilag.cfg.bak");
                enabled = false;
            }

            chkEnableFrameLimiter.Checked = enabled;
            lblHostFPS.Enabled = enabled;
            numHostFPS.Enabled = enabled;
            btnSaveFPS.Enabled = enabled;
        }

        private void cboRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboRegion.SelectedValue != "")
            {
                string launcherCfgPath = Launcher.rootDir + "launcher.cfg";
                string launcherText = File.ReadAllText(Launcher.rootDir + "launcher.cfg");
                var launcherCfgLines = File.ReadAllLines(launcherCfgPath);
                var region_old = launcherCfgLines.Where(s => s.Contains("region=")).ToList().First();
                String region_val = ((KeyValuePair<string, string>)cboRegion.SelectedItem).Value;
                launcherText = launcherText.Replace(region_old, "region=" + region_val);
                File.WriteAllText(Launcher.rootDir + "launcher.cfg", launcherText);

                string cfgText = File.ReadAllText(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg");
                var us_bios_path = Path.Combine(Launcher.rootDir, "nulldc-1-0-4-en-win", "data", "naomi_boot.bin");
                if (region_val == "japan")
                {
                    cfgText = cfgText.Replace("Region=USA", "Region=JPN");
                    if (File.Exists(us_bios_path))
                        File.Move(us_bios_path, $"{us_bios_path}.inactive");
                }
                else if(region_val == "usa")
                {
                    cfgText = cfgText.Replace("Region=JPN", "Region=USA");
                    if (File.Exists($"{us_bios_path}.inactive"))
                        File.Move($"{us_bios_path}.inactive", us_bios_path);
                }
                File.WriteAllText(Launcher.rootDir + "nulldc-1-0-4-en-win\\nullDC.cfg", cfgText);
            }
        }
    }
}
