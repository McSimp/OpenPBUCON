using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace PBUCONClient
{
    class PBUCONServer
    {
        private UInt32 clientChallenge = 0;
        public UInt32 ClientChallenge
        {
            get { return clientChallenge; }
        }
        private UInt32 serverChallenge = 0;
        public UInt32 ServerChallenge
        {
            get { return serverChallenge; }
        }
        private void SetServerChallenge(UInt32 chg)
        {
            this.serverChallenge = chg;
            crypt.SetServerChallenge(chg);
        }

        private String username;
        private String password;
        private String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private IPEndPoint endPoint;
        public IPEndPoint EndPoint
        {
            get { return endPoint; }
        }
        private void SetIPAddress(string ip, int port)
        {
            this.endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        private PBCrypt crypt;

        public event EventHandler<UCONMessageEventArgs> NewMessage;
        public event EventHandler<ServerChallengeChangedEventArgs> ServerChallengeChanged;

        public PBUCONServer(string name, string ip, int port, string username, string password)
        {
            Init(name, ip, port, username, password);
        }

        public PBUCONServer(string ip, int port, string username, string password)
        {
            Init(ip + ":" + port, ip, port, username, password);
        }

        private void Init(string name, string ip, int port, string username, string password)
        {
            SetIPAddress(ip, port);
            crypt = new PBCrypt(password);

            this.Name = name;
            this.username = username;
            this.password = password;

            Random random = new Random();
            this.clientChallenge = (UInt32)random.Next(int.MinValue, int.MaxValue);
        }

        private List<byte> GetBaseSendPacket(string ip, int port)
        {
            //int packetLength = ip.Length + port.Length + this.username.Length + 35;

            byte[] header = { 255, 255, 255, 255, 80, 66, 95, 85 }; // FF FF FF FF PB_U

            byte[] clientChallengeData = BitConverter.GetBytes(this.clientChallenge);
            byte[] serverChallengeData = BitConverter.GetBytes(this.serverChallenge);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(clientChallengeData);
                Array.Reverse(serverChallengeData);
            }

            List<byte> packetData = new List<byte>();
            packetData.AddRange(header);
            packetData.AddRange(clientChallengeData);
            packetData.AddRange(serverChallengeData);
            packetData.AddRange(this.crypt.ChallengeKey);

            ASCIIEncoding encoding = new ASCIIEncoding();
            packetData.AddRange(encoding.GetBytes(ip + ":" + port.ToString() + ";"));
            packetData.AddRange(encoding.GetBytes(this.username + ";"));

            return packetData;
        }

        // IP and port are YOUR ip and port
        public byte[] GetCommandPacket(string command, string ip, int port)
        {
            List<byte> basePacket = GetBaseSendPacket(ip, port);
            basePacket.AddRange(crypt.Encrypt(command));

            return basePacket.ToArray();
        }

        // IP and port are YOUR ip and port
        public byte[] GetHeartbeatPacket(string ip, int port)
        {
            List<byte> basePacket = GetBaseSendPacket(ip, port);
            basePacket.Add(0x00);

            return basePacket.ToArray();
        }

        private void OnNewMessage(string message)
        {
            UCONMessageEventArgs e = new UCONMessageEventArgs(message);
            EventArgExtensions.Raise<UCONMessageEventArgs>(e, this, ref NewMessage);
        }

        private void OnServerChallengeChanged(UInt32 newChallenge)
        {
            UInt32 oldChallenge = this.serverChallenge;
            SetServerChallenge(newChallenge);

            ServerChallengeChangedEventArgs e = new ServerChallengeChangedEventArgs(newChallenge, oldChallenge);
            EventArgExtensions.Raise<ServerChallengeChangedEventArgs>(e, this, ref ServerChallengeChanged);
        }

        // Horrible hack. Stupid endianness.
        private UInt32 ReverseBytes(UInt32 value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                   (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }

        // This is hacked together just to get a working example
        public void ProcessPacket(byte[] data)
        {
            // Ensure the packet is big enough
            if (data.Length < 8)
            {
                Console.WriteLine("Short packet from " + this.Name);
                return;
            }

            UInt32 pktClientChallenge = BitConverter.ToUInt32(data, 0);
            UInt32 pktServerChallenge = BitConverter.ToUInt32(data, 4);
            if (BitConverter.IsLittleEndian)
            {
                // Reverse
                pktClientChallenge = ReverseBytes(pktClientChallenge);
                pktServerChallenge = ReverseBytes(pktServerChallenge);
            }

            if (pktClientChallenge != this.clientChallenge)
            {
                Console.WriteLine("Invalid client challenge from " + this.Name + " (Is " + pktClientChallenge.ToString("X8") + ", should be " + this.clientChallenge.ToString("X8") + ")");
                return;
            }

            if (pktServerChallenge != this.serverChallenge)
            {
                OnServerChallengeChanged(pktServerChallenge);
            }

            // This packet is only a heartbeat, screw further processing
            if (data.Length < 10 && data[8] == 84)
            {
                return;
            }

            // Decrypt data and raise event
            string message = crypt.Decrypt(data, 8);
            OnNewMessage(message);
        }
    }
}
