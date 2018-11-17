using System;
using System.Collections.Generic;

namespace Ranges
{
    public static class Program
    {
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

        //public static int Main(string[] args)
        //{
        //    try
        //    {
        //        List<int> input = new List<int>(args.Length);

        //        Array.ForEach(args, x => input.Add(int.Parse(x)));

        //        Range<int>[] ranges = ConvertToRanges(input.ToArray());

        //        Range<int>[] result = RangesTools.MergeRanges(ranges);

        //        int[] resultArray = AsArray(result);

        //        Array.ForEach(resultArray, x => Console.Write($" {x} "));

        //        Console.WriteLine();

        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);

        //        return 1;
        //    }
        //}
    }
}
