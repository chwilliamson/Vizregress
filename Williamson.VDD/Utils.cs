using System.Drawing;
using System.Globalization;
using System.IO;
using AForge.Imaging.Filters;

namespace Williamson.VDD
{
    public class Utils
    {
        /// <summary>
        /// Get a difference image between to images
        /// </summary>
        /// <param name="org"></param>
        /// <param name="act"></param>
        /// <returns></returns>
        public static Bitmap GetDifferenceImage(Bitmap org, Bitmap act)
        {
            var difference = new Difference(org);
            var result = difference.Apply(act);
            return result;
        }

        private class Xy
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public static Rectangle GetBounds(Stream image, string color = "#FF0000")
        {
            var org = new Bitmap(image).To24bpp();
            return GetBounds(org);
        }

        /// <summary>
        /// Get the bounds for the <paramref name="color"/> rectangle on the <paramref name="image"/>
        /// </summary>
        /// <param name="image"></param>
        /// <param name="color"></param>
        public static Rectangle GetBounds(Bitmap image, string color = "FFFF0000")
        {
            var tl = new Xy { X = -1, Y = -1 };
            var br = new Xy { X = -1, Y = -1 };

            //check src image to see if it's the escape color
            var actualColor = int.Parse(color, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
            var expectedColor = Color.FromArgb(actualColor);
                   
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var pix = image.GetPixel(x, y);

                    //ensure we're not dealing in a ignore zone
                    if (Color.FromArgb(pix.ToArgb()) == expectedColor)
                    {
                        //sets the lower bounds
                        if (tl.X == -1) tl.X = x;
                        if (tl.Y == -1) tl.Y = y;
                        //update lower bounds
                        if (x > br.X) br.X = x;
                        if (x > br.Y) br.Y = y;
                    }
                    
                }
            }

            return new Rectangle(tl.X,tl.Y,(br.X - tl.X)+1,(br.Y - tl.Y)+1);
        }
    }
}
