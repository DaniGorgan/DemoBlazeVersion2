using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using DemoBlaze.specs.PageObjectModels;

namespace DemoBlaze.specs
{
    [Binding, Scope(Feature = "Testcase2")]
    public class Testcase2Steps
    {
        public static IWebDriver driver;

        public string newUsername = "TestUser3";
        public string newPassword = "TempPassword";

        [Given(@"I am on the homepage")]
        public void GivenIAmOnTheHomepage()
        {
            driver = new ChromeDriver();
            var homePage = new HomePage(driver);
            homePage.NavigateTo();
        }

        [Given(@"I click on Sign Up button")]
        public void GivenIClickOnSignUpButton()
        {
            var homePage = new HomePage(driver);
            homePage.signup();
        }
        
        [When(@"I fill in required data")]
        public void WhenIFillInRequiredData()
        {
            var homePage = new HomePage(driver);
            homePage.fillSignUpForm(newUsername, newPassword);
        }
        
        [Then(@"I get registered")]
        public void ThenIGetRegistered()
        {
            var homePage = new HomePage(driver);
            homePage.ensureSignUpCompleted();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
        }
    }
}
