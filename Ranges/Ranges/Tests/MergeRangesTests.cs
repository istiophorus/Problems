using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranges.Tests
{
    [TestClass]
    public sealed class MergeRangesTests
    {
        [TestMethod]
        public void Test1()
        {
            Range<int>[] ranges = new Range<int>[]
            {
                new Range<int>(2, 2),
                new Range<int>(3, 3),
                new Range<int>(1, 1),
            };

            Range<int>[] result = RangesTools.MergeRanges(ranges);

            Assert.AreEqual(3, result.Length);
        }

        [TestMethod]
        public void Test2()
        {
            Range<int>[] ranges = new Range<int>[]
            {
                new Range<int>(1, 3),
                new Range<int>(8, 10),
                new Range<int>(2, 6),
                new Range<int>(15, 18)
            };

            Range<int>[] result = RangesTools.MergeRanges(ranges);

            Range<int>[] expected = new Range<int>[]
            {
                new Range<int>(1, 6),
                new Range<int>(8, 10),
                new Range<int>(15, 18)
            };

            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void Test8()
        {
            Range<int>[] ranges = new Range<int>[]
            {
                new Range<int>(0, 1),
                new Range<int>(-1, 0),
                new Range<int>(Int32.MinValue, -1),
                new Range<int>(1, Int32.MaxValue)
            };

            Range<int>[] result = RangesTools.MergeRanges(ranges);

            Range<int>[] expected = new Range<int>[]
            {
                new Range<int>(Int32.MinValue, Int32.MaxValue)
            };

            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void Test7()
        {
            Range<int>[] ranges = new Range<int>[]
            {
                new Range<int>(0, 1),
                new Range<int>(-1, 0),
                new Range<int>(-2, -1),
                new Range<int>(5, Int32.MaxValue)
            };

            Range<int>[] result = RangesTools.MergeRanges(ranges);

            Range<int>[] expected = new Range<int>[]
            {
                new Range<int>(-2, 1),
                new Range<int>(5, Int32.MaxValue)
            };

            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void Test6()
        {
            Range<int>[] ranges = new Range<int>[]
            {
                new Range<int>(0, 0),
                new Range<int>(-1, 1),
                new Range<int>(-2, 2),
            };

            Range<int>[] result = RangesTools.MergeRanges(ranges);

            Range<int>[] expected = new Range<int>[]
            {
                new Range<int>(-2, 2)
            };

            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void Test5()
        {
            Range<int>[] ranges = new Range<int>[]
            {
                new Range<int>(0, 0),
                new Range<int>(0, 0),
                new Range<int>(0, 0),
            };

            Range<int>[] result = RangesTools.MergeRanges(ranges);

            Range<int>[] expected = new Range<int>[]
            {
                new Range<int>(0, 0)
            };

            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void Test4()
        {
            Range<int>[] ranges = new Range<int>[]
            {
                new Range<int>(15, 18)
            };

            Range<int>[] result = RangesTools.MergeRanges(ranges);

            Range<int>[] expected = new Range<int>[]
            {
                new Range<int>(15, 18)
            };

            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void Test3()
        {
            Range<int>[] ranges = new Range<int>[]
            {
                new Range<int>(15, 18),
                new Range<int>(4, 9),
                new Range<int>(1, 3),
                new Range<int>(1, 5),
                new Range<int>(Int32.MinValue, 5),
            };

            Range<int>[] result = RangesTools.MergeRanges(ranges);

            Range<int>[] expected = new Range<int>[]
            {
                new Range<int>(Int32.MinValue, 9),
                new Range<int>(15, 18)
            };

            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}


