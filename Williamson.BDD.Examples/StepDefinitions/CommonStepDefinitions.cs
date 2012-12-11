using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Williamson.Example.Web;

namespace Williamson.BDD.Examples.StepDefinitions
{
    /// <summary>
    /// Step definitions that are common across everything
    /// </summary>
    [Binding]
    public class CommonStepDefinitions : AbstractStepDefinitions
    {
        /// <summary>
        /// Share driver between scenarios to save time
        /// </summary>
        [BeforeFeature]
        public static void CreateDriver()
        {
            var factory = new WebDriverFactory();
            var id = factory.Create();
            FeatureContext.Current.Set<IInformationalWebDriver>(id);
        }

        /// <summary>
        /// Close the driver once the feature is complete
        /// </summary>
        [AfterFeature]
        public static void CloseDriver()
        {
            WebDriver.Close();
        }

        /// <summary>
        /// Start-up the self host
        /// </summary>
        [BeforeFeature("SelfHost")]
        public static void StartSelfHost() {
            var app = new App();
            app.Start();
            FeatureContext.Current.Set<App>(app);
        }

        /// <summary>
        /// Shutdown SelfHost
        /// </summary>
        [AfterFeature("SelfHost")]
        public static void StopSelfHost()
        {
            FeatureContext.Current.Get<App>().Stop();
        }
    }
}
