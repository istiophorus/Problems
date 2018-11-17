using System;
using System.Collections.Generic;

namespace RangesTask
{
    public sealed class ListItem<T>
    {
        private readonly ListItem<T> _next;

        public ListItem(ListItem<T> next)
        {
            _next = next;
        }

        public T Data { get; set; }

        public ListItem<T> Next
        {
            get
            {
                return _next;
            }
        }
    }

    public static class ListItemTools
    {
        public static T[] ToArray<T>(this ListItem<T> item)
        {
            if (null == item)
            {
                return new T[0];
            }

            List<T> results = new List<T>();

            while (item != null)
            {
                results.Add(item.Data);

                item = item.Next;
            }

            return results.ToArray();
        }

        public static ListItem<T> ToList<T>(this T[] input)
        {
            if (null == input)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Length <= 0)
            {
                return null;
            }

            ListItem<T> item = null;

            for (int q = input.Length - 1; q >= 0; q--)
            {
                ListItem<T> old = item;

                item = new ListItem<T>(old);
                item.Data = input[q];
            }

            return item;
        }
    }

    public static class Solution
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

        public static bool CanMerge<T>(Range<T> a, Range<T> b) where T : IComparable<T>
        {
            if (null == a)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (null == b)
            {
                throw new ArgumentNullException(nameof(b));
            }

            if (a.End.CompareTo(b.Begin) == -1)
            {
                return false;
            }

            if (b.End.CompareTo(a.Begin) == -1)
            {
                return false;
            }

            return true;
        }

        public static Range<T> MergeRanges<T>(Range<T> a, Range<T> b) where T : IComparable<T>
        {
            if (null == a)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (null == b)
            {
                throw new ArgumentNullException(nameof(b));
            }

            T begin = a.Begin.CompareTo(b.Begin) <= 0 ? a.Begin : b.Begin;

            T end = a.End.CompareTo(b.End) >= 0 ? a.End : b.End;

            return new Range<T>(begin, end);
        }

        public static Range<T>[] MergeRanges<T>(Range<T>[] ranges) where T : IComparable<T>
        {
            if (null == ranges)
            {
                throw new ArgumentNullException(nameof(ranges));
            }

            if (ranges.Length <= 1)
            {
                return ranges;
            }

            Array.Sort(ranges, (a, b) => a.Begin.CompareTo(b.Begin));

            ListItem<Range<T>> rangesListItem = ranges.ToList();

            List<Range<T>> results = new List<Range<T>>();

            while (rangesListItem != null)
            {
                ListItem<Range<T>> current = rangesListItem;

                ListItem<Range<T>> next = current.Next;

                if (null != next)
                {
                    if (CanMerge(current.Data, next.Data))
                    {
                        Range<T> mergedRange = MergeRanges(current.Data, next.Data);

                        next.Data = mergedRange;

                        rangesListItem = next;
                    }
                    else
                    {
                        results.Add(current.Data);

                        rangesListItem = next;
                    }
                }
                else
                {
                    results.Add(current.Data);

                    rangesListItem = next;
                }
            }

            return results.ToArray();
        }

        private static Range<T>[] ConvertToRanges<T>(T[] input) where T : IComparable<T>
        {
            if (null == input)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if ((input.Length & 1) != 0)
            {
                throw new ArgumentException("Even amount of items has benn expected");
            }

            List<Range<T>> result = new List<Range<T>>(input.Length / 2);

            for (int q = 0, maxQ = input.Length; q < maxQ; q += 2)
            {
                result.Add(new Range<T>(input[q], input[q + 1]));
            }

            return result.ToArray();
        }

        private static T[] AsArray<T>(Range<T>[] input) where T : IComparable<T>
        {
            if (null == input)
            {
                throw new ArgumentNullException(nameof(input));
            }

            List<T> result = new List<T>(input.Length * 2);

            Array.ForEach(input, r =>
            {
                result.Add(r.Begin);
                result.Add(r.End);
            });

            return result.ToArray();
        }

        private static void Solve(int[] input)
        {
            

            Range<int>[] ranges = ConvertToRanges(input);

            Range<int>[] result = MergeRanges(ranges);

            int[] resultArray = AsArray(result);

            Array.ForEach(resultArray, x => Console.Write($" {x} "));

            Console.WriteLine();
        }

        public static int Main(string[] args)
        {
            try
            {
                Solve(new int[] { 1, 3, 8, 10, 2, 6, 15, 18 });

                Solve(new int[] { 1, 3, 2, Int32.MaxValue, 2, 7, 15, 18 });

                Solve(new int[] { Int32.MinValue, -1, 1, Int32.MaxValue });

                Solve(new int[] { Int32.MinValue, 0, 0, Int32.MaxValue });

                Solve(new int[] { 1, 3, 2, 6, 5, 8 });

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return 1;
            }
        }
    }
}


