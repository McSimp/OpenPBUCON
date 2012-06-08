using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBUCONClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Testing
            String password = "simppass";
            PBCrypt test = new PBCrypt(password);

            byte[] data = { 0x04, 0x00, 0x27, 0xC3, 0x27, 0xDF, 0x57, 0x5C, 0x58, 0x86, 0xEB, 0xA2, 0xF8, 0x69, 0x56, 0xD1, 0x49, 0xE6, 0x31, 0x90, 0xBA, 0x34 };
            String decrypted = test.decrypt(data);
            Console.WriteLine(decrypted);

            Console.ReadLine();
        }
    }
}
