using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace PBUCONClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Testing

            
            String password = "simppass";
            PBCrypt test = new PBCrypt(password);

           //byte[] data = { 0x04, 0x00, 0x27, 0xC3, 0x27, 0xDF, 0x57, 0x5C, 0x58, 0x86, 0xEB, 0xA2, 0xF8, 0x69, 0x56, 0xD1, 0x49, 0xE6, 0x31, 0x90, 0xBA, 0x34 };
            byte[] data = { 0x24, 0x20, 0x2b, 0xe6, 0x6b, 0xa0, 0x6c, 0x58, 0x59, 0xf2 };
            String decrypted = test.Decrypt(data);
            Console.WriteLine(decrypted);
            
            PBUCONServer testServer = new PBUCONServer("Test server", "203.46.105.23", 21000, "simpuser", "simppass");

            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
ProtocolType.Udp);

            IPAddress serverAddr = IPAddress.Parse("203.46.105.23");

            IPEndPoint endPoint = new IPEndPoint(serverAddr, 21000);

            byte[] send_buffer = testServer.GetHeartbeatPacket("124.124.124.124", "27015");


            //Console.WriteLine("[" + testServer.Name + "] Client challenge set to: " + testServer.clientChallenge.ToString("X8") + " aka " + testServer.clientChallenge);

            Console.WriteLine();
            sock.SendTo(send_buffer, endPoint);

            Console.ReadLine();
            
        }
    }
}
