using System;
using System.Drawing;

namespace Williamson.VDD
{
    /// <summary>
    /// Means that the images were not the same size
    /// </summary>
    public class ImagesAreNotSameSizeException : ApplicationException
    {
        public ImagesAreNotSameSizeException(Bitmap actual,Bitmap expected) : 
            base(
            string.Format("The images are not the same size. They should be the same size when you want to make a comparison. Expected: {0}, Actual: {1}",
            expected.Size,actual.Size))
        {
            this.Actual = actual;
            this.Expected = expected;
            
        }

        /// <summary>
        /// The Actual Image
        /// </summary>
        public Bitmap Actual
        {
            get;
            private set;
        }

        /// <summary>
        /// The Expected Image
        /// </summary>
        public Bitmap Expected
        {
            get;
            private set;
        }
    }
}
