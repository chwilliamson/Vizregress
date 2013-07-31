using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace Vizregress.Tests
{
    /// <summary>
    /// Test utilities
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Loads image.
        /// </summary>
        /// <param name="image">The image without it's extension. By default .png is applied 
        /// unless you specify a <paramref name="extension"/></param>
        /// <param name="extension">Default .png</param>
        /// <returns>A resource stream</returns>
        public static Stream Load(string image, string extension = "png")
        {
            var r = "Vizregress.Tests.Images." + image + "." + extension;
            var s = Assembly.GetExecutingAssembly().GetManifestResourceStream(r);
            //if adding new images ensure they are added as an embedded resource
            if (s == null) Assert.Fail("Cannot load:" + r);
            return s;
        }
    }
}
