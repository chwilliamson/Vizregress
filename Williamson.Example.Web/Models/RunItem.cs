
namespace Williamson.Example.Web.Models
{
    public class RunItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id
        {
            get;
            set;
        }

        public int RunId
        {
            get;
            set;
        }

        /// <summary>
        /// Result
        /// </summary>
        public RunItemResult Result
        {
            get;
            set;
        }

        /// <summary>
        /// Expected Image Identifier
        /// </summary>
        public string ExpectedId
        {
            get;
            set;
        }

        /// <summary>
        /// Actual Image Identifier that has already been uploaded
        /// </summary>
        public int ImageId
        {
            get;
            set;
        }
    }
}
