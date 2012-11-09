using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Williamson.Example.Web;

namespace Williamson.BDD.Examples.StepDefinitions
{
    [Binding]
    public class DriverDefinitions : AbstractStepDefinitions
    {
        [Given(@"I visit (.*)")]
        public void GivenIVisit(string name)
        {
            var url = "https://github.com/";
            if (!name.Equals("github", StringComparison.OrdinalIgnoreCase))
            {
                url = FeatureContext.Current.Get<App>().Uri.ToString() + "/" + name;
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
