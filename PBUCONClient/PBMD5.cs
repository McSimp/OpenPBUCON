using System;
using System.Runtime.InteropServices;

namespace PBUCONClient
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct MD5_CTX
    {
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
        public uint[] i;

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
        public uint[] buf;

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] @in;

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] digest; 
    }

    static class PBMD5
    {
        [DllImport("PBMD5.dll")]
        public static extern void MD5Init(ref MD5_CTX mdContext, UInt32 pseudoRandomNumber = 0);

        [DllImport("PBMD5.dll")]
        public static extern void MD5Update(ref MD5_CTX mdContext, IntPtr inBuf, UInt32 inLen);

        [DllImport("PBMD5.dll")]
        public static extern void MD5Final(ref MD5_CTX mdContext);
    }
}