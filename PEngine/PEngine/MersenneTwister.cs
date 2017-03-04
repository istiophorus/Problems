using System;

namespace PEngine
{
    public sealed class MersenneTwister : IRandom
    {
        public const int w = 64;
        public const ulong n = 312;
        public const ulong m = 156;
        public const ulong r = 31;
        public const ulong a = 0xB5026F5AA96619E9;
        public const int u = 29;
        public const ulong d = 0x5555555555555555;
        public const int s = 17;
        public const ulong b = 0x71D67FFFEDA60000;
        public const int t = 37;
        public const ulong c = 0xFFF7EEE000000000;
        public const int l = 43;
        public const ulong f = 6364136223846793005;

        public const ulong lower_mask = 0x7FFFFFFF;
        public const ulong upper_mask = ~lower_mask;

        private ulong[] MT = new ulong[n];
        private ulong index = n + 1;

        public MersenneTwister(ulong seed)
        {
            seed_mt(seed);
        }

        private void seed_mt(ulong seed)
        {
            index = n;
            MT[0] = seed;

            for (ulong i = 1; i < n; ++i)
            {
                MT[i] = (f * (MT[i - 1] ^ (MT[i - 1] >> (w - 2))) + i);
            }
        }

        public Int32 Next(Int32 range)
        {
            Int32 value = Next();

            value = value & 0x7fFFffFF;

            return value % range;
        }

        public Int32 Next()
        {
            if (index >= n)
            {
                if (index > n)
                {
                    throw new Exception("Generator was never seeded");
                }
                twist();
            }

            ulong y = MT[index];
            y = y ^ ((y >> u) & d);
            y = y ^ ((y << s) & b);
            y = y ^ ((y << t) & c);
            y = y ^ (y >> l);

            ++index;

            Int32 result = (Int32)((y & 0xFFffFFffFF) ^ ((y >> 32) & 0xFFffFFffFF));

            return result;
        }

        private void twist()
        {
            for (ulong i = 0; i < n; ++i)
            {
                ulong x = (MT[i] & upper_mask) + (MT[(i + 1) % n] & lower_mask);
                ulong xA = x >> 1;

                if (x % 2 != 0)
                {
                    xA = xA ^ a;
                }

                MT[i] = MT[(i + m) % n] ^ xA;
            }

            index = 0;
        }
    }
}