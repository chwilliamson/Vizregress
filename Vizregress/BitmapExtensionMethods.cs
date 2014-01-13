using System.Drawing;
using System.Drawing.Imaging;

namespace Vizregress
{
    /// <summary>
    /// Extension methods for <see cref="Bitmap"/>
    /// </summary>
    public static class BitmapExtensionMethods
    {
        /// <summary>
        /// Convert to the bitmap to 24 bit
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Bitmap To24Bpp(this Bitmap bitmap)
        {
            var bmp = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format24bppRgb);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            return bmp;
        }
    }
}
