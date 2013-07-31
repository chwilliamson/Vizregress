using System;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Vizregress.Example.Web;

namespace Vizregress.BDD.Examples.StepDefinitions
{
    /// <summary>
    /// Driver related step definitions
    /// </summary>
    [Binding]
    public class DriverDefinitions : AbstractStepDefinitions
    {
        [Given(@"I visit (.*)")]
        public void GivenIVisit(string name)
        {
            var url = "https://github.com/";
            if (name.Equals("Example", StringComparison.OrdinalIgnoreCase))
            {
                url = FeatureContext.Current.Get<App>().Uri.ToString() + "/";
            }
            WebDriver.
                Navigate().
                GoToUrl(url);
        }

        [Then("the page title should be (.*)")]
        public void Title(string title)
        {
            Assert.AreEqual(title,WebDriver.Title);
        }
    }
}
