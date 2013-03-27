using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;

namespace Williamson.VDD.Zone
{
    public class ZoneExtractor
    {
        /// <summary>
        /// Extracts the image zone
        /// </summary>
        /// <param name="imageWithoutZone"></param>
        /// <param name="imageWithZone"></param>
        /// <returns></returns>
        public Zone ExtractZone(Stream imageWithoutZone, Stream imageWithZone)
        {
            var org = new Bitmap(imageWithZone).To24bpp();
            var act = new Bitmap(imageWithoutZone).To24bpp();

            //size check
            if (org.Size != act.Size) throw new ImagesAreNotSameSizeException(org, act);

            var orgData = org.LockBits(
                    new Rectangle(0, 0, org.Width, org.Height),
                    ImageLockMode.ReadWrite, org.PixelFormat);
            var actData = act.LockBits(
                   new Rectangle(0, 0, act.Width, act.Height),
                   ImageLockMode.ReadWrite, act.PixelFormat);

            var zone = new Zone();
            try
            {
                
            }
            finally
            {
                org.UnlockBits(orgData);
                act.UnlockBits(actData);
            }

            return zone;
        }

        private void FindZone()
        {
            
        }

        public class Zone
        {
            public Zone()
            {
                
            }
            public Zone(string name, Stream image)
            {
                Image = image;
                Name = name;
            }
            public Stream Image { get; internal set; }
            public string Name { get; internal set; }
        }
    }
}
