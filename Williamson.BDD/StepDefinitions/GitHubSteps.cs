using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using Williamson.VDD;

namespace Williamson.BDD.StepDefinitions
{
    /// <summary>
    /// GutHub Step Definitions
    /// </summary>
    [Binding]
    public class GitHubSteps
    {
        /// <summary>
        /// Share driver between scenarios to save time
        /// </summary>
        [BeforeFeature]
        public static void CreateDriver()
        {
            var driver = new FirefoxDriver();
            driver.Manage().Window.Size = new Size(994, 1014); //copying my size
            FeatureContext.Current.Set<IWebDriver>(driver);
        }

        /// <summary>
        /// Close the driver onces the feature is complete
        /// </summary>
        [AfterFeature]
        public static void CloseDriver()
        {
            WebDriver.Close();
        }

        [Given(@"I visit github")]
        public void GivenIVisit()
        {
            WebDriver.
                Navigate().
                GoToUrl("https://github.com/");            
        }        

        [Then(@"the screen should look like (.*)")]
        public void LookLike(string name)
        {
            var driver = FeatureContext.Current.Get<IWebDriver>();
            if (driver is ITakesScreenshot)
            {
                var s = ((ITakesScreenshot)driver).GetScreenshot();
                var output = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name + ".actual.png");
                //save current shot in dir
                s.SaveAsFile(
                    output,
                    ImageFormat.Png);
                
                //load base reference image
                using (var streamExpected = Assembly.GetExecutingAssembly().GetManifestResourceStream("Williamson.BDD.Images." + name + ".png"))
                using (var streamActual = File.OpenRead(output))
                {
                    if (streamExpected == null) Assert.Fail("No image: " + name);
                    //now compare current with base
                    Assert.IsTrue(new ImageComparer().IsEqual(streamExpected, streamActual, (bm) => {
                        //save the diffences to manually inspect
                        bm.Save(output.Replace("actual","difference"));
                    }), "Images do not match");
                }
                
            }
            else
            {
                Assert.Fail("Can't take a screenshot");
            }
        }

        [Then(@"the repository count should be greater than (.*)")]
        public void ThenTheRepositoryCountShouldBeGreatThan(Decimal value)
        {
            var elements = WebDriver.FindElements(By.CssSelector(".hero h1 strong"));
            Assert.AreEqual(2, elements.Count);
            Assert.Greater( Decimal.Parse(elements[1].Text),value);
        }

        private static IDictionary<string, By> buttonMap = new Dictionary<string, By> { 
            {"Plans, Pricing and Signup", By.CssSelector(".signup-button")}
        };

        [Given(@"click the (.*) button")]
        public void GivenClickThePlansPricingAndSignupButton(string buttonTxt)
        {
            var by = buttonMap[buttonTxt];
            WebDriver.FindElement(by).Click();
            
        }

        [Then(@"I should be at (.*) page")]
        public void ThenIShouldBeAtPage(string title)
        {
            var header = WebDriver.FindElement(By.CssSelector(".pagehead h1"));
            Assert.AreEqual(title, header.Text);
        }

        /// <summary>
        /// Get access to feature context <see cref="IWebDriver"/>
        /// </summary>
        private static IWebDriver WebDriver
        {
            get { return FeatureContext.Current.Get<IWebDriver>(); }
        }
    }
}
