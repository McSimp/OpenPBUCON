using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace PBUCONClient
{
    class PBUCONServer
    {
        private UInt32 clientChallenge = 0;
        private UInt32 serverChallenge = 0;

        private String username;
        private String password;
        private String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private IPEndPoint ep;
        private void setIPAddress(string ip, int port)
        {
            this.ep = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        private PBCrypt crypt;

        public PBUCONServer(string ip, int port, string username, string password)
        {
            init(ip + ":" + port, ip, port, username, password);
        }

        public PBUCONServer(string name, string ip, int port, string username, string password)
        {
            init(name, ip, port, username, password);
        }

        private void init(string name, string ip, int port, string username, string password)
        {
            setIPAddress(ip, port);
            crypt = new PBCrypt(password);

            this.Name = name;
            this.username = username;
            this.password = password;

            Random random = new Random();
            this.clientChallenge = (UInt32)random.Next(int.MinValue, int.MaxValue);

            Console.WriteLine("Client challenge set to: " + this.clientChallenge.ToString("X8"));
        }
    }
}
