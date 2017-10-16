using System;
using System.Collections.Generic;
using System.Linq;

namespace Ranges
{
    public static class RangesTools
    {
        public static int? BinarySearchArray<T>(Range<T>[] input, int startIndex, int length, Range<T> pattern) where T : IComparable<T>
        {
            if (startIndex <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            if (length <= 0)
            {
                return null;
            }

            if (startIndex + length > input.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            if (input[input.Length - 1].Begin.CompareTo(pattern.End) <= 0)
            {
                return null;
            }

            int secondTestIndex = (startIndex * 2 + length) / 2;

            int testIndex = secondTestIndex - 1;

            int resultTest = input[testIndex].Begin.CompareTo(pattern.End);

            int resultSecondTest = input[secondTestIndex].Begin.CompareTo(pattern.End);

            if (resultSecondTest <= 0)
            {
                if (length == 1)
                {
                    return null;
                }
                else
                {
                    return BinarySearchArray<T>(input, secondTestIndex, startIndex + length - 1 - secondTestIndex + 1, pattern);
                }
            }

            if (resultTest <= 0 && resultSecondTest == 1)
            {
                return secondTestIndex;
            }

            if (resultTest == 1)
            {
                if (length == 1)
                {
                    return testIndex;
                }
                else
                {
                    return BinarySearchArray<T>(input, startIndex, testIndex - startIndex + 1, pattern);
                }
            }

            throw new ApplicationException("Nobody expected spanish inquisiton ... and this case");
        }

        public static Tuple<Range<T>, Range<T>>[] GetSeparableRanges<T>(IEnumerable<Range<T>> input) where T : IComparable<T>
        {
            if (null == input)
            {
                throw new ArgumentNullException(nameof(input));
            }

            Range<T>[] ascendingBegin = input.ToArray();

            Array.Sort(ascendingBegin, (a, b) => a.Begin.CompareTo(b.Begin));

            List<Tuple<Range<T>, Range<T>>> result = new List<Tuple<Range<T>, Range<T>>>();

            for (int q = 0, maxQ = ascendingBegin.Length - 1; q < maxQ; q++)
            {
                Range<T> a = ascendingBegin[q];

                int? res = BinarySearchArray(ascendingBegin, q + 1, ascendingBegin.Length - q - 1, a);

                if (res.HasValue)
                {
                    for (int w = res.Value; w < ascendingBegin.Length; w++)
                    {
                        result.Add(new Tuple<Range<T>, Range<T>>(a, ascendingBegin[w]));
                    }
                }
            }

            return result.ToArray();
        }
    }
}
