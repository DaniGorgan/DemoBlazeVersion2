using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;
using DemoBlaze.specs.PageObjectModels;

namespace DemoBlaze.specs
{
    [Binding, Scope(Feature = "Testcase1")]
    public class Testcase1Steps
    {

        // all the variables used here can be made private
        public static IWebDriver driver;

        public Boolean findFlag = false;

        public string phoneName1;
        public string phonePrice1;
        public string phoneName2;
        public string phonePrice2;

        public string username = "TestUser1";
        public string password = "TempPassword";

        public int totalPrice = 0;
        public int remainingBudget;

        //temp budget can be declared only where it's used, since it's not used in other methods
        public int tempBudget;

        Random number = new Random();

        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            driver = new ChromeDriver();
            var homePage = new HomePage(driver);
            homePage.NavigateTo();

            homePage.clickLoginButton();
            //remove  thread.sleep
            Thread.Sleep(1000);
            homePage.loginEnterCredentials(username, password);

            string welcomeText = "Welcome " + username;

            homePage.ensureLoginPerformed(welcomeText);
        }

        [Given(@"I have a budget of (.*)\$")]
        public void GivenIHaveABudgetOf(int budget)
        {
            remainingBudget = budget;
        }
        
        [When(@"I filter by Phones")]
        public void WhenIFilterByPhones()
        {
            var homePage = new HomePage(driver);
            homePage.filterBy("Phones");
        }
        
        [Then(@"I can add to cart 2 random phones that don't exceed my budget")]
        public void ThenICanAddToCartRandomPhonesThatDonTExceedMyBudget()
        {
            //try emptying the cart before executing test.
            var homePage = new HomePage(driver);
            var cartPage = new CartPage(driver);

            tempBudget = remainingBudget;

            ReadOnlyCollection<IWebElement> _divs = homePage.returnAllElementsFiltered();
            int selector = number.Next(1, _divs.Count);

            //phoneName1 not returning product name
            phoneName1 = homePage.getProductName(selector);
            phonePrice1 = homePage.getProductPrice(selector);

            homePage.addItemToCart(selector);

            //adjust budget after selecting item
            tempBudget -= Int32.Parse(phonePrice1);
            totalPrice += Int32.Parse(phonePrice1);

            homePage.NavigateTo();

            while (findFlag == false)
            {
                //phone
                homePage.filterBy("Phones");

                _divs = homePage.returnAllElementsFiltered();
                selector = number.Next(1, _divs.Count);

                phoneName2 = homePage.getProductName(selector);
                phonePrice2 = homePage.getProductPrice(selector);

                if (Int32.Parse(phonePrice2) > tempBudget)
                {
                    homePage.NavigateTo();
                    continue;
                }
                //redundant else. as you have the continue keyword in the if, you can lose the else. Or vice versa (continue or else)
                else
                {
                    //add to cart
                    homePage.addItemToCart(selector);

                    findFlag = true;

                    tempBudget -= Int32.Parse(phonePrice2);
                    totalPrice += Int32.Parse(phonePrice2);
                }
            }

            cartPage.NavigateTo();
            Thread.Sleep(1000);
            
            Assert.Equal(totalPrice, Int32.Parse(driver.FindElement(By.Id("totalp")).Text));
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //remove items from cart
            driver.FindElement(By.CssSelector("#tbodyid > tr:nth-child(1) > td:nth-child(4) > a")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.CssSelector("#tbodyid > tr > td:nth-child(4) > a")).Click();
            Thread.Sleep(1000);

            driver.Close();
        }
    }
}
