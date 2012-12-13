using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Williamson.Example.Web
{
    public class AppCfg
    {
        /// <summary>
        /// AppConfig friendly name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indicates that a database pre-population should occur
        /// </summary>
        public bool PrePopulate { get; set; }
    }
}
