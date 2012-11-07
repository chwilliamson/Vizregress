using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
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
            FeatureContext.Current.Get<IWebDriver>().Close();
        }

        [Given(@"I visit github")]
        public void GivenIVisit()
        {
            FeatureContext.Current.
                Get<IWebDriver>().
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
                        bm.Save(output.Replace("actual","expected"));
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
            var elements = FeatureContext.Current.Get<IWebDriver>().FindElements(By.CssSelector(".hero h1 strong"));
            Assert.AreEqual(2, elements.Count);
            Assert.Greater( Decimal.Parse(elements[1].Text),value);
        }
    }
}
