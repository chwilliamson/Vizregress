using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace Vizregress.BDD.Examples.StepDefinitions
{
    /// <summary>
    /// Driver related step definitions
    /// </summary>
    [Binding]
    public class DriverDefinitions : AbstractStepDefinitions
    {
        [Given(@"I visit (.*)")]
        public void GivenIVisit(string url)
        {
            WebDriver.
                Navigate().
                GoToUrl(url);
            //ensure everything is loaded
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30)).Until(d=>((IJavaScriptExecutor)d).ExecuteScript("return document.readyState;").Equals("complete"));
        }

        [Then("the page title should be (.*)")]
        public void Title(string title)
        {
            Assert.AreEqual(title,WebDriver.Title);
        }
    }
}
