using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using PBUCONClient;

namespace WinFormPBUCON
{
    public class ServerManager
    {
        private PBUCONClient.PBUCONClient client;
        public event EventHandler<ServerAddedEventArgs> ServerAdded;
        public frmMainApp MainApp;

        // TODO: Fix hack
        public bool AutoScroll
        {
            get
            {
                if (MainApp == null)
                {
                    return false;
                }

                return MainApp.autoScrollToolStripMenuItem.Checked;
            }
        }

        public ServerManager()
        {
            this.MainApp = new frmMainApp(this);
        }

        public void StartPBClient(string ip, int port)
        {
            this.client = new PBUCONClient.PBUCONClient(ip, port);
            AddServer("Wake", "203.46.105.23", 21000, "simpuser", "simppass");
            //AddServer("Karkand", "203.46.105.23", 20300, "simpuser", "simppass");
        }

        public void AddServerDialog()
        {
            Form frmAddServer = new frmAddServer(this);
            frmAddServer.ShowDialog();
        }

        public void AddServer(string name, string ip, int port, string username, string password)
        {
            PBUCONServer server = new PBUCONServer(name, ip, port, username, password);
            AddServer(server);
        }

        public void AddServer(PBUCONServer server)
        {
            server.Active = true;
            this.client.AddServer(server);
            ServerAddedEventArgs e = new ServerAddedEventArgs(server);
            EventArgExtensions.Raise <ServerAddedEventArgs>(e, this, ref ServerAdded);
        }

        public void BroadcastCommand(string command)
        {
            this.client.BroadcastCommand(command);
        }

        public void SendCommand(PBUCONServer server, string command)
        {
            this.client.SendCommand(server, command);
        }
    }
}
