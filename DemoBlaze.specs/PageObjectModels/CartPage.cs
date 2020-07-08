using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public void pressCheckout() => Driver.FindElement(By.ClassName("btn-success")).Click();

        public void completeOrderForm()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));

            Driver.FindElement(By.Id("name")).SendKeys("Test User");
            Driver.FindElement(By.Id("country")).SendKeys("Romania");
            Driver.FindElement(By.Id("city")).SendKeys("Timisoara");
            Driver.FindElement(By.Id("card")).SendKeys("1234 5678 1234 5678");
            Driver.FindElement(By.Id("month")).SendKeys("July");
            Driver.FindElement(By.Id("year")).SendKeys("2020");
            Thread.Sleep(1000);

            Driver.FindElement(By.CssSelector("#orderModal > div > div > div.modal-footer > button.btn.btn-primary")).Click();

            Thread.Sleep(1000);
        }

        public void confirmOrder()
        {
            Driver.FindElement(By.XPath("/html/body/div[10]/div[7]/div/button")).Click();

            Thread.Sleep(1000);
        }

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(CartUrl);
            Driver.Manage().Window.Maximize();
            EnsurePageLoaded();
        }

        public void EnsurePageLoaded()
        {
            bool pageHasLoaded = (Driver.Url == CartUrl) && (Driver.Title == CartTitle);

            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = '{Driver.Url}' Page Source: \r\n {Driver.PageSource}");
            }
        }
    }
}
