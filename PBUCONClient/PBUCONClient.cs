using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace PBUCONClient
{
    public class PBUCONClient
    {
        private List<PBUCONServer> servers = new List<PBUCONServer>();
        private IPEndPoint listenEP;
        private UdpClient client;
        private string listenIP;
        private int listenPort;
        private const int heartbeatTime = 30;
        private Timer heartbeatTimer;

        public PBUCONClient(string listenIP, int listenPort)
        {
            this.listenPort = listenPort;
            this.listenIP = listenIP;

            this.listenEP = new IPEndPoint(IPAddress.Any, this.listenPort);
            this.client = new UdpClient(this.listenEP);

            this.client.BeginReceive(new AsyncCallback(ReceiveMessage), null);

            // I think this can be optimised to only send heartbeats if no other packet has been
            // sent to the server in the last 30 (or whatever) seconds, even though it's not
            // implemented in the offical UCON client. This is on my todo when I have a look at 
            // the serverside binaries. It's only a few bytes so doesn't matter too much.
            this.heartbeatTimer = new Timer(heartbeatTime * 1000);
            this.heartbeatTimer.Elapsed += new ElapsedEventHandler(delegate(Object source, ElapsedEventArgs e)
                {
                    SendHeartbeats();
                });
            this.heartbeatTimer.Start();
        }

        public void AddServer(PBUCONServer server)
        {
            server.ServerChallengeChanged += ServerChallengeChange;
            this.servers.Add(server);
            SendHeartbeat(server);
        }

        // This is called by the async UdpClient BeginReceive and will pass off processing of
        // the packet to the server it is destined for. 
        private void ReceiveMessage(IAsyncResult ar)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, this.listenPort); // Temp endpoint
            byte[] receiveBytes = this.client.EndReceive(ar, ref endPoint);

            this.client.BeginReceive(new AsyncCallback(ReceiveMessage), null); // Be ready to receieve again

            // Figure out which server sent the packet
            PBUCONServer sendingServer = null;
            foreach (PBUCONServer server in servers)
            {
                if (server.EndPoint.Equals(endPoint))
                {
                    sendingServer = server;
                    break; // It's how I roll.
                }
            }

            if (sendingServer != null)
            {
                sendingServer.ProcessPacket(receiveBytes);
            }
        }
        
        // Need this because a heartbeat needs to be sent when the challenge is changed
        private void ServerChallengeChange(Object sender, ServerChallengeChangedEventArgs e)
        {
            PBUCONServer server = sender as PBUCONServer;
            SendHeartbeat(server);
        }

        public void SendHeartbeat(PBUCONServer server)
        {
            if (server.Active) // Only send the heartbeat if the server is meant to be connected
            {
                //Console.WriteLine("Sending heartbeat");
                byte[] data = server.GetHeartbeatPacket(this.listenIP, this.listenPort);
                client.Send(data, data.Length, server.EndPoint);
            }
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
            if (server.Active) // Only send the heartbeat if the server is meant to be connected
            {
                byte[] data = server.GetCommandPacket(command, this.listenIP, this.listenPort);
                client.Send(data, data.Length, server.EndPoint);
            }
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
