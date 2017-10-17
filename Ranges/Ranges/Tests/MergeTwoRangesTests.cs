using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ranges.Tests
{
    [TestClass]
    public sealed class MergeTwoRangesTests
    {
        private void TestBody(int b1, int e1, int b2, int e2, int eb, int ee)
        {
            Range<int> a = new Range<int>(b1, e1);

            Range<int> b = new Range<int>(b2, e2);

            Range<int> result = RangesTools.MergeRanges(a, b);

            Assert.AreEqual(eb, result.Begin);

            Assert.AreEqual(ee, result.End);
        }

        [TestMethod]
        public void MergeTwoRanges2()
        {
            TestBody(1, 2, 2, 4, 1, 4);
        }

        [TestMethod]
        public void MergeTwoRanges3()
        {
            TestBody(1, 3, 2, 4, 1, 4);
        }

        [TestMethod]
        public void MergeTwoRanges4()
        {
            TestBody(1, 4, 2, 4, 1, 4);
        }

        [TestMethod]
        public void MergeTwoRanges5()
        {
            TestBody(1, 5, 2, 4, 1, 5);
        }

        [TestMethod]
        public void MergeTwoRanges6()
        {
            TestBody(2, 5, 2, 4, 2, 5);
        }

        [TestMethod]
        public void MergeTwoRanges7()
        {
            TestBody(3, 6, 2, 4, 2, 6);
        }

        [TestMethod]
        public void MergeTwoRanges8()
        {
            TestBody(4, 6, 2, 4, 2, 6);
        }
    }
}
