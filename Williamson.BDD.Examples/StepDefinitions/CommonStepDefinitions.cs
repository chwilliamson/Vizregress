using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Williamson.BDD.Examples.StepDefinitions
{
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
        /// Close the driver onces the feature is complete
        /// </summary>
        [AfterFeature]
        public static void CloseDriver()
        {
            WebDriver.Close();
        }        
    }
}
