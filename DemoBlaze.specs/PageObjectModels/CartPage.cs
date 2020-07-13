using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace DemoBlaze.specs.PageObjectModels
{
    class CartPage
    {
        private readonly IWebDriver Driver;

        public const string CartUrl = "https://www.demoblaze.com/cart.html";
        public const string CartTitle = "STORE";

        public CartPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void pressCheckout()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(6));

            //this sleep is needed here, checkout button is not active and cannot be clicked if not for this second
            Thread.Sleep(1000);
            IWebElement checkoutButton = wait.Until((d) => Driver.FindElement(By.ClassName("btn-success")));
            checkoutButton.Click();
        }

        public void completeOrderForm(string NameOfUser, string CountryOfUser, string CityOfUser, string CardNumber, string Month, string Year)
            // faptul ca ai facut o metoda pentru acest form care poate fi refolosita e un lucru bun, insa e indicat sa o faci parametrizata
            // iar datele sa nu fie harcodate ci date ca parametru, asa devine mult mai utila daca vrei sa o folosesti in alte teste si cu alte date, etc
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));

            // Pentru Page Object Model identificarea elementelor e indicat sa se faca inafara metodelor
            // La fel cum ai facut cu CartUrl, CartTitle
            // apoi in metodele unde ai nevoie de element il folosesti. 

            //FeedbackCristianPopescu
            //remove thread.sleep. Try to use explicit wait if you need to wait for something
            //you can use wait referenced above

            //this sleep is necessary since the fields aren't active yet, wait.Until from below doesn't seem to do the trick, need more info

            Thread.Sleep(1000);

            wait.Until((d) => Driver.FindElement(By.Id("name")));

            Driver.FindElement(By.Id("name")).SendKeys(NameOfUser);
            Driver.FindElement(By.Id("country")).SendKeys(CountryOfUser);
            Driver.FindElement(By.Id("city")).SendKeys(CityOfUser);
            Driver.FindElement(By.Id("card")).SendKeys(CardNumber);
            Driver.FindElement(By.Id("month")).SendKeys(Month);
            Driver.FindElement(By.Id("year")).SendKeys(Year);
            // using sleeps is not a best practice, try to use Wait until when you find elements

            IWebElement completeOrderButton = wait.Until((d) => Driver.FindElement(By.CssSelector("#orderModal > div > div > div.modal-footer > button.btn.btn-primary")));
            completeOrderButton.Click();
        }

        public void confirmOrder()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));

            //FeedbackCristianPopescu
            //remove thread.sleep
            Thread.Sleep(1000);

            IWebElement confirmOrderButton = wait.Until((d) => Driver.FindElement(By.XPath("/html/body/div[10]/div[7]/div/button")));
            confirmOrderButton.Click();

        }

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(CartUrl);
            Driver.Manage().Window.Maximize();
            EnsurePageLoaded();
        }

        public bool EnsurePageLoaded()
        {
            bool pageHasLoaded = (Driver.Url == CartUrl) && (Driver.Title == CartTitle);

            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = '{Driver.Url}' Page Source: \r\n {Driver.PageSource}");
            }

            return pageHasLoaded;
        }
    }
}
