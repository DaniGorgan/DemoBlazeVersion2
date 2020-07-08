using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using TechTalk.SpecFlow;
using DemoBlaze.specs.PageObjectModels;

namespace DemoBlaze.specs
{
    [Binding, Scope(Feature = "Testcase3")]
    public class Testcase3Steps
    {
        public static IWebDriver driver;

        public string username = "TestUser1";
        public string password = "TempPassword";

        [Given(@"I am on the homepage")]
        public void GivenIAmOnTheHomepage()
        {
            driver = new ChromeDriver();
            var homePage = new HomePage(driver);
            homePage.NavigateTo();
        }

        [When(@"I click on the login button")]
        public void WhenIClickOnTheLoginButton()
        {
            var homePage = new HomePage(driver);
            homePage.clickLoginButton();
            Thread.Sleep(1000);
        }
        
        [When(@"I enter my credentials")]
        public void WhenIEnterMyCredentials()
        {
            var homePage = new HomePage(driver);
            homePage.loginEnterCredentials(username, password);
        }
        
        [Then(@"I get logged in")]
        public void ThenIGetLoggedIn()
        {
            var homePage = new HomePage(driver);
            string welcomeText = "Welcome " + username;

            homePage.ensureLoginPerformed(welcomeText);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
        }
    }
}
