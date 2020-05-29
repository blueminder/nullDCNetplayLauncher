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
    public partial class HostControl : UserControl
    {
        public HostControl()
        {
            InitializeComponent();
        }

        private void btnGuess_Click(object sender, EventArgs e)
        {
            long guessedDelay = Launcher.GuessDelay(txtGuestIP.Text);
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
            var hostCode = Launcher.GenerateHostCode(txtHostIP.Text,
                                                     txtHostPort.Text,
                                                     Convert.ToInt32(numDelay.Value).ToString());
            txtHostCode.Text = hostCode;
        }

        private void btnLaunchGame_Click(object sender, EventArgs e)
        {
            Launcher.UpdateCFGFile(
                netplayEnabled: true,
                isHost: true,
                hostAddress: txtHostIP.Text,
                hostPort: txtHostPort.Text,
                frameDelay: Convert.ToInt32(numDelay.Value)
                                   .ToString());
            Launcher.LaunchNullDC(Launcher.SelectedGame, isHost: true);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtHostCode.Text);
            MessageBox.Show("Host Code Copied to Clipboard");
        }

    }
}
