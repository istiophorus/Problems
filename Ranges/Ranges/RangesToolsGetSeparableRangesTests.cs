using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Ranges
{
    [TestClass]
    public sealed class RangesToolsGetSeparableRangesTests
    {
        [TestMethod]
        public void GetSeparableRangesTestBasicList()
        {
            List<Range<int>> data = new List<Range<int>>();

            data.Add(new Range<int>(0, 0));

            for (int q = 1; q < 100; q++)
            {
                data.Add(new Range<int>(q, q));

                Tuple<Range<int>, Range<int>>[] result = RangesTools.GetSeparableRanges(data.ToArray());

                Assert.IsNotNull(result);

                int expectedValue = (data.Count - 1) * data.Count / 2;

                Assert.AreEqual(expectedValue, result.Length);
            }
        }

        [TestMethod]
        public void GetSeparableRangesTest1()
        {
            Range<int>[] ranges = new[]
            {
                new Range<int>(1, 2),
                new Range<int>(2, 3),
                new Range<int>(3, 5),
                new Range<int>(4, 5),
                new Range<int>(5, 6)
            };

            Tuple<Range<int>, Range<int>>[] result = RangesTools.GetSeparableRanges(ranges);

            Assert.IsNotNull(result);

            Assert.AreEqual(5, result.Length);
        }
    }
}
