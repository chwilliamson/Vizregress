using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using AForge.Imaging;
using Vizregress.Zone;
using Williamson.VDD;

namespace Vizregress
{
    /// <summary>
    /// Class used to compare images
    /// </summary>
    public class ImageComparer
    {
        public const string IGNORE_COLOR = "FFFFD800";
        
        /// <summary>
        /// The color that is used as the colour to ignore parts of the image
        /// </summary>
        public string IgnoreColor
        {
            get;
            private set;
        }

        /// <summary>
        /// Default constructor using default colour <see cref="IGNORE_COLOR"/>
        /// </summary>
        public ImageComparer()
            : this(IGNORE_COLOR)
        {           
        }

        public ImageComparer(string ignoreCode)
        {
            if (ignoreCode == null) throw new ArgumentNullException("ignoreCode");
            if (ignoreCode.Length == 6) ignoreCode = "FF" + ignoreCode;
            this.IgnoreColor = ignoreCode;
        }

        /// <summary>
        /// Determines whether or not a zone looks as expected.
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="actualWithZone"></param>
        /// <returns></returns>
        public bool ZonedEqual(Stream expected, Stream actual, Stream actualWithZone)
        {
            var extractor = new ZoneExtractor();
            var extracted = extractor.ExtractZone(actual, actualWithZone);
            return IsEqual(expected, extracted);
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
            // convert to 24 bit
            var org = new Bitmap(expected).To24bpp();
            var act = new Bitmap(actual).To24bpp();

            //size check
            if (org.Size != act.Size) throw new ImagesAreNotSameSizeException(org,act);
            
            var orgData = org.LockBits(
                    new Rectangle( 0, 0, org.Width, org.Height ),
                    ImageLockMode.ReadWrite, org.PixelFormat );
            var actData = act.LockBits(
                   new Rectangle(0, 0, act.Width, act.Height),
                   ImageLockMode.ReadWrite, act.PixelFormat);
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
        /// Get a differences between two images and determines if the difference is within the ignored region.
        /// </summary>
        /// <param name="org"></param>
        /// <param name="act"></param>
        /// <param name="differenceImage"></param>
        /// <returns></returns>
        private bool IsDifferenceTheEscapingColor(Bitmap org, Bitmap act, Action<Bitmap> differenceImage)
        {
            var result = Utils.GetDifferenceImage(org,act);
            //now get the differences and ensure the color isn't the escape color
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
