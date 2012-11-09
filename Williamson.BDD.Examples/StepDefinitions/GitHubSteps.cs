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

namespace Williamson.BDD.Examples.StepDefinitions
{
    /// <summary>
    /// GutHub Step Definitions
    /// </summary>
    [Binding]
    public class GitHubSteps : AbstractStepDefinitions
    {       

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
       
    }
}
