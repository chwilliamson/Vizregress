using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace Williamson.TDD
{
    /// <summary>
    /// Class used to compare images
    /// </summary>
    public class ImageComparer
    {
        /// <summary>
        /// Determines if two images are the same
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <returns></returns>
        public bool IsEqual(Stream expected, Stream actual)
        {
            // create template matching algorithm's instance
            // use zero similarity to make sure algorithm will provide anything
            var tm = new ExhaustiveTemplateMatching(0);
            // compare two images
            var org = new Bitmap(expected);
            var act = new Bitmap(actual);
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


                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                org.UnlockBits(orgData);
                act.UnlockBits(actData);
            }
        }
    }
}
