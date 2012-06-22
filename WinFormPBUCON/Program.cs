using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace WinFormPBUCON
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ServerManager manager = new ServerManager();

            // Get external IP
            string ip;
            try
            {
                ip = getExternalIP();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: Could not determine external IP. Either you're not connected to the internet or whatismyip.com is blocked.");
                Application.Exit();
                return;
            }

            // Decide on a port
            KeyValueConfigurationCollection appSettings = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.Settings;

            int port;
            if (appSettings["ListenPort"] != null)
            {
                port = Convert.ToInt32(appSettings["ListenPort"].Value);
            }
            else
            {
                Random r = new Random();
                port = r.Next(49152, 65535);
            }

            manager.StartPBClient(ip, port);
            manager.MainApp.Text += " [IP: " + ip + " - Listen port: " + port + "]";

            // Add servers from config
            int numServers = Convert.ToInt32(appSettings["NumServers"].Value);
            for (int i = 1; i <= numServers; i++)
            {
                string setting = appSettings["Server" + i].Value;
                string[] settings = setting.Split(';');
                if (settings.Length == 5)
                {
                    manager.AddServer(settings[0], settings[1], Convert.ToInt32(settings[2]), settings[3], settings[4]);
                }
            }

            Application.Run(manager.MainApp);
        }

        static string getExternalIP()
        {
            string apiURL = "http://automation.whatismyip.com/n09230945.asp";
            WebRequest getReq = WebRequest.Create(apiURL);
            getReq.Timeout = 10000;
            Stream responseStream = getReq.GetResponse().GetResponseStream();
            StreamReader responseReader = new StreamReader(responseStream);

            string ip = responseReader.ReadToEnd();
            IPAddress addr;
            if (!IPAddress.TryParse(ip, out addr))
            {
                throw new Exception("Failed to get external IP");
            }

            return ip;
        }
    }
}
