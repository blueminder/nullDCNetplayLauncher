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

            cboHostIP.DataSource = new BindingSource(GetIPsByNetwork(), null);
            cboHostIP.DisplayMember = "Value";
            cboHostIP.ValueMember = "Value";

            String hostIP;
            if (GetRadminHostIP() != null)
                hostIP = GetRadminHostIP();
            else if (GetExternalIP() != null)
                hostIP = GetExternalIP();
            else
                hostIP = GetIPsByNetwork().Values.First();
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

        private void btnGuess_Click(object sender, EventArgs e)
        {
            long guessedDelay = Launcher.GuessDelay(txtOpponentIP.Text);
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

        private void btnGenHostCode_Click(object sender, EventArgs e)
        {
            var hostCode = Launcher.GenerateHostCode(cboHostIP.Text,
                                                     txtHostPort.Text,
                                                     Convert.ToInt32(numDelay.Value).ToString(),
                                                     Convert.ToInt32(cboMethod.SelectedValue).ToString());
            txtHostCode.Text = hostCode;
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

        private void cboHostnameIP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary> 
        /// This utility function displays all the IPv4 addresses of the local computer. 
        /// </summary> 
        /// https://blog.stephencleary.com/2009/05/getting-local-ip-addresses.html
        public static Dictionary<String, String> GetIPsByNetwork()
        {
            var IPsByNetwork = new Dictionary<String, String>();

            var externalIP = GetExternalIP();
            if (externalIP != null)
                IPsByNetwork.Add("External", externalIP);

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface network in networkInterfaces)
            {
                IPInterfaceProperties properties = network.GetIPProperties();
                foreach (IPAddressInformation address in properties.UnicastAddresses)
                {
                    if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                        continue;

                    if (IPAddress.IsLoopback(address.Address))
                        continue;

                    IPsByNetwork[network.Name] = address.Address.ToString();
                }
            }

            return IPsByNetwork;
        }

        private static string GetExternalIP()
        {
            try
            {
                string externalIP;
                externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch { return null; }
        }

        public static String GetRadminHostIP()
        {
            String radminHostIP;
            try
            {
                radminHostIP = GetIPsByNetwork()["Radmin VPN"];
            }
            catch
            {
                radminHostIP = null;
            }
            return radminHostIP;
        }

        private void btnGuessAgain_Click(object sender, EventArgs e)
        {
            var RadminHostIP = GetIPsByNetwork()["Radmin VPN"];
            MessageBox.Show(RadminHostIP);
        }

        private void btnExpandCollapse_Click(object sender, EventArgs e)
        {
            if(splitHost.Panel2Collapsed)
            {
                this.Height = splitHost.Panel1.Height + 200;
                
                //splitHost.IsSplitterFixed = false;
                //this.Width = splitHost.MaximumSize.Width;
                //this.Height = splitHost.MaximumSize.Height;
                btnExpandCollapse.Text = "▼                    ▼";
                splitHost.Panel2Collapsed = false;
            }
            else
            {
                this.Height = splitHost.MaximumSize.Height;
                
                //splitHost.IsSplitterFixed = false;
                //this.Width = splitHost.MinimumSize.Width;
                //this.Height = splitHost.MinimumSize.Height;
                //splitHost.Size = splitHost.MaximumSize;
                btnExpandCollapse.Text = "▲                    ▲";
                splitHost.Panel2Collapsed = true;
            }
            
            
        }

    }
}
