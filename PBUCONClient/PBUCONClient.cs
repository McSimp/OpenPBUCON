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
                // Process packet
                sendingServer.ProcessPacket(receiveBytes);
            }
        }

        public void SendHeartbeats()
        {
            // Foreach server, construct the packet and send to that server's endpoint.
            foreach (PBUCONServer server in servers)
            {
                byte[] data = server.GetHeartbeatPacket(this.listenIP, this.listenPort);
                client.Send(data, data.Length, server.EndPoint);
            }
        }
    }
}
