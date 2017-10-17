using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ranges.Tests
{
    [TestClass]
    public sealed class CanMergeTests
    {
        private void TestBody(int b1, int e1, int b2, int e2, bool expectedResult)
        {
            Range<int> a = new Range<int>(b1, e1);

            Range<int> b = new Range<int>(b2, e2);

            bool result = RangesTools.CanMerge(a, b);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void CanMergeTest1()
        {
            TestBody(1, 2, 3, 4, false);
        }

        [TestMethod]
        public void CanMergeTest2()
        {
            TestBody(1, 2, 2, 4, true);
        }

        [TestMethod]
        public void CanMergeTest3()
        {
            TestBody(1, 3, 2, 4, true);
        }

        [TestMethod]
        public void CanMergeTest4()
        {
            TestBody(1, 4, 2, 4, true);
        }

        [TestMethod]
        public void CanMergeTest5()
        {
            TestBody(1, 5, 2, 4, true);
        }

        [TestMethod]
        public void CanMergeTest6()
        {
            TestBody(2, 5, 2, 4, true);
        }

        [TestMethod]
        public void CanMergeTest7()
        {
            TestBody(3, 6, 2, 4, true);
        }

        [TestMethod]
        public void CanMergeTest8()
        {
            TestBody(4, 6, 2, 4, true);
        }

        [TestMethod]
        public void CanMergeTest9()
        {
            TestBody(5, 6, 2, 4, false);
        }
    }
}
