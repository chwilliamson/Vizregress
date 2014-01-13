using TechTalk.SpecFlow;

namespace Vizregress.BDD.Examples.StepDefinitions
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
            FeatureContext.Current.Set(id);
        }

        /// <summary>
        /// Close the driver once the feature is complete
        /// </summary>
        [AfterFeature]
        public static void CloseDriver()
        {
            WebDriver.Close();
        }
    }
}
