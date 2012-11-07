using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using Williamson.VDD;

namespace Williamson.BDD.StepDefinitions
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
    }
}
