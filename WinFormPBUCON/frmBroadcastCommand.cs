using System;
using System.Windows.Forms;

namespace WinFormPBUCON
{
    public partial class frmBroadcastCommand : Form
    {
        private ServerManager manager;

        public frmBroadcastCommand(ServerManager manager)
        {
            InitializeComponent();
            this.manager = manager;
        }

        private void btnSendCommand_Click(object sender, EventArgs e)
        {
            RunCommand();
        }

        private void tbCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // 13 is enter. TODO: 13 is ugly.
            {
                RunCommand();
            }
        }

        private void RunCommand()
        {
            if (tbCommand.Text.Length == 0)
            {
                MessageBox.Show("You must enter a command");
                return;
            }

            this.manager.BroadcastCommand(tbCommand.Text);
            this.Close();
        }
    }
}
