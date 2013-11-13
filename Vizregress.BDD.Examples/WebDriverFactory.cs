using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Vizregress.BDD.Examples
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
            var options = new PhantomJSOptions();
            options.AddAdditionalCapability("phantomjs.page.settings.userAgent", "Mozilla/5.0 (Windows NT 6.0; WOW64) AppleWebKit/535.7 (KHTML, like Gecko) Chrome/16.0.912.75 Safari/535.7");
            var driver = new PhantomJSDriver(options);
     
            driver.Manage().Window.Size = new Size(1280, 800);
            //there options should come from the environment
            return new InformationalDriver(Browsers.Phantomjs, null,driver);
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
                WebDriver = driver;
                Browser = browser;
                Locale = ietf;
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
