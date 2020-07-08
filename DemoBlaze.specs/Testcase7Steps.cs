using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using DemoBlaze.specs.PageObjectModels;

namespace DemoBlaze.specs
{
    [Binding, Scope(Feature = "Testcase7")]
    public class Testcase7Steps
    {
        public static IWebDriver driver;

        [Given(@"I am on the homepage")]
        public void GivenIAmOnTheHomepage()
        {
            driver = new ChromeDriver();
            var homePage = new HomePage(driver);
            homePage.NavigateTo();
        }
        
        [When(@"I navigate to the cart page")]
        public void WhenINavigateToTheCartPage()
        {
            var cartPage = new CartPage(driver);
            cartPage.NavigateTo();
        }
        
        [Then(@"the cart page should be displayed")]
        public void ThenTheCartPageShouldBeDisplayed()
        {
            var cartPage = new CartPage(driver);
            cartPage.EnsurePageLoaded();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
        }
    }
}
