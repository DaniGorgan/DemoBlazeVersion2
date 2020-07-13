using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit.Abstractions;
using DemoBlaze.specs.PageObjectModels;
using OpenQA.Selenium.Support.UI;
using System;

namespace DemoBlaze.specs
{
    [Binding, Scope(Feature = "Testcase12")]
    public class Testcase12Steps
    {
        public static IWebDriver driver;
        private readonly ITestOutputHelper output;

        public Testcase12Steps(ITestOutputHelper output)
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
        
        [Given(@"I add an item to the cart")]
        public void GivenIAddAnItemToTheCart()
        {
            var homePage = new HomePage(driver);
            homePage.addItemToCart(1);
        }
        
        [When(@"I go to the cart page")]
        public void WhenIGoToTheCartPage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

            var cartPage = new CartPage(driver);

            //click Cart Button
            driver.FindElement(By.CssSelector("#navbarExample > ul > li:nth-child(4) > a")).Click();
            //Thread.Sleep(1000);

            wait.Until((d) => cartPage.EnsurePageLoaded());

            //modul in care accesezi cart-ul se face prin URL, deci functionalitatea butonului Cart nu se verifica daca nu faci click pe el
            //practic daca butonul Cart nu ar merge, user-ul nu ar putea face o comanda daca nu ar stii de URL care duce la pagina Cart
        }
        
        [When(@"I press Checkout")]
        public void WhenIPressCheckout()
        {
            var cartPage = new CartPage(driver);
            cartPage.pressCheckout();
        }
        
        [Then(@"I purchase the item")]
        public void ThenIPurchaseTheItem()
        {
            var cartPage = new CartPage(driver);
            cartPage.completeOrderForm("Test User", "Romania", "Timisoara", "1234 5678 1234 5678", "July", "2020");
            cartPage.confirmOrder();
        }

        [Then(@"the cart is empty")]
        public void Thenthecartisempty()
        {
            try
            {
                driver.FindElement(By.Id("totalp"));
            }
            catch (NoSuchElementException exception)
            {
                output.WriteLine("Popup is not present: " + exception);
            }

        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
        }
    }
}
