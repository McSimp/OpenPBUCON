// Lovingly crafted from http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/VERSIONS/C-LANG/980409/mt19937-2.c
using System;

namespace PBUCONClient
{
    class PBMersenne
    {
        private const int N = 624;
        private const int M = 397;
        private const UInt32 MATRIX_A = 0x9908B0DF;
        private const UInt32 UPPER_MASK = 0x80000000;
        private const UInt32 LOWER_MASK = 0x7FFFFFFF;
        private const UInt32 TEMPERING_MASK_B = 0x9d2c5680;
        private const UInt32 TEMPERING_MASK_C = 0xefc60000;

        private UInt32[] mt = new UInt32[N];
        private int mti = N + 1;

        private bool initialState = false; // If a new set of numbers had to be generated, this will be true.

        private static UInt32 TEMPERING_SHIFT_U(UInt32 y)
        {
            return (y >> 11);
        }

        private static UInt32 TEMPERING_SHIFT_S(UInt32 y)
        {
            return (y << 7);
        }

        private static UInt32 TEMPERING_SHIFT_T(UInt32 y)
        {
            return (y << 15);
        }

        private static UInt32 TEMPERING_SHIFT_L(UInt32 y)
        {
            return (y >> 18);
        }

        public void init(UInt32 seed)
        {
            if (!initialState)
            {
                mt[0] = seed;
                for (mti = 1; mti < N; mti++)
                {
                    mt[mti] = 69069 * mt[mti - 1];
                }

                generate_numbers();
                initialState = true;
            }
            else
            {
                mti = 0;
            }
        }

        private void generate_numbers()
        {
            UInt32 y;
            UInt32[] mag01 = { 0x0, MATRIX_A };
            int kk = 0;
            for (; kk < N - M; kk++)
            {
                y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                mt[kk] = mt[kk + M] ^ (y >> 1) ^ mag01[y & 0x1];
            }

            for (; kk < N - 1; kk++)
            {
                y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                mt[kk] = mt[kk + (M - N)] ^ (y >> 1) ^ mag01[y & 0x1];
            }

            y = (mt[N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
            mt[N - 1] = mt[M - 1] ^ (y >> 1) ^ mag01[y & 0x1];

            mti = 0;
        }

        // This shouldn't really be called 
        public UInt32 getNext()
        {
            if (mti >= N)
            {
                if (mti == N + 1) // No seed set
                {
                    // This might seem dumb, but Punkbuster still has it.
                    init(4357);
                }
                generate_numbers();
                initialState = false;
            }

            UInt32 y = mt[mti++];
            y ^= TEMPERING_SHIFT_U(y);
            y ^= TEMPERING_SHIFT_S(y) & TEMPERING_MASK_B;
            y ^= TEMPERING_SHIFT_T(y) & TEMPERING_MASK_C;
            y ^= TEMPERING_SHIFT_L(y);

            return y;
        }
    }
}
