using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Williamson.BDD
{
    /// <summary>
    /// An extention to <see cref="IWebDriver"/> that adds addition information
    /// about the driver
    /// </summary>
    public interface IInformationalWebDriver
    {
        /// <summary>
        /// The IETFTag. e.g en-GB. 
        /// <c>null</c> indicates invariant.
        /// </summary>
        string Locale
        {
            get;
          
        }

        /// <summary>
        /// The browser
        /// </summary>
        Browsers Browser
        {
            get;
           
        }

        /// <summary>
        /// The WebDriver
        /// </summary>
        IWebDriver WebDriver
        {
            get;
           
        }
    }
}
