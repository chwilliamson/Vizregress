using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Williamson.Example.Web.Models
{
    /// <summary>
    /// An approved item is an item that is promoted
    /// </summary>
    public class ApprovedItem
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        public int ImageId
        {
            get;
            set;
        }
    }
}
