using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using Xunit;

namespace DemoBlaze.specs.PageObjectModels
{
    public class HomePage
    {
        private readonly IWebDriver Driver;

        public const string HomeUrl = "https://www.demoblaze.com/";
        public const string HomeTitle = "STORE";

        public HomePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void clickLoginButton() => Driver.FindElement(By.Id("login2")).Click();

        public void loginEnterCredentials(string Username, string Password)
        {
            Driver.FindElement(By.Id("loginusername")).SendKeys(Username);
            Driver.FindElement(By.Id("loginpassword")).SendKeys(Password);

            //remove thread.sleep
            Thread.Sleep(1000);

            Driver.FindElement(By.XPath("//button[contains(text(), 'Log in')]")).Click();
            Thread.Sleep(2000);
        }

        public void ensureLoginPerformed(string concatenatedString)
        {
            string welcomeText = Driver.FindElement(By.Id("nameofuser")).Text;
            Assert.Equal(concatenatedString, welcomeText);
        }

        public void filterBy(string filterText)
        {
            Driver.FindElement(By.XPath("//a[contains(.,'" + filterText + "')]")).Click();
            Thread.Sleep(1000);
        }

        public void signup()
        {
            Driver.FindElement(By.Id("signin2")).Click();
            Thread.Sleep(1000);
        }

        public void fillSignUpForm(string newUsername, string newPassword)
        {
            Driver.FindElement(By.Id("sign-username")).SendKeys(newUsername);
            Driver.FindElement(By.Id("sign-password")).SendKeys(newPassword);
            Thread.Sleep(1000);
        }
        public void ensureSignUpCompleted()
        {
            Driver.FindElement(By.XPath("//button[contains(text(), 'Sign up')]")).Click();

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            IAlert alertBox = wait.Until(ExpectedConditions.AlertIsPresent());

            Assert.Equal("Sign up successful.", alertBox.Text);
            alertBox.Dismiss();
        }

        public string getCarouselItemName()
        {
            return Driver.FindElement(By.CssSelector("#carouselExampleIndicators > div > div.carousel-item.active > img")).GetProperty("alt");
        }

        public void clickCarouselButton(string buttonToBePressed) => Driver.FindElement(By.ClassName(buttonToBePressed)).Click();

        public void selectItem(int itemToBeSelected) => Driver.FindElement(By.CssSelector("#tbodyid > div:nth-child(" + itemToBeSelected + ") > div > a > img")).Click();

        //method at line 83 be simplified like this. Also, try naming the methods to start with Uppercase letter
        //public ReadOnlyCollection<IWebElement> ReturnAllElementsFiltered2() => Driver.FindElements(By.XPath("//*[@id='tbodyid']/div"));

        public ReadOnlyCollection<IWebElement> returnAllElementsFiltered() => Driver.FindElements(By.XPath("//*[@id='tbodyid']/div"));

        public string getProductName_NotFiltered(int index)
        {
            return Driver.FindElement(By.CssSelector("#tbodyid > div:nth-child(" + index + ") > div > div > h4 > a")).Text;
        }

        public string getProductName(int index)
        {
            return Driver.FindElement(By.CssSelector("#tbodyid > div:nth-child(" + index + ") > div > a > img")).Text;
        }

        public string getProductPrice(int index)
        {
            return Driver.FindElement(By.CssSelector("#tbodyid > div:nth-child(" + index + ") > div > div > h5")).Text.Substring(1);
        }

        public void addItemToCart(int index)
        {
            Driver.FindElement(By.CssSelector("#tbodyid > div:nth-child(" + index + ") > div > a > img")).Click();
            Thread.Sleep(1000);

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));

            Driver.FindElement(By.CssSelector("#tbodyid > div.row > div > a")).Click();
            Thread.Sleep(1000);

            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

            //to check difference between logged in and not logged in
            //this can be done as well like this:
            Assert.Contains("Product added", alert.Text);
            //Assert.Equal("Product added", alert.Text);

            Thread.Sleep(1000);

            alert.Accept();
        }

        public string addItemToCartPartialLink(string textToSearch)
        {
            Driver.FindElement(By.PartialLinkText(textToSearch)).Click();
            Thread.Sleep(1000);

            string productName = Driver.FindElement(By.CssSelector("#tbodyid > h2")).Text;

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));

            Driver.FindElement(By.CssSelector("#tbodyid > div.row > div > a")).Click();
            Thread.Sleep(1000);

            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

            Assert.Equal("Product added", alert.Text);

            Thread.Sleep(1000);

            alert.Accept();

            return productName;
        }


        //the following 2 methods could be extracted into a Common class, and used throughout the project, when needed. I saw the same method in CartPage.cs class
        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(HomeUrl);
            Driver.Manage().Window.Maximize();
            EnsurePageLoaded();
        }

        public void EnsurePageLoaded()
        {
            bool pageHasLoaded = (Driver.Url == HomeUrl) && (Driver.Title == HomeTitle);

            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = '{Driver.Url}' Page Source: \r\n {Driver.PageSource}");
            }
        }
    }
}
