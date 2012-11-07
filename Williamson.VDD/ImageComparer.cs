using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace Williamson.VDD
{
    /// <summary>
    /// Class used to compare images
    /// </summary>
    public class ImageComparer
    {
        public string IgnoreColor
        {
            get;
            private set;
        }
        public ImageComparer()
            : this("FFFFD800")
        {           
        }

        public ImageComparer(string ignoreCode)
        {
            if (ignoreCode.Length == 6) ignoreCode = "FF" + ignoreCode;
            this.IgnoreColor = ignoreCode;
        }

        /// <summary>
        /// Determines if 2 images are equal
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <returns></returns>
        public bool IsEqual(Stream expected, Stream actual)
        {
            return this.IsEqual(expected,actual,null);
        }
        /// <summary>
        /// Determines if two images are the same
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="differenceImage">A image showing the different is generated when not true</param>
        /// <returns></returns>
        public bool IsEqual(Stream expected, Stream actual, Action<Bitmap> differenceImage)
        {
            // create template matching algorithm's instance
            // use zero similarity to make sure algorithm will provide anything
            var tm = new ExhaustiveTemplateMatching(0);
            // compare two images
            var org = new Bitmap(expected);
            var act = new Bitmap(actual);

            //size check
            if (org.Size != act.Size) throw new ImagesAreNotSameSizeException();

            var orgData = org.LockBits(
                    new Rectangle( 0, 0, org.Width, org.Height ),
                    ImageLockMode.ReadWrite, org.PixelFormat );
            var actData = act.LockBits(
                   new Rectangle(0, 0, act.Width, act.Height),
                   ImageLockMode.ReadWrite, org.PixelFormat);
            try
            {
                var matchings = tm.ProcessImage(new UnmanagedImage(orgData), new UnmanagedImage(actData));
                // check similarity level; if one pixel out; fail!
                if (matchings[0].Similarity == 1.0f)
                {
                    return true;
                }             
            }
            catch
            {
                return false;
            }
            finally
            {
                org.UnlockBits(orgData);
                act.UnlockBits(actData);
            }

            return IsDifferenceTheEscapingColor(org,act,differenceImage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="org"></param>
        /// <param name="act"></param>
        /// <param name="differenceImage"></param>
        /// <returns></returns>
        private bool IsDifferenceTheEscapingColor(Bitmap org, Bitmap act, Action<Bitmap> differenceImage)
        {
            //now get the differences and ensure the color isn't the escape color
            var difference = new Difference(org);
            var result = difference.Apply(act);
            #if DEBUG
            result.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory , "diff.png"));
            #endif
            if (differenceImage != null) differenceImage(result);
            //look at all pixels not black
            for (int x = 0; x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                {
                    var pix = result.GetPixel(x, y);
                    if (pix.R!=0 && pix.G!=0 && pix.B!=0)
                    {
                        //check src image to see if it's the escape color
                        var srcPix = org.GetPixel(x, y);
                        var ignore = int.Parse(IgnoreColor,NumberStyles.AllowHexSpecifier,CultureInfo.InvariantCulture);
                        //ensure we're not dealing in a ignore zone
                        if (Color.FromArgb(srcPix.ToArgb()) != Color.FromArgb(ignore))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
