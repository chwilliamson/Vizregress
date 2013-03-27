using NUnit.Framework;

namespace Williamson.VDD.Tests
{
    [TestFixture]
    public class UtilsFixture
    {
        /// <summary>
        /// Gets the Zone where the whole image is red
        /// </summary>
        [Test]
        public void GetZoneTest()
        {
            var result = VDD.Utils.GetBounds(Utils.Load("Zoning.Red"));
            Assert.AreEqual(0, result.X);
            Assert.AreEqual(0, result.Y);
            Assert.AreEqual(20, result.Width);
            Assert.AreEqual(20, result.Height);
        }


        /// <summary>
        /// Gets the Zone where the whole image is red
        /// </summary>
        [Test]
        public void GetZoneTest2()
        {
            var result = VDD.Utils.GetBounds(Utils.Load("Zoning.OverallStatus_Zoned"));
            Assert.AreEqual(621, result.X);
            Assert.AreEqual(328, result.Y);
            Assert.AreEqual(249, result.Width);
            Assert.AreEqual(177, result.Height);
        }
    }
}
