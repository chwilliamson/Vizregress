using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Vizregress.Zone
{
    /// <summary>
    /// A extract that will extract rectangles from images.  
    /// <para/>
    /// Images with a single zone are expected.
    /// </summary>
    public class ZoneExtractor
    {
        /// <summary>
        /// Extracts the image zone
        /// </summary>
        /// <param name="imageWithoutZone"></param>
        /// <param name="imageWithZone"></param>
        /// <returns></returns>
        public Stream ExtractZone(Stream imageWithoutZone, Stream imageWithZone)
        {
            var org = new Bitmap(imageWithZone).To24bpp();
            var act = new Bitmap(imageWithoutZone).To24bpp();

            //size check
            if (org.Size != act.Size) throw new ImagesAreNotSameSizeException(org, act);
            
            var bounds = Utils.GetBounds(imageWithZone);

            //cut image
            var target = new Bitmap(bounds.Width, bounds.Height);

            using (var g = Graphics.FromImage(target))
            {
                //extract the rectangle
                g.DrawImage(act, new Rectangle(0, 0, target.Width, target.Height),
                                    bounds,
                                    GraphicsUnit.Pixel);
            }

            var s = new MemoryStream();
            target.Save(s,ImageFormat.Png);
            return s;
        }
        
    }
}
