using System;
using System.Collections.Generic;
using System.Linq;
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

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                manager.StartPBClient(ip, Convert.ToInt32(args[1]));
            }
            else
            {
                Random r = new Random();
                //int port = r.Next(49152, 65535);
                int port = 33333;
                manager.StartPBClient(ip, port);
            }

            Application.Run(manager.MainApp);
        }

        static string getExternalIP()
        {
            string apiURL = "http://automation.whatismyip.com/n09230945.asp";
            WebRequest getReq = WebRequest.Create(apiURL);
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
