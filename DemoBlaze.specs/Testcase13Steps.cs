using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit.Abstractions;
using DemoBlaze.specs.PageObjectModels;

namespace DemoBlaze.specs
{
    [Binding, Scope(Feature = "Testcase13")]
    public class Testcase13Steps
    {
        public static IWebDriver driver;
        public Boolean findFlag = false;

        public string phoneName;
        public string phonePrice;
        public string laptopName;
        public string laptopPrice;
        public string monitorName;
        public string monitorPrice;
        public int remainingBudget;
        public int tempBudget;

        public const string HomeUrl = "https://www.demoblaze.com/";
        public const string CartUrl = "https://www.demoblaze.com/cart.html";
        public const string HomeTitle = "STORE";

        private readonly ITestOutputHelper output;

        Random number = new Random();

        public Testcase13Steps(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Given(@"I have (.*)\$")]
        public void GivenIHave(int budget)
        {
            driver = new ChromeDriver();
            var homePage = new HomePage(driver);
            homePage.NavigateTo();

            remainingBudget = budget;
        }
        
        [When(@"I add the items needed in the cart")]
        public void WhenIAddTheItemsNeededInTheCart()
        {
            var homePage = new HomePage(driver);
            tempBudget = remainingBudget;

            while (findFlag == false)
            {
                //phone
                homePage.filterBy("Phones");

                ReadOnlyCollection<IWebElement> _divs = homePage.returnAllElementsFiltered();
                int selector = number.Next(1, _divs.Count);

                phoneName = homePage.getProductName(selector);
                phonePrice = homePage.getProductPrice(selector);

                homePage.addItemToCart(selector);

                //adjust budget after selecting item
                tempBudget -= Int32.Parse(phonePrice);

                homePage.NavigateTo();

                if (tempBudget < 0)
                {
                    tempBudget = remainingBudget;
                    continue;
                };

                //laptop
                homePage.filterBy("Laptops");

                _divs = homePage.returnAllElementsFiltered();
                selector = number.Next(1, _divs.Count);

                laptopName = homePage.getProductName(selector);
                laptopPrice = homePage.getProductPrice(selector);

                homePage.addItemToCart(selector);

                //adjust budget after selecting item
                tempBudget -= Int32.Parse(laptopPrice);

                homePage.NavigateTo();

                if (tempBudget < 0)
                {
                    tempBudget = remainingBudget;
                    continue;
                };

                //monitor
                homePage.filterBy("Monitors");

                _divs = homePage.returnAllElementsFiltered();
                selector = number.Next(1, _divs.Count);

                monitorName = homePage.getProductName(selector);
                monitorPrice = homePage.getProductPrice(selector);

                homePage.addItemToCart(selector);

                //adjust budget after selecting item
                tempBudget -= Int32.Parse(monitorPrice);

                homePage.NavigateTo();

                if (tempBudget > 0) 
                    findFlag = true;
                else 
                {
                    tempBudget = remainingBudget;
                }
            }
            
            Thread.Sleep(1000);
        }
        
        [Then(@"I should be able to buy them")]
        public void ThenIShouldBeAbleToBuyThem()
        {
            var cartPage = new CartPage(driver);
            cartPage.NavigateTo();
            cartPage.pressCheckout();
            Thread.Sleep(1000);
            cartPage.completeOrderForm();

            try
            {
                driver.SwitchTo().ActiveElement();
            }
            catch (NoAlertPresentException exception)
            {
                output.WriteLine("Popup is not present: " + exception);
            }

            cartPage.confirmOrder();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
        }
    }
}
