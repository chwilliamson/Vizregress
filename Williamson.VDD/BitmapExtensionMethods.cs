using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Williamson.VDD
{
    public static class BitmapExtensionMethods
    {
        public static Bitmap To24bpp(this Bitmap bitmap)
        {
            var bmp = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format24bppRgb);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            return bmp;
        }
    }
}
