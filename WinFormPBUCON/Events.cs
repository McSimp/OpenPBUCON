using System;
using PBUCONClient;

namespace WinFormPBUCON
{
    public class ServerAddedEventArgs : EventArgs
    {
        private readonly PBUCONServer server;
        public PBUCONServer Server
        {
            get { return server; }
        }

        public ServerAddedEventArgs(PBUCONServer server)
        {
            this.server = server;
        }
    }
}
