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
        /// Indicates that a database pre-population should occur. This will override the existing
        /// database and add some default configuration used for tests.
        /// </summary>
        public bool PrePopulate { get; set; }
    }
}
