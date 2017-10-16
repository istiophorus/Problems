using System;

namespace Ranges
{
    public sealed class Range<T> where T : IComparable<T>
    {
        private readonly T _begin;

        private readonly T _end;

        public Range(T begin, T end)
        {
            if (begin.CompareTo(end) == 1)
            {
                throw new ArgumentOutOfRangeException(nameof(end));
            }

            _begin = begin;

            _end = end;
        }

        public T Begin
        {
            get
            {
                return _begin;
            }
        }

        public T End
        {
            get
            {
                return _end;
            }
        }
    }
}
