﻿using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PBUCONClient
{
    class PBCrypt
    {
        private const UInt32 STATIC_SEED = 3;

        private UInt32 staticKey;
        private byte[] challengeKey = new byte[16]; // Used in the packet
        public byte[] ChallengeKey
        {
            get { return challengeKey; }
        }
        private String password; // Used to generate keys
        private PBMersenne mt;

        public PBCrypt(String password)
        {
            this.password = password;
            this.staticKey = BitConverter.ToUInt32(GenerateKey(STATIC_SEED), 0);
            this.mt = new PBMersenne();
        }

        public void SetServerChallenge(UInt32 serverChallenge)
        {
            challengeKey = GenerateKey(serverChallenge);
            challengeKey = Encrypt(challengeKey);
        }

        private byte[] GenerateKey(UInt32 seed)
        {
            MD5_CTX mdContext = new MD5_CTX();
            IntPtr ptrPassword = Marshal.StringToHGlobalAnsi(password);

            PBMD5.MD5Init(ref mdContext, seed);
            PBMD5.MD5Update(ref mdContext, ptrPassword, (UInt32)password.Length);
            PBMD5.MD5Final(ref mdContext);

            return mdContext.digest;
        }

        private void InitMersenne()
        {
            mt.Init(staticKey);
        }

        public String Decrypt(byte[] data, int dataOffset = 0)
        {
            InitMersenne();

            StringBuilder sb = new StringBuilder();

            for (int i = dataOffset; i < data.Length; i++)
            {
                byte d = (byte)(data[i] ^ (byte)mt.GetNext());
                sb.Append((char)d); // TODO: Fix this mess
            }

            return sb.ToString();
        }

        public byte[] Encrypt(String data)
        {
            return Encrypt(ASCIIEncoding.ASCII.GetBytes(data));
        }

        public byte[] Encrypt(byte[] data)
        {
            InitMersenne();
            byte[] output = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                byte d = (byte)(data[i] ^ (byte)mt.GetNext());
                output[i] = d;
            }

            return output;
        }
    }
}
