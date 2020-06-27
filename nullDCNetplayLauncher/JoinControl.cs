using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace nullDCNetplayLauncher
{
    public partial class JoinControl : UserControl
    {
        ConnectionPresetList presets;

        public JoinControl()
        {
            presets = ConnectionPreset.ReadPresetsFile();
            InitializeComponent();

            cboPresetName.DataSource = presets.ConnectionPresets;

            btnDeletePreset.Enabled = presets.ConnectionPresets.Count > 1;

            Dictionary<string, string> RegionOptions = new Dictionary<string, string>();
            RegionOptions["Japan"] = "japan";
            RegionOptions["USA"] = "usa";

            cboRegion.DataSource = new BindingSource(RegionOptions, null);
            cboRegion.DisplayMember = "Key";
            cboRegion.ValueMember = "Value";
        }

        private void JoinControl_Load(object sender, EventArgs e)
        {
            cboMethod.DataSource = new BindingSource(Launcher.MethodOptions, null);
            cboMethod.DisplayMember = "Key";
            cboMethod.ValueMember = "Value";

            txtOpponentIP.Text = "";
        }

        private void GuessDelay(string ip)
        {
            long guessedDelay = Launcher.GuessDelay(ip);
            if (guessedDelay >= 0)
            {
                numDelay.Value = guessedDelay;
                txtOpponentIP.BackColor = Color.Honeydew;
                numDelay.BackColor = Color.Honeydew;
            }
            else
            {
                numDelay.Text = "";
                txtOpponentIP.BackColor = Color.LightCoral;
                numDelay.BackColor = Color.LightCoral;
            }
        }

        private void btnGuess_Click(object sender, EventArgs e)
        {
            GuessDelay(txtOpponentIP.Text);
        }

        private void btnLaunchGame_Click(object sender, EventArgs e)
        {
            Launcher.SwitchRegion(cboRegion.SelectedValue.ToString());
            Launcher.UpdateCFGFile(
                netplayEnabled: true,
                isHost: false,
                hostAddress: txtOpponentIP.Text,
                hostPort: txtHostPort.Text,
                frameDelay: Convert.ToInt32(numDelay.Value)
                                   .ToString());
            Launcher.LaunchNullDC(Launcher.SelectedGame, isHost: false);
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            txtHostCode.BackColor = Color.LemonChiffon;
            txtHostCode.Text = Clipboard.GetText();
        }

        private void txtHostCode_GotFocus(object sender, EventArgs e)
        {
            txtHostCode.BackColor = Color.White;
        }

        private void txtGuestPort_GotFocus(object sender, EventArgs e)
        {
            txtHostPort.BackColor = Color.White;
        }

        private void numDelay_GotFocus(object sender, EventArgs e)
        {
            txtHostCode.BackColor = Color.White;
        }

        private void cboPresetName_GotFocus(object sender, EventArgs e)
        {
            cboPresetName.BackColor = Color.White;
        }

        private void txtHostCode_TextChanged(object sender, EventArgs e)
        {
            txtHostCode.BackColor = Color.White;
            string base64Pattern = @"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9 +/]{3}=)?$";
            bool isBase64 = Regex.IsMatch(txtHostCode.Text, base64Pattern);
            if (isBase64 || txtHostCode.Text == "")
            {
                var hostInfo = Launcher.DecodeHostCode(txtHostCode.Text);

                if (!String.IsNullOrEmpty(hostInfo.Port))
                {
                    cboPresetName.Text = "";

                    txtHostCode.BackColor = Color.LemonChiffon;

                    txtOpponentIP.BackColor = Color.Honeydew;
                    numDelay.BackColor = Color.Honeydew;

                    txtHostIP.BackColor = Color.LemonChiffon;
                    txtHostPort.BackColor = Color.LemonChiffon;
                    cboMethod.BackColor = Color.LemonChiffon;
                    cboRegion.BackColor = Color.LemonChiffon;

                    txtOpponentIP.Text = hostInfo.IP;
                    txtHostIP.Text = hostInfo.IP;
                    txtHostPort.Text = hostInfo.Port;
                    numDelay.Value = Convert.ToInt32(hostInfo.Delay);

                    var oldRegion = cboRegion.SelectedValue.ToString();
                    var newRegion = hostInfo.Region;
                    cboRegion.SelectedValue = hostInfo.Region;

                    var oldMethod = Convert.ToInt32(cboMethod.SelectedValue);
                    var newMethod = Convert.ToInt32(hostInfo.Method);
                    cboMethod.SelectedValue = newMethod;

                    if (oldMethod != newMethod || oldRegion != newRegion)
                    {
                        splitGuest.Panel2Collapsed = false;

                        btnExpandCollapse.Text = "↓   Advanced Options   ↓";
                        var win = this.Parent;
                        win.Size = win.MaximumSize;
                    }
                }
            }
        }

        public void SavePreset(string presetName)
        {
            var toEdit = presets.ConnectionPresets.FirstOrDefault(p => p.Name == presetName);
            if (toEdit != null)
            {
                toEdit.IP = txtOpponentIP.Text;
                toEdit.Port = txtHostPort.Text;
                toEdit.Delay = numDelay.Value;
                toEdit.Method = Convert.ToInt32(cboMethod.SelectedValue);
                toEdit.Region = cboRegion.SelectedValue.ToString();
            }
            else
            {
                var toAdd = new ConnectionPreset();
                toAdd.Name = cboPresetName.Text;
                toAdd.IP = txtOpponentIP.Text;
                toAdd.Port = txtHostPort.Text;
                toAdd.Delay = numDelay.Value;
                toAdd.Method = Convert.ToInt32(cboMethod.SelectedValue);
                toAdd.Region = cboRegion.SelectedValue.ToString();
                presets.ConnectionPresets.Add(toAdd);
            }

            var path = Launcher.GetApplicationConfigurationDirectoryName() + "//ConnectionPresetList.xml";
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(ConnectionPresetList));
            StreamWriter writer = new StreamWriter(path);
            serializer.Serialize(writer.BaseStream, presets);
            writer.Close();
            presets = ConnectionPreset.ReadPresetsFile();
            cboPresetName.DataSource = presets.ConnectionPresets;
            cboPresetName.SelectedIndex = cboPresetName.FindStringExact(presetName);
            if (presets.ConnectionPresets.Count > 1)
            {
                btnDeletePreset.Enabled = true;
            }
            cboPresetName.BackColor = Color.LemonChiffon;
        }

        public void DeletePreset(string presetName)
        {
            if (presets.ConnectionPresets.Count > 1)
            {
                var toDelete = presets.ConnectionPresets.FirstOrDefault(p => p.Name == presetName);
                presets.ConnectionPresets.Remove(toDelete);

                var path = Launcher.rootDir + "\\ConnectionPresetList.xml";
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(ConnectionPresetList));
                StreamWriter writer = new StreamWriter(path);
                serializer.Serialize(writer.BaseStream, presets);
                writer.Close();
                presets = ConnectionPreset.ReadPresetsFile();
                cboPresetName.DataSource = presets.ConnectionPresets;
                cboPresetName.SelectedIndex = 0;
                if (presets.ConnectionPresets.Count == 1)
                {
                    btnDeletePreset.Enabled = false;
                }
            }
        }

        private void LoadPreset(string presetName)
        {
            ConnectionPreset toLoad = presets.ConnectionPresets.FirstOrDefault(p => p.Name == presetName);
            if (toLoad != null)
            {
                txtHostCode.Text = "";
                txtOpponentIP.Text = toLoad.IP;
                txtHostIP.Text = toLoad.IP;
                txtHostPort.Text = toLoad.Port;
                numDelay.Value = toLoad.Delay;
                cboMethod.SelectedValue = toLoad.Method;
                cboRegion.SelectedValue = toLoad.Region;
            }
        }

        private void btnSavePreset_Click(object sender, EventArgs e)
        {
            SavePreset(cboPresetName.Text);
        }

        private void btnDeletePreset_Click(object sender, EventArgs e)
        {
            DeletePreset(cboPresetName.Text);
        }

        private void cboPresetName_TextChanged(object sender, EventArgs e)
        {
            LoadPreset(cboPresetName.Text);
        }

        private void cboPresetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHostCode.Text = "";
            cboPresetName.BackColor = Color.White;
            txtHostCode.BackColor = Color.White;
            txtOpponentIP.BackColor = Color.White;
            txtHostIP.BackColor = Color.White;
            txtHostPort.BackColor = Color.White;
            numDelay.BackColor = Color.White;
            cboMethod.BackColor = Color.White;
            LoadPreset(cboPresetName.Text);
        }

        private void btnExpandCollapse_Click(object sender, EventArgs e)
        {
            var win = this.Parent;
            if (splitGuest.Panel2Collapsed)
            {
                btnExpandCollapse.Text = "↓   Advanced Options   ↓";
                win.Size = win.MaximumSize;
                splitGuest.Panel2Collapsed = false;
            }
            else
            {
                btnExpandCollapse.Text = "↑   Advanced Options   ↑";
                win.Size = win.MinimumSize;
                splitGuest.Panel2Collapsed = true;
            }
        }
    }
}
