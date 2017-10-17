using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ranges
{
    [TestClass]
    public sealed class RangesToolsBinarySearchTests
    {
        [TestMethod]
        public void TestSearch1()
        {
            Range<int>[] ranges = new[]
            {
                new Range<int>(1, 5),
                new Range<int>(2, 3),
                new Range<int>(5, 6),
                new Range<int>(6, 7)
            };

            Array.Sort(ranges, (a, b) => a.Begin.CompareTo(b.Begin));

            int? result = RangesTools.BinarySearchArray(ranges, 1, 3, ranges[0]);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual(3, result.Value);
        }

        [TestMethod]
        public void TestSearch2()
        {
            Range<int>[] ranges = new[]
            {
                new Range<int>(1, 5),
                new Range<int>(2, 3),
                new Range<int>(5, 6),
                new Range<int>(6, 7)
            };

            Array.Sort(ranges, (a, b) => a.Begin.CompareTo(b.Begin));

            int? result = RangesTools.BinarySearchArray(ranges, 2, 2, ranges[1]);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual(2, result.Value);
        }

        [TestMethod]
        public void TestSearch3()
        {
            Range<int>[] ranges = new[]
            {
                new Range<int>(1, 5),
                new Range<int>(2, 3)
            };

            Array.Sort(ranges, (a, b) => a.Begin.CompareTo(b.Begin));

            int? result = RangesTools.BinarySearchArray(ranges, 1, 1, ranges[1]);

            Assert.IsFalse(result.HasValue);
        }

        [TestMethod]
        public void TestSearch5()
        {
            Range<int>[] ranges = new[]
            {
                new Range<int>(1, 5),
                new Range<int>(2, 3)
            };

            Array.Sort(ranges, (a, b) => a.Begin.CompareTo(b.Begin));

            int? result = RangesTools.BinarySearchArray(ranges, 1, 1, ranges[0]);

            Assert.IsFalse(result.HasValue);
        }

        [TestMethod]
        public void TestSearch4()
        {
            Range<int>[] ranges = new[]
            {
                new Range<int>(1, 5)
            };

            Array.Sort(ranges, (a, b) => a.Begin.CompareTo(b.Begin));

            int? result = RangesTools.BinarySearchArray(ranges, 1, 0, ranges[0]);

            Assert.IsFalse(result.HasValue);
        }

        [TestMethod]
        public void TestSearch6()
        {
            Range<int>[] ranges = new[]
            {
                new Range<int>(1, 2),
                new Range<int>(2, 3),
                new Range<int>(4, 5),
                new Range<int>(5, 6)
            };

            Array.Sort(ranges, (a, b) => a.Begin.CompareTo(b.Begin));

            int? result = RangesTools.BinarySearchArray(ranges, 3, 1, ranges[2]);

            Assert.IsFalse(result.HasValue);
        }

        [TestMethod]
        public void TestSearch7()
        {
            Range<int>[] ranges = new[]
            {
                new Range<int>(1, 2),
                new Range<int>(2, 3),
                new Range<int>(4, 5),
                new Range<int>(5, 6)
            };

            Array.Sort(ranges, (a, b) => a.Begin.CompareTo(b.Begin));

            int? result = RangesTools.BinarySearchArray(ranges, 4, 0, ranges[3]);

            Assert.IsFalse(result.HasValue);
        }


        [TestMethod]
        public void TestSearch8()
        {
            Range<int>[] ranges = new[]
            {
                new Range<int>(1, 2),
                new Range<int>(2, 3),
                new Range<int>(2, 5),
                new Range<int>(4, 5),
                new Range<int>(5, 6)
            };

            Array.Sort(ranges, (a, b) => a.Begin.CompareTo(b.Begin));

            int? result = RangesTools.BinarySearchArray(ranges, 2, 3, ranges[1]);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual(3, result.Value);
        }

        [TestMethod]
        public void TestSearch9()
        {
            Range<int>[] ranges = new[]
            {
                new Range<int>(1, 2),
                new Range<int>(2, 3),
                new Range<int>(3, 5),
                new Range<int>(4, 5),
                new Range<int>(5, 6)
            };

            Array.Sort(ranges, (a, b) => a.Begin.CompareTo(b.Begin));

            int? result = RangesTools.BinarySearchArray(ranges, 1, 4, ranges[0]);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual(2, result.Value);
        }
    }
}

