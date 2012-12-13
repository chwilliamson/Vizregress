using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Williamson.Example.Web.Model
{
    public class RunItem
    {
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
        public Guid ExpectedImageId
        {
            get;
            set;
        }

        /// <summary>
        /// Actual Image Identifier
        /// </summary>
        public Guid ActualSourceId
        {
            get;
            set;
        }
    }
}
