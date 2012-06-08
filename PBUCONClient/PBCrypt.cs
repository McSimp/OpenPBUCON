using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PBUCONClient
{
    class PBCrypt
    {
        private const UInt32 STATIC_SEED = 3;

        private UInt32 staticKey;
        private byte[] challengeKey = new byte[16]; // Used in the packet
        private String password; // Used to generate keys
        private PBMersenne mt;

        public PBCrypt(String password)
        {
            this.password = password;
            this.staticKey = BitConverter.ToUInt32(generateKey(STATIC_SEED), 0);
            this.mt = new PBMersenne();
        }

        public void setServerChallenge(UInt32 serverChallenge)
        {
            challengeKey = generateKey(serverChallenge);
            challengeKey = encrypt(challengeKey);
        }

        private byte[] generateKey(UInt32 seed)
        {
            MD5_CTX mdContext = new MD5_CTX();
            IntPtr ptrPassword = Marshal.StringToHGlobalAnsi(password);

            PBMD5.MD5Init(ref mdContext, seed);
            PBMD5.MD5Update(ref mdContext, ptrPassword, (UInt32)password.Length);
            PBMD5.MD5Final(ref mdContext);

            return mdContext.digest;
        }

        private void initMersenne()
        {
            mt.init(staticKey);
        }

        public String decrypt(byte[] data)
        {
            initMersenne();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                byte d = (byte)(data[i] ^ (byte)mt.getNext());
                sb.Append((char)d); // TODO: Fix this mess
            }

            return sb.ToString();
        }

        public byte[] encrypt(String data)
        {
            return encrypt(ASCIIEncoding.ASCII.GetBytes(data));
        }

        public byte[] encrypt(byte[] data)
        {
            initMersenne();
            byte[] output = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                byte d = (byte)(data[i] ^ (byte)mt.getNext());
                output[i] = d;
            }

            return output;
        }
    }
}
