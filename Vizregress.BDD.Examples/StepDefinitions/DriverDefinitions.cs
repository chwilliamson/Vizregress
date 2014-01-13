using System;
using System.Resources;
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
        public void TitleShouldBe(string title)
        {
            Assert.AreEqual(title,WebDriver.Title,"Expected Title to equal: " + title);
        }

        [When("I click the (.*) button")]
        public void ClickButton(string name)
        {
            Click(name,"Button");
        }

        [Then("a new window should open with url (.*)")]
        public void NewWindowShouldOpen(string url)
        {
            var currentWindow = WebDriver.CurrentWindowHandle;
            Assert.AreEqual(2,WebDriver.WindowHandles.Count);

            try
            {
                WebDriver.SwitchTo().Window(WebDriver.WindowHandles[1]);
                UrlShouldBe(url);
            }
            finally
            {
                //switch back
                WebDriver.SwitchTo().Window(currentWindow);
            }
        }


        [Then("the page url should be (.*)")]
        public void UrlShouldBe(string url)
        {
            Assert.AreEqual(url, WebDriver.Url,"Expected url to equal:" + url);
        }

        private void Click(string name, string type)
        {
            var resourceName = string.Concat(name, type);
            var value = GetResourceString(WebDriver.Url, resourceName);
            var split = value.Split('=');
            By by = null;
            if(split[0].Equals("css"))
            {
                by = By.CssSelector(split[1]);
            }
            if(by==null) throw new NullReferenceException("No selector");
            WebDriver.FindElement(by).Click();
        }

        private string GetResourceString(string url,string resourceName)
        {
            var host = "stockport-kitchens";
            var rm = new ResourceManager("Vizregress.BDD.Examples.Maps."+host, GetType().Assembly);
            var value = rm.GetString(resourceName);
            if (value == null) throw new NullReferenceException("Resource " + resourceName + " not found");
            return value;
        }
    }
}
