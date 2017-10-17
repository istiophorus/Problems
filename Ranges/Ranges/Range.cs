using System;

namespace Ranges
{
    public sealed class Range<T> where T : IComparable<T>
    {
        private readonly T _begin;

        private readonly T _end;

        private readonly int _hashCode;

        private readonly string _toString;

        public Range(T begin, T end)
        {
            if (begin.CompareTo(end) == 1)
            {
                throw new ArgumentOutOfRangeException(nameof(end));
            }

            _begin = begin;

            _end = end;

            _hashCode = (((GetType().GetHashCode() << 8) ^ _begin.GetHashCode()) << 8) ^ _end.GetHashCode();

            _toString = $"<{_begin};{_end}>";
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

        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            Range<T> other = obj as Range<T>;

            if (null == other)
            {
                return false;
            }

            return Begin.Equals(other.Begin) && End.Equals(other.End);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override string ToString()
        {
            return _toString;
        }
    }
}


