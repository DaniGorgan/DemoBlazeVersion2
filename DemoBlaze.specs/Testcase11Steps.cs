using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;
using DemoBlaze.specs.PageObjectModels;

namespace DemoBlaze.specs
{
    [Binding, Scope(Feature = "Testcase11")]
    public class Testcase11Steps
    {
        public static IWebDriver driver;

        public string productName;

        [Given(@"I am on the homepage")]
        public void GivenIAmOnTheHomepage()
        {
            driver = new ChromeDriver();
            var homePage = new HomePage(driver);
            homePage.NavigateTo();
        }
        
        [When(@"I search for the product that I need")]
        public void WhenISerachForTheProductThatINeed()
        {
            var homePage = new HomePage(driver);
            homePage.filterBy("Laptops");
        }
        
        [When(@"I add it to my cart")]
        public void WhenIAddItToMyCart()
        {
            var homePage = new HomePage(driver);
            productName = homePage.addItemToCartPartialLink("2017 Dell");
        }
        
        [Then(@"the correct item is displayed in the cart")]
        public void ThenTheCorrectItemIsDisplayedInTheCart()
        {
            driver.FindElement(By.CssSelector("#navbarExample > ul > li:nth-child(4) > a")).Click();
            Thread.Sleep(1000);

            Assert.Equal(productName, driver.FindElement(By.CssSelector("#tbodyid > tr > td:nth-child(2)")).Text);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
        }
    }
}
