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
    //Test is incorrect. After one test execution i ended up with a bill of 8480$. 
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
        public void WhenIAddTheItemsNeededInTheCart() // am rulat si am ajuns in cos cu produse ce valorau un total de peste 16000 de $
                                                     // un mod de a face ar fi sa iei produsul din fiecare categorie care are cel mai mic pret, le adaugi in cart si apoi verifici ca se 
                                                    // incadreaza in buget
        {
            /* method needs to change. Some implementation ways:
             * 1. To be sure that you can stay in the budget, scan all the prices from each
             * category, chose the lowest, then add them to cart. This is the easiest way
             * 2. If you want to take them random, scan the prices from each category and save
             * them to a list of objects with properties (phone name / price).
             * then do a random pick from each list. After that, compare result with give budget.
             * When you found the right price, you can select then each product and add it to cart
             */
            var homePage = new HomePage(driver);
            tempBudget = remainingBudget;

            while (findFlag == false)
            {
                //monitor
                homePage.filterBy("Monitors");

                ReadOnlyCollection<IWebElement> _filteredProducts = homePage.returnAllElementsFiltered();// o denumire mai clara a variabilelor. Divs nu prea ajuta pentru a intelege ce elemente contine
                int selector = number.Next(1, _filteredProducts.Count);

                monitorName = homePage.getProductName(selector);
                monitorPrice = homePage.getProductPrice(selector);

                //adjust budget after selecting item
                tempBudget -= Int32.Parse(monitorPrice);

                if (tempBudget > 0)
                {
                    homePage.addItemToCart(selector);
                    homePage.NavigateTo();
                }
                else
                {
                    tempBudget = remainingBudget;
                    homePage.NavigateTo();
                    continue;
                }

                remainingBudget = tempBudget; //Re-adjust remaining budget after selecting a Monitor

                //laptop
                while (remainingBudget - tempBudget <= 320) //320 is the lowest price of a phone - needed in the next product selection
                {
                    homePage.filterBy("Laptops");

                    _filteredProducts = homePage.returnAllElementsFiltered();
                    selector = number.Next(1, _filteredProducts.Count);

                    laptopName = homePage.getProductName(selector);
                    laptopPrice = homePage.getProductPrice(selector);

                    //adjust budget after selecting item
                    tempBudget -= Int32.Parse(laptopPrice);

                    if (tempBudget < 320)
                    {
                        tempBudget = remainingBudget;
                        homePage.NavigateTo();
                        continue;
                    }
                    else
                    {
                        homePage.addItemToCart(selector);
                        homePage.NavigateTo();
                    }
                }

                remainingBudget = tempBudget; //Re-adjust remaining budget after selecting a Laptop

                //phone
                while (remainingBudget - tempBudget <= 0)
                {
                    homePage.filterBy("Phones");

                    _filteredProducts = homePage.returnAllElementsFiltered();
                    selector = number.Next(1, _filteredProducts.Count);

                    phoneName = homePage.getProductName(selector);
                    phonePrice = homePage.getProductPrice(selector);

                    //adjust budget after selecting item
                    tempBudget -= Int32.Parse(phonePrice);

                    if (tempBudget < 0)
                    {
                        tempBudget = remainingBudget;
                        homePage.NavigateTo();
                    }
                    else
                    {
                        homePage.addItemToCart(selector);
                        homePage.NavigateTo();
                        findFlag = true;
                    }
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
            cartPage.completeOrderForm("Test User", "Romania", "Timisoara", "1234 5678 1234 5678", "July", "2020");

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
