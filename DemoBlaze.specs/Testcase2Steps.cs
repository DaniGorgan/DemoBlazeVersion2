using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using DemoBlaze.specs.PageObjectModels;

namespace DemoBlaze.specs
{
    [Binding, Scope(Feature = "Testcase2")]   //am rulat acest test si da fail
    public class Testcase2Steps
    {
        public static IWebDriver driver;
        //FeedbackCristianPopescu
        //use a guid for creating new users. this way you are sure that each time you run the test you have a different name
        //something like this:
        private string _guidUsername = Guid.NewGuid().ToString("N");
        public string newUsername = "TestUser3";//pentru testele de register e bine sa se foloseasca useri unici la fiecare rulare, poti genera un user unic folosind o logica in a crea useruname-ul (vezi metoda Random())

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
