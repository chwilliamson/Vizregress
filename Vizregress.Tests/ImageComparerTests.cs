using NUnit.Framework;

namespace Vizregress.Tests
{
    /// <summary>
    /// My fixture
    /// </summary>
    [TestFixture]
    public class ImageComparerTests
    {
        
        /// <summary>
        /// Performs a test checking whether or not two images match
        /// </summary>
        [Test]
        [TestCase("Google.Home","Google.Home",true,TestName="Same Images")]
        [TestCase("Google.Home", "Google.Home.LogoMoved",false, TestName = "Logo To left")]
        [TestCase("Google.Home", "Google.Home.SearchButtonBlur", false, TestName = "Search Button Blurred")]
        [TestCase("Google.Home", "Google.Home.SearchButtonMissingS", false, TestName = "Search Button Missing letter 'S'")]
        [TestCase("Google.Home", "Google.Home.SigninReducedSize", false, TestName = "Search Button Smaller")]
        [TestCase("Github.Home", "Github.Home", true, TestName = "Github same")]
        [TestCase("Github.Home.IgnoreSections", "Github.Home.LineThroughStats", true, TestName = "Github same with change in an 'ignored' section")]
        [TestCase("Github.Home.IgnoreSections", "Github.Home.LineThroughNoneIgnoredSection", false, TestName = "Github same with change not in 'ignored' section")]
        [TestCase("Github.Home.IgnoreSections", "Github.Home.IgnoreSections", true, TestName = "Lines Through")]
        [TestCase("Github.Home", "Github.TooSmall", false, TestName = "Images not same size", ExpectedException = typeof(ImagesAreNotSameSizeException))]
        [TestCase("SK.Home", "SK.Home", true, TestName = "Same Images Again")]
        [TestCase("SK.Home", "SK.Home.Line", false, TestName = "Line through it")]
        public void IsEqual(string src1, string src2, bool equal)
        {
            using (var s1 = Utils.Load(src1))
            using (var s2 = Utils.Load(src2))
            {
                var ic = new ImageComparer();
                Assert.AreEqual(equal, ic.IsEqual(s1,s2));
            }
        }        
    }
}
