using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Vizregress.BDD.Examples.StepDefinitions
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
