using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;
using Xunit.Abstractions;
using DemoBlaze.specs.PageObjectModels;

namespace DemoBlaze.specs
{
    [Binding, Scope(Feature = "Testcase10")]
    public class Testcase10Steps
    {
        public static IWebDriver driver;
        private readonly ITestOutputHelper output;

        public string pageInTest;
        public const string HomePageUrl = "https://www.demoblaze.com/index.html";
        public const string CartUrl = "https://www.demoblaze.com/cart.html";

        public Testcase10Steps(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Given(@"I am on the homepage")]
        public void GivenIAmOnTheHomepage()
        {
            driver = new ChromeDriver(); // poti scoate driver-ul inafara metodelor si apoi doar apelezi obiectul unde ai nevoie
            var homePage = new HomePage(driver);
            homePage.NavigateTo();
        }
        
        [When(@"I access “(.*)”")]
        public void WhenIAccess(string pageName)
        {
            if (pageName == "Home")
            {
                pageInTest = pageName;

                driver.FindElement(By.CssSelector("#navbarExample > ul > li.nav-item.active > a")).Click();// poti scoate intr-o metoda in HomePage, partea asta de navigare
                                                                                                            // gen GoToHomePage(); sau poti face o metoda care sa navigheze la o pagina in functie de parametru
                                                                                                            //asa reduci codul din Steps
                Thread.Sleep(1000);
            }
            else if (pageName == "Contact")
            {
                pageInTest = pageName;

                driver.FindElement(By.CssSelector("#navbarExample > ul > li:nth-child(2) > a")).Click();
                Thread.Sleep(1000);
            }
            else if (pageName == "About us")
            {
                pageInTest = pageName;

                driver.FindElement(By.CssSelector("#navbarExample > ul > li:nth-child(3) > a")).Click();
                Thread.Sleep(1000);
            }
            else if (pageName == "Cart")
            {
                pageInTest = pageName;

                driver.FindElement(By.CssSelector("#navbarExample > ul > li:nth-child(4) > a")).Click();
                Thread.Sleep(1000);
            }
            else if (pageName == "Log in")
            {
                pageInTest = pageName;

                driver.FindElement(By.CssSelector("#navbarExample > ul > li:nth-child(5) > a")).Click();
                Thread.Sleep(1000);
            }
            else if (pageName == "Sign up")
            {
                pageInTest = pageName;

                driver.FindElement(By.CssSelector("#navbarExample > ul > li:nth-child(8) > a")).Click();
                Thread.Sleep(1000);
            }
        }
        
        [Then(@"the correct page should be displayed")]
        public void ThenTheCorrectPageShouldBeDisplayed()
        {
            var homePage = new HomePage(driver);
            var cartPage = new CartPage(driver);

            if (pageInTest == "Home")
            {
                Assert.Equal(HomePageUrl, driver.Url);

                homePage.NavigateTo(); // Nu mai ai nevoie de inca o navigare catre homepage
            }
            else if (pageInTest == "Contact")
            {
                try
                {
                    driver.SwitchTo().ActiveElement();
                }
                catch (NoAlertPresentException exception)
                {
                    output.WriteLine("Popup is not present: " + exception);
                }

                //homePage.NavigateTo();
            }
            else if (pageInTest == "About us")
            {
                try
                {
                    driver.SwitchTo().ActiveElement();
                }
                catch (NoAlertPresentException exception)
                {
                    output.WriteLine("Popup is not present: " + exception);
                }

                //homePage.NavigateTo();
            }
            else if (pageInTest == "Cart")
            {
                cartPage.EnsurePageLoaded();

                homePage.NavigateTo();
            }
            else if (pageInTest == "Log in")
            {
                try
                {
                    driver.SwitchTo().ActiveElement();
                }
                catch (NoAlertPresentException exception)
                {
                    output.WriteLine("Popup is not present: " + exception);
                }

               // homePage.NavigateTo();
            }
            else if (pageInTest == "Sign up")
            {
                try
                {
                    driver.SwitchTo().ActiveElement();
                }
                catch (NoAlertPresentException exception)
                {
                    output.WriteLine("Popup is not present: " + exception);
                }

                homePage.NavigateTo();
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
        }
    }
}
