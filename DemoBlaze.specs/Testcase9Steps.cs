using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;
using DemoBlaze.specs.PageObjectModels;

namespace DemoBlaze.specs
{
    [Binding, Scope(Feature = "Testcase9")]
    public class Testcase9Steps
    {
        public static IWebDriver driver;

        public string productName;
        public string productPrice;

        [Given(@"I am on the homepage")]
        public void GivenIAmOnTheHomepage()
        {
            driver = new ChromeDriver();
            var homePage = new HomePage(driver);
            homePage.NavigateTo();
            Thread.Sleep(1000);
        }
        
        [Given(@"I select the first available product")]
        public void GivenISelectTheFirstAvailableProduct()
        {
            var homePage = new HomePage(driver);
            productName = homePage.getProductName_NotFiltered(1);
            productPrice = homePage.getProductPrice(1);
        }
        
        [When(@"I add it to my cart")]
        public void WhenIAddItToMyCart()
        {
            var homePage = new HomePage(driver);
            homePage.addItemToCart(1);
        }
        
        [When(@"I navigate to the cart page")]
        public void WhenINavigateToTheCartPage()
        {
            var cartPage = new CartPage(driver);
            cartPage.NavigateTo();
            Thread.Sleep(1000);
        }
        
        [Then(@"the selected product with the correct price should be displayed")]
        public void ThenTheSelectedProductWithTheCorrectPriceShouldBeDisplayed()
        {
            Assert.Equal(productName, driver.FindElement(By.CssSelector("#tbodyid > tr > td:nth-child(2)")).Text);
            Assert.Equal(productPrice, driver.FindElement(By.CssSelector("#tbodyid > tr > td:nth-child(3)")).Text);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
        }
    }
}
