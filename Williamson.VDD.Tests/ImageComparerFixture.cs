using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Williamson.VDD.Tests
{
    /// <summary>
    /// My fixture
    /// </summary>
    [TestFixture]
    public class ImageComparerFixture
    {
        /// <summary>
        /// Loads image.
        /// </summary>
        /// <param name="image">The image without it's extension. By default .png is applied 
        /// unless you specify a <paramref name="extension"/></param>
        /// <param name="extension">Default .png</param>
        /// <returns>A resource stream</returns>
        private Stream Load(string image, string extension = "png")
        {
            var r = "Williamson.VDD.Tests.Images." + image + "." + extension;
            var s =  Assembly.GetExecutingAssembly().GetManifestResourceStream(r);
            //if adding new images ensure they are added as an embedded resource
            if (s == null) Assert.Fail("Cannot load:" + r);
            return s;
        }
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
        [TestCase("Github.Home.IgnoreSections", "Github.Home.LineThroughNoneIgnoredSection", false, TestName = "Github same with change not an 'ignored' section")]
        [TestCase("Github.Home.IgnoreSections", "Github.Home.IgnoreSections", true, TestName = "Lines Through")]
        [TestCase("Github.Home", "Github.TooSmall", false, TestName = "Images not same size", ExpectedException = typeof(ImagesAreNotSameSizeException))]        
        public void IsEqual(string src1, string src2, bool equal)
        {
            using (var s1 = this.Load(src1))
            using (var s2 = this.Load(src2))
            {
                var ic = new ImageComparer();
                Assert.AreEqual(equal, ic.IsEqual(s1,s2));
            }
        }        
    }
}
