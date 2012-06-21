using System.Net;
using System.Windows.Forms;

namespace WinFormPBUCON
{
    public partial class frmAddServer : Form
    {
        private ServerManager manager;

        public frmAddServer(ServerManager manager)
        {
            this.manager = manager;
            InitializeComponent();
        }

        private void btnAddServer_Click(object sender, System.EventArgs e)
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

            manager.AddServer(name, ip, port, username, password);
            this.Close();
        }
    }
}
