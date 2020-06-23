using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.DirectoryServices;
using System.Text.RegularExpressions;

namespace nullDCNetplayLauncher
{
    public partial class HostControl : UserControl
    {
        ConnectionPresetList presets;

        public HostControl()
        {
            presets = ConnectionPreset.ReadPresetsFile();
            InitializeComponent();

            cboPresetName.DataSource = presets.ConnectionPresets;

            btnDeletePreset.Enabled = presets.ConnectionPresets.Count > 1;
        }

        private void HostControl_Load(object sender, EventArgs e)
        {
            cboMethod.DataSource = new BindingSource(Launcher.MethodOptions, null);
            cboMethod.DisplayMember = "Key";
            cboMethod.ValueMember = "Value";
            

            cboHostIP.DataSource = new BindingSource(Launcher.NetQuery.LocalIPsByNetwork, null);
            cboHostIP.DisplayMember = "Value";
            cboHostIP.ValueMember = "Value";
            
            String hostIP;
            if (NetworkQuery.GetRadminHostIP() != null)
                hostIP = NetworkQuery.GetRadminHostIP();
            else if (NetworkQuery.GetExternalIP() != null)
                hostIP = NetworkQuery.GetExternalIP();
            else
                hostIP = (string)Launcher.NetQuery.LocalIPsByNetwork.First().Value;
            cboHostIP.SelectedValue = hostIP;
            
        }

        public void SavePreset(string presetName)
        {
            var toEdit = presets.ConnectionPresets.FirstOrDefault(p => p.Name == presetName);
            if (toEdit != null)
            {
                toEdit.IP = cboHostIP.Text;
                toEdit.Port = txtHostPort.Text;
                toEdit.Delay = numDelay.Value;
                toEdit.Method = Convert.ToInt32(cboMethod.SelectedValue);
            }
            else
            {
                var toAdd = new ConnectionPreset();
                toAdd.Name = cboPresetName.Text;
                toAdd.IP = cboHostIP.Text;
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
                cboHostIP.Text = toLoad.IP;
                txtHostPort.Text = toLoad.Port;
                numDelay.Value = toLoad.Delay;
                cboMethod.SelectedValue = toLoad.Method;
            }
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

        private void btnGenHostCode_Click(object sender, EventArgs e)
        {
            var hostCode = Launcher.GenerateHostCode(cboHostIP.Text,
                                                     txtHostPort.Text,
                                                     Convert.ToInt32(numDelay.Value).ToString(),
                                                     Convert.ToInt32(cboMethod.SelectedValue).ToString());
            txtHostCode.Text = hostCode;
            txtHostCode.BackColor = Color.Honeydew;
        }

        private void btnLaunchGame_Click(object sender, EventArgs e)
        {
            Launcher.UpdateCFGFile(
                netplayEnabled: true,
                isHost: true,
                hostAddress: cboHostIP.Text,
                hostPort: txtHostPort.Text,
                frameDelay: Convert.ToInt32(numDelay.Value)
                                   .ToString(),
                frameMethod: cboMethod.SelectedValue.ToString());
            Launcher.LaunchNullDC(Launcher.SelectedGame, isHost: true);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (txtHostCode.Text == "")
            {
                txtHostCode.BackColor = Color.Tomato;
            }
            else
            {
                txtHostCode.BackColor = Color.LemonChiffon;
                Clipboard.SetText(txtHostCode.Text);
            }
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
            numDelay.BackColor = Color.White;
        }

        private void cboPresetName_GotFocus(object sender, EventArgs e)
        {
            cboPresetName.BackColor = Color.White;
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
            txtHostCode.BackColor = Color.White;
            cboHostIP.BackColor = Color.White;
            txtHostPort.BackColor = Color.White;
            numDelay.BackColor = Color.White;
            cboMethod.BackColor = Color.White;
            LoadPreset(cboPresetName.Text);
        }

        private void btnExpandCollapse_Click(object sender, EventArgs e)
        {
            var win = this.Parent;
            if (splitHost.Panel2Collapsed)
            {
                win.Size = win.MaximumSize;
                splitHost.Panel2Collapsed = false;
            }
            else
            {
                win.Size = win.MinimumSize;
                splitHost.Panel2Collapsed = true;
            }
        }

        private void txtOpponentIP_TextChanged(object sender, EventArgs e)
        {
            if (NetworkQuery.ValidateIPv4(txtOpponentIP.Text))
                GuessDelay(txtOpponentIP.Text);
            else
                txtOpponentIP.BackColor = Color.White;
        }

        private void numDelay_ValueChanged(object sender, EventArgs e)
        {
            numDelay.BackColor = Color.White;
        }

        private void txtHostCode_TextChanged(object sender, EventArgs e)
        {
            txtHostCode.BackColor = Color.White;
        }

        private void cboHostIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHostCode.Text = "";
        }
    }
}
