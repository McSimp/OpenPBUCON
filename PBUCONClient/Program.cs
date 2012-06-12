using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace PBUCONClient
{
    class Program
    {
        static string getExternalIP()
        {
            string api_url = "http://automation.whatismyip.com/n09230945.asp";
            WebRequest getReq = WebRequest.Create(api_url);
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

        static void NewMessage(Object sender, UCONMessageEventArgs e)
        {
            PBUCONServer server = sender as PBUCONServer;
            Console.WriteLine(server.Name + " -> " + e.Message);
        }

        static void Main(string[] args)
        {
            // pb_sv_uconadd 1 124.170.53.60 simpuser simppass
            PBUCONServer wake = new PBUCONServer("Wake Rape", "203.46.105.23", 21000, "simpuser", "simppass");
            wake.NewMessage += NewMessage;
            PBUCONClient test = new PBUCONClient(getExternalIP(), 33333);
            test.AddServer(wake);

            while (true)
            {
                string command = Console.ReadLine();
                if (command == "heartbeat")
                {
                    test.SendHeartbeats();
                }
                Console.WriteLine("Command was: " + command);
            }
        }
    }
}
