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
        public void ZoneIsExtracted()
        {
            var original = Utils.Load("Zoning.OverallStatus_NoZones"); //article without cut zone
            var zoned = Utils.Load("Zoning.OverallStatus_Zoned"); //article with cut zone
           
            var extractor = new ZoneExtractor();

            var actual = extractor.ExtractZone(original, zoned);
            var expected = Utils.Load("Zoning.OverallStatus_ZoneCut"); //just the donkey

            var comparer = new ImageComparer();
            Assert.IsTrue(comparer.IsEqual(expected, actual));
        }

        /// <summary>
        /// Perform as test to ensure the zone is extracted but is wrong
        /// </summary>
        [Test]
        public void ZoneExtractedIsWrong()
        {
            var original = Utils.Load("Zoning.OverallStatus_NoZones"); //article without cut zone
            var zoned = Utils.Load("Zoning.OverallStatus_Zoned"); //article with cut zone

            var extractor = new ZoneExtractor();

            var actual = extractor.ExtractZone(original, zoned);
            var expected = Utils.Load("Zoning.OverallStatus_ZoneCutIsCat"); //its the cat

            var comparer = new ImageComparer();
            Assert.IsFalse(comparer.IsEqual(expected, actual));
        }
    }
}
