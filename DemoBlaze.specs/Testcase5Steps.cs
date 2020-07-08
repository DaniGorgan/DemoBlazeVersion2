using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;
using DemoBlaze.specs.PageObjectModels;

namespace DemoBlaze.specs
{
    [Binding, Scope(Feature = "Testcase5")]
    public class Testcase5Steps
    {
        public static IWebDriver driver;

        public string itemDisplayed;

        [Given(@"I am on the homepage")]
        public void GivenIAmOnTheHomepage()
        {
            driver = new ChromeDriver();
            var homePage = new HomePage(driver);
            homePage.NavigateTo();

            itemDisplayed = homePage.getCarouselItemName();
        }

        [When(@"I click on the “Previous” button from Image Slider")]
        public void WhenIClickOnThePreviousButtonFromImageSlider()
        {
            var homePage = new HomePage(driver);

            homePage.clickCarouselButton("carousel-control-prev");
            Thread.Sleep(1000);
        }

        [When(@"I click on the “Next” button from Image Slider")]
        public void WhenIClickOnTheNextButtonFromImageSlider()
        {
            var homePage = new HomePage(driver);

            homePage.clickCarouselButton("carousel-control-next");
            Thread.Sleep(1000);
        }

        [Then(@"I see a different product")]
        public void ThenISeeADifferentProduct()
        {
            var homePage = new HomePage(driver);
            Assert.NotEqual(itemDisplayed, homePage.getCarouselItemName());

            itemDisplayed = homePage.getCarouselItemName();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
        }
    }
}
