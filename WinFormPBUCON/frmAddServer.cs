using System;
using System.Net;
using System.Windows.Forms;

namespace WinFormPBUCON
{
    public partial class frmAddServer : Form
    {
        private ServerManager manager;

        public frmAddServer(ServerManager manager)
        {
            InitializeComponent();
            this.manager = manager;
        }

        private void btnAddServer_Click(object sender, EventArgs e)
        {
            ProcessForm();
        }

        private void textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // 13 is enter. TODO: 13 is ugly.
            {
                ProcessForm();
            }
        }

        private void ProcessForm()
        {
            // Validate form
            string name = tbServerName.Text;
            if (name.Length == 0)
            {
                MessageBox.Show("You must enter a server name");
                return;
            }

            string username = tbUsername.Text;
            if (username.Length == 0)
            {
                MessageBox.Show("You must enter a username");
                return;
            }

            string password = mtbPassword.Text;
            if (password.Length == 0)
            {
                MessageBox.Show("You must enter a password");
                return;
            }

            string ip = tbServerIP.Text;
            IPAddress addr;
            if (!IPAddress.TryParse(ip, out addr))
            {
                MessageBox.Show("Invalid IP");
                return;
            }

            int port;
            if (!int.TryParse(tbServerPort.Text, out port))
            {
                MessageBox.Show("Invalid port");
                return;
            }

            // Tell the manager we've got a new server
            manager.AddServer(name, ip, port, username, password);
            this.Close();
        }
    }
}
