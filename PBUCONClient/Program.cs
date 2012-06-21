using System;
using System.Net;
using System.IO;

namespace PBUCONClient
{
    /*
    class Program
    {
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

        static void NewMessage(Object sender, UCONMessageEventArgs e)
        {
            PBUCONServer server = sender as PBUCONServer;
            Console.WriteLine("[" + server.Name + "] -> " + e.Message);
        }

        static void ChallengeChange(Object sender, ServerChallengeChangedEventArgs e)
        {
            PBUCONServer server = sender as PBUCONServer;
            Console.WriteLine("[" + server.Name + "] Server challenge now " + e.NewServerChallenge.ToString("X8") + ", was " + e.OldServerChallenge.ToString("X8"));

        }

        static void Main(string[] args)
        {
            // pb_sv_uconadd 1 124.170.53.60 simpuser simppass
            PBUCONServer wake = new PBUCONServer("Wake Rape", "203.46.105.23", 21000, "simpuser", "simppass");
            wake.NewMessage += NewMessage;
            wake.ServerChallengeChanged += ChallengeChange;
            wake.Active = true;
            PBUCONClient test = new PBUCONClient(getExternalIP(), 33333);
            test.AddServer(wake);
            Console.WriteLine("Client challenge: " + wake.ClientChallenge.ToString("X8"));

            while (true)
            {
                string command = Console.ReadLine();
                if (command == "heartbeat")
                {
                    test.SendHeartbeats();
                }
                else if (command == "exit")
                {
                    test.BroadcastCommand("pbuconexit");
                    break;
                }
                else
                {
                    test.BroadcastCommand(command);
                }
                Console.WriteLine("Command was: " + command);
            }
        }
    }
    */
}