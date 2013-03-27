using System.IO;
using NUnit.Framework;
using Williamson.VDD.Zone;

namespace Williamson.VDD.Tests.Zone
{
    /// <summary>
    /// Performs tests against <see cref="ZoneExtractor"/>
    /// </summary>
    [TestFixture]
    public class ZoneExtractorFeature
    {
        /// <summary>
        /// Perform as test to ensure the zone is extracted
        /// </summary>
        [Test]
        public void ZoneIsExtractedTest()
        {
            var original = Utils.Load("Zoning.OverallStatus_NoZones");
            var zoned = Utils.Load("Zoning.OverallStatus_Zoned");
           
            var extractor = new ZoneExtractor();

            var actual = extractor.ExtractZone(original, zoned);
            var expected = Utils.Load("Zoning.OverallStatus_ZoneCut");

            var comparer = new ImageComparer();
            Assert.IsTrue(comparer.IsEqual(expected, actual));
        }
    }
}
