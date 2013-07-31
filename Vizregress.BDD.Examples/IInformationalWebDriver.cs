using OpenQA.Selenium;

namespace Vizregress.BDD.Examples
{
    /// <summary>
    /// An extension to <see cref="IWebDriver"/> that adds addition information
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
