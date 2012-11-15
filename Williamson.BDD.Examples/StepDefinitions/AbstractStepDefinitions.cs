using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Williamson.BDD.Examples.StepDefinitions
{
    /// <summary>
    /// Definitions shared with subclasses
    /// </summary>
    public abstract class AbstractStepDefinitions
    {
        /// <summary>
        /// Get access to feature context <see cref="IWebDriver"/>
        /// </summary>
        protected static IInformationalWebDriver InformationalWebDriver
        {
            get { return FeatureContext.Current.Get<IInformationalWebDriver>(); }
        }

        /// <summary>
        /// The underlying web driver
        /// </summary>
        protected static IWebDriver WebDriver
        {
            get { return InformationalWebDriver.WebDriver; }
        }
    }
}
