using System;

namespace PBUCONClient
{
    class UCONMessageEventArgs : EventArgs
    {
        private readonly string message;
        public string Message 
        { 
            get { return message; }
        }

        public UCONMessageEventArgs(string message)
        {
            this.message = message;
        }
    }

    class ServerChallengeChangedEventArgs : EventArgs
    {
        private readonly UInt32 newServerChg;
        public UInt32 NewServerChallenge 
        { 
            get { return newServerChg; } 
        }

        private readonly UInt32 oldServerChg;
        public UInt32 OldServerChallenge 
        { 
            get { return oldServerChg; } 
        }

        public ServerChallengeChangedEventArgs(UInt32 newServerChallenge, UInt32 oldServerChallenge)
        {
            this.newServerChg = newServerChallenge;
            this.oldServerChg = oldServerChallenge;
        }
    }
}
