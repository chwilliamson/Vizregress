using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Williamson.BDD
{
    /// <summary>
    /// A factory interface for creating <see cref="IInformationalWebDriver"/>
    /// </summary>
    public interface IWebDriverFactory
    {
        /// <summary>
        /// The informational webdriver
        /// </summary>
        /// <returns></returns>
        IInformationalWebDriver Create();       
    }
}
