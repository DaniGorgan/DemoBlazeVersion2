using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;
using Xunit.Abstractions;
using DemoBlaze.specs.PageObjectModels;

namespace DemoBlaze.specs
{
    [Binding, Scope(Feature = "Testcase8")]
    public class Testcase8Steps
    {
        public static IWebDriver driver;

        private readonly ITestOutputHelper output;

        public Testcase8Steps(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Given(@"I am on the homepage")]
        public void GivenIAmOnTheHomepage()
        {
            driver = new ChromeDriver();
            var homePage = new HomePage(driver);
            homePage.NavigateTo();
        }
        
        [When(@"I select the first product")]
        public void GivenISelectTheFirstProduct()
        {
            var homePage = new HomePage(driver);
            homePage.selectItem(1);
            Thread.Sleep(1000);
        }
        
        [Then(@"the price should be displayed in the new page")]
        public void ThenThePriceShouldBeDisplayedInTheNewPage()
        {
            Assert.NotNull(driver.FindElement(By.ClassName("price-container")));
            output.WriteLine("The price of the selected product is: " + driver.FindElement(By.ClassName("price-container")).Text);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
        }
    }
}
