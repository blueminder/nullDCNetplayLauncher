using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nullDCNetplayLauncher
{
    public partial class JoinControl : UserControl
    {
        public JoinControl()
        {
            InitializeComponent();
        }

        private void btnLaunchGame_Click(object sender, EventArgs e)
        {
            Launcher.HostInfo decodedHostInfo = Launcher.DecodeHostCode(txtHostCode.Text);
            Launcher.UpdateCFGFile(
                netplayEnabled: true,
                isHost: false,
                hostAddress: decodedHostInfo.IP,
                hostPort: decodedHostInfo.Port,
                frameDelay: decodedHostInfo.Delay);
            Launcher.LaunchNullDC(Launcher.SelectedGame);
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            txtHostCode.Text = Clipboard.GetText();
            MessageBox.Show("Host Code Pasted from Clipboard");
        }

    }
}
