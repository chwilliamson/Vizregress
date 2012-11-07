using System;
using System.Drawing.Imaging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace Williamsons.BDD.StepDefinitions
{
    [Binding]
    public class GithubSteps
    {
        [Given(@"I visit github")]
        public void GivenIVisit()
        {
            var cd = new FirefoxDriver();
            cd.Navigate().GoToUrl("https://github.com/");

            FeatureContext.Current.Set<IWebDriver>(cd);
        }

        [Then(@"I close the browser")]
        public void Close()
        {
            FeatureContext.Current.Get<IWebDriver>().Close();
        }

        [Then(@"the screen should look like")]
        public void LookLike()
        {
            var driver = FeatureContext.Current.Get<IWebDriver>();
            if (driver is ITakesScreenshot)
            {
                var s = ((ITakesScreenshot)driver).GetScreenshot();
                s.SaveAsFile("C:\\temp\\screen.png", ImageFormat.Png);
            }
        }
    }
}
