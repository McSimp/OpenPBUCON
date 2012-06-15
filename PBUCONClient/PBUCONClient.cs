using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;

namespace PBUCONClient
{
    class PBUCONClient
    {
        private List<PBUCONServer> servers = new List<PBUCONServer>();
        private IPEndPoint listenEP;
        private UdpClient client;
        private string listenIP;
        private int listenPort;

        public PBUCONClient(string listenIP, int listenPort)
        {
            this.listenPort = listenPort;
            this.listenIP = listenIP;

            this.listenEP = new IPEndPoint(IPAddress.Any, this.listenPort);
            this.client = new UdpClient(this.listenEP);

            this.client.BeginReceive(new AsyncCallback(ReceiveMessage), null);
        }

        public void AddServer(PBUCONServer server)
        {
            server.ServerChallengeChanged += ServerChallengeChange;
            this.servers.Add(server);
        }

        private void ReceiveMessage(IAsyncResult ar)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, this.listenPort); // Temp endpoint
            byte[] receiveBytes = this.client.EndReceive(ar, ref endPoint);

            this.client.BeginReceive(new AsyncCallback(ReceiveMessage), null); // Be ready to receieve again

            // Figure out which server sent the packet
            PBUCONServer sendingServer = null;
            foreach (PBUCONServer server in servers)
            {
                // TODO: Check dis.
                if (server.EndPoint.Equals(endPoint))
                {
                    sendingServer = server;
                    break;
                }
            }

            if (sendingServer != null)
            {
                sendingServer.ProcessPacket(receiveBytes);
            }
        }
        
        // Need this because a heartbeat needs to be sent when the challenge
        // is changed
        private void ServerChallengeChange(Object sender, ServerChallengeChangedEventArgs e)
        {
            PBUCONServer server = sender as PBUCONServer;
            SendHeartbeat(server);
        }

        public void SendHeartbeat(PBUCONServer server)
        {
            byte[] data = server.GetHeartbeatPacket(this.listenIP, this.listenPort);
            client.Send(data, data.Length, server.EndPoint);
        }

        public void SendHeartbeats()
        {
            foreach (PBUCONServer server in servers)
            {
                SendHeartbeat(server);
            }
        }

        public void SendCommand(PBUCONServer server, string command)
        {
            byte[] data = server.GetCommandPacket(command, this.listenIP, this.listenPort);
            client.Send(data, data.Length, server.EndPoint);
        }

        public void BroadcastCommand(string command)
        {
            foreach (PBUCONServer server in servers)
            {
                SendCommand(server, command);
            }
        }
    }
}
