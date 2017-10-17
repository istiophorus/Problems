using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ranges
{
    [TestClass]
    public sealed class ListitemTests
    {
        [TestMethod]
        public void ArrayToListTest()
        {
            int[] arr = new int[] { 1, 2, 3, 4 };

            ListItem<int> item = arr.ToList();

            int[] restored = item.ToArray();

            CollectionAssert.AreEqual(arr, restored);
        }

        [TestMethod]
        public void EmptyArrayToListTest()
        {
            int[] arr = new int[0];

            ListItem<int> item = arr.ToList();

            int[] restored = item.ToArray();

            CollectionAssert.AreEqual(arr, restored);
        }
    }
}
