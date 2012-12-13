using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Williamson.Example.Web.Model
{
    /// <summary>
    /// Item type
    /// </summary>
    public enum RunItemResult
    {
        /// <summary>
        /// No expectation image found
        /// </summary>
        NoExpectation,

        /// <summary>
        /// Successful match
        /// </summary>
        Success,

        /// <summary>
        /// Failed match
        /// </summary>
        Failure
    }
}
