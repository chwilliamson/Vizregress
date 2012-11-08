using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Williamson.VDD;

namespace Williamson.BDD.StepDefinitions
{
    /// <summary>
    /// Look and feel step definitions
    /// </summary>
    [Binding]
    public class LookAndFeelSteps : AbstractStepDefinitions
    {
        /// <summary>
        /// The name of the embedded resource to use.  The image resources are stored in 
        /// <see cref="Williamson.BDD.Images"/>
        /// </summary>
        /// <param name="name"></param>
        [Then(@"the screen should look like (.*)")]
        public void LooksLike(string name)
        {
            var driver = WebDriver;
            if (driver is ITakesScreenshot)
            {
                var s = ((ITakesScreenshot)driver).GetScreenshot();
                var actualOutput = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name + ".actual.png");
                var expectedOutput = actualOutput.Replace("actual", "expected");
                var differenceOutput = actualOutput.Replace("actual", "difference");
                
                //save current shot in dir
                s.SaveAsFile(
                    actualOutput,
                    ImageFormat.Png);                

                //load base reference image
                var split = name.Split('.');
                //e.g. Williamson.BDD.Images.GitHub.FireFox.Home.png
                var resource = "Williamson.BDD.Images." + split[0] + "." + InformationalWebDriver.Browser + "." + split[1] +".png";
                //TODO: use locale
                using (var streamExpected = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                using (var streamActual = File.OpenRead(actualOutput))
                {
                    if (streamExpected == null) Assert.Fail("No image: " + name);
                    //save expected
                    new Bitmap(streamExpected).Save(expectedOutput);
                       
                    //now compare current with base
                    Assert.IsTrue(new ImageComparer().IsEqual(streamExpected, streamActual, (bm) =>
                    {
                        //save the diffences to manually inspect later
                         bm.Save(differenceOutput);
                    }), 
                    string.Format("Expected Image: {0}{3}But was: {1}{3}Difference: {2}", actualOutput,expectedOutput,differenceOutput, Environment.NewLine));
                }

            }
            else
            {
                Assert.Fail("The driver doesn't allow screenshots");
            }
        }
    }
}
