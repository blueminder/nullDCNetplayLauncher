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
        }

        private void JoinControl_Load(object sender, EventArgs e)
        {
            cboMethod.DataSource = new BindingSource(Launcher.MethodOptions, null);
            cboMethod.DisplayMember = "Key";
            cboMethod.ValueMember = "Value";
        }

        private void btnGuess_Click(object sender, EventArgs e)
        {
            long guessedDelay = Launcher.GuessDelay(txtHostIP.Text);
            if (guessedDelay >= 0)
            {
                numDelay.BackColor = Color.White;
                numDelay.Value = guessedDelay;
            }
            else
            {
                numDelay.BackColor = Color.Tomato;
                numDelay.Text = "";
            }
        }

        private void btnLaunchGame_Click(object sender, EventArgs e)
        {
            Launcher.UpdateCFGFile(
                netplayEnabled: true,
                isHost: false,
                hostAddress: txtHostIP.Text,
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

        private void txtHostPort_GotFocus(object sender, EventArgs e)
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
            string base64Pattern = @"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9 +/]{3}=)?$";
            bool isBase64 = Regex.IsMatch(txtHostCode.Text, base64Pattern);
            if (isBase64 || txtHostCode.Text == "")
            {
                var hostInfo = Launcher.DecodeHostCode(txtHostCode.Text);

                if (!String.IsNullOrEmpty(hostInfo.Port))
                {
                    cboPresetName.Text = "";

                    txtHostIP.BackColor = Color.LemonChiffon;
                    txtHostPort.BackColor = Color.LemonChiffon;
                    numDelay.BackColor = Color.LemonChiffon;
                    cboMethod.BackColor = Color.LemonChiffon;

                    txtHostIP.Text = hostInfo.IP;
                    txtHostPort.Text = hostInfo.Port;
                    numDelay.Value = Convert.ToInt32(hostInfo.Delay);

                    cboMethod.SelectedValue = Convert.ToInt32(hostInfo.Method);
                }
            }
        }

        public void SavePreset(string presetName)
        {
            var toEdit = presets.ConnectionPresets.FirstOrDefault(p => p.Name == presetName);
            if (toEdit != null)
            {
                toEdit.IP = txtHostIP.Text;
                toEdit.Port = txtHostPort.Text;
                toEdit.Delay = numDelay.Value;
                toEdit.Method = Convert.ToInt32(cboMethod.SelectedValue);
            }
            else
            {
                var toAdd = new ConnectionPreset();
                toAdd.Name = cboPresetName.Text;
                toAdd.IP = txtHostIP.Text;
                toAdd.Port = txtHostPort.Text;
                toAdd.Delay = numDelay.Value;
                toAdd.Method = Convert.ToInt32(cboMethod.SelectedValue);
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

                var path = Launcher.GetApplicationConfigurationDirectoryName() + "//ConnectionPresetList.xml";
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
                txtHostIP.Text = toLoad.IP;
                txtHostPort.Text = toLoad.Port;
                numDelay.Value = toLoad.Delay;
                cboMethod.SelectedValue = toLoad.Method;
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
            cboPresetName.BackColor = Color.White;
            txtHostCode.BackColor = Color.White;
            txtHostIP.BackColor = Color.White;
            txtHostPort.BackColor = Color.White;
            numDelay.BackColor = Color.White;
            LoadPreset(cboPresetName.Text);
        }

    }
}
