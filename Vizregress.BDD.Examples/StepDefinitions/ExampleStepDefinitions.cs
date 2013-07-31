using OpenQA.Selenium;
using TechTalk.SpecFlow;
using System.Linq;

namespace Vizregress.BDD.Examples.StepDefinitions
{
    [Binding]
    public class ExampleStepDefinitions : AbstractStepDefinitions
    {
        [Given(@"I click the '(.*)' button")]
        public void GivenIClickTheButton(string buttonText)
        {
            var el =  WebDriver.FindElements(By.ClassName("ui-button-text")).FirstOrDefault(e => e.Text.Equals(buttonText));
            if (el == null)throw new NotFoundException("Cannot find button: " + buttonText);
            el.Click();
        }
    }
}
