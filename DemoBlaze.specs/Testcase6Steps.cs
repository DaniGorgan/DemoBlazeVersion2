using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using DemoBlaze.specs.PageObjectModels;

namespace DemoBlaze.specs
{
    [Binding, Scope(Feature = "Testcase6")]
    public class Testcase6Steps
    {
        public static IWebDriver driver;

        [Given(@"I am on the homepage")]
        public void GivenIAmOnTheHomepage()
        {
            driver = new ChromeDriver();
            var homePage = new HomePage(driver);
            homePage.NavigateTo();
        }
        
        [Then(@"the correct page title should be displayed")]
        public void ThenTheCorrectPageTitleShouldBeDisplayed()
        {
            var homePage = new HomePage(driver);
            homePage.EnsurePageLoaded();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
        }
    }
}
