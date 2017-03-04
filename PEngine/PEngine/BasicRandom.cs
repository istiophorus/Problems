using System;

namespace PEngine
{
    public sealed class BasicRandom : IRandom
    {
        private readonly Random _random;

        public BasicRandom(Int32 seed)
        {
            _random = new Random(seed);
        }

        public Int32 Next()
        {
            return _random.Next();
        }

        public Int32 Next(Int32 range)
        {
            if (range < 2)
            {
                throw new ArgumentOutOfRangeException("range");
            }

            return Math.Abs(_random.Next()) % range;
        }
    }
}
