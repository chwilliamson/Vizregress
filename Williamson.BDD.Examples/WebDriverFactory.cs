using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Williamson.BDD.Examples
{
    /// <summary>
    /// A factory for constructing <see cref="IInformationalWebDriver"/>
    /// </summary>
    public class WebDriverFactory : IWebDriverFactory
    {
        #region IWebDriverFactory Members
        
        /// <summary>
        /// Create an instance that implements <see cref="IInformationalWebDriver"/>
        /// </summary>
        /// <returns></returns>
        public IInformationalWebDriver Create()
        {
            var driver = new FirefoxDriver();
            driver.Manage().Window.Size = new Size(994, 1014);
            //there options should come from the environment
            return new InformationalDriver(Browsers.FireFox12, null,driver);
        }

        #endregion

        /// <summary>
        /// Represents some additional information about the <see cref="IWebDriver"/>
        /// </summary>
        private class InformationalDriver : IInformationalWebDriver
        {
            public InformationalDriver(
                Browsers browser, 
                string ietf, 
                IWebDriver driver)
            {
                this.WebDriver = driver;
                this.Browser = browser;
                this.Locale = ietf;
            }            

            #region IInformationalWebDriver Members

            public string Locale
            {
                get;
                private set;
            }

            public Browsers Browser
            {
                get;
                private set;
            }


            public IWebDriver WebDriver
            {
                get;
                private set;
            }

            #endregion           
        }
    }
}
