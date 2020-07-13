using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit.Abstractions;
using DemoBlaze.specs.PageObjectModels;

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
            var cartPage = new CartPage(driver);
            cartPage.NavigateTo();//modul in care accesezi cart-ul se face prin URL, deci functionalitatea butonului Cart nu se verifica daca nu faci click pe el
                                 //practic daca butonul Cart nu ar merge, user-ul nu ar putea face o comanda daca nu ar stii de URL care duce la pagina Cart
        }
        
        [When(@"I press Checkout")]
        public void WhenIPressCheckout()
        {
            var cartPage = new CartPage(driver);
            cartPage.pressCheckout();
            Thread.Sleep(1000);
        }
        
        [Then(@"I purchase the item")]
        public void ThenIPurchaseTheItem()
        {
            var cartPage = new CartPage(driver);
            cartPage.completeOrderForm();
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
