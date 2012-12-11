using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Williamson.Example.Web.Model
{
    /// <summary>
    /// Represents an individual run
    /// </summary>
    public class Run
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }
        /// <summary>
        /// Name of run
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Time first image was detected
        /// </summary>
        public DateTime RunTime
        {
            get;
            set;
        }
    }
}
