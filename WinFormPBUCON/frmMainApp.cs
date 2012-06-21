using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormPBUCON
{
    public partial class frmMainApp : Form
    {
        private ServerManager manager;
        private List<ServerTab> tabs;

        public frmMainApp(ServerManager manager)
        {
            InitializeComponent();
            manager.ServerAdded += ServerAdded;
            this.manager = manager;
            this.tabs = new List<ServerTab>();
        }

        private void ServerAdded(Object sender, ServerAddedEventArgs e)
        {
            // Create a new tab
            ServerTab serverTab = new ServerTab(this.manager, e.Server);

            // Add the tab to the tablist
            this.tabs.Add(serverTab);
            this.serverTabs.Controls.Add(serverTab.Tab);
        }

        private void addServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.manager.AddServerDialog();
        }

        private void broadcastCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Create form and broadcast message
        }

        private void frmMainApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.manager.BroadcastCommand("pbuconexit");
        }
    }
}
