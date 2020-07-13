using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit.Abstractions;
using DemoBlaze.specs.PageObjectModels;

namespace DemoBlaze.specs
{
    [Binding, Scope(Feature = "Testcase4")]
    public class Testcase4Steps
    {
        public static IWebDriver driver;
        private readonly ITestOutputHelper output;

        public Testcase4Steps(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Given(@"I am on the homepage")]
        public void GivenIAmOnTheHomepage()
        {
            driver = new ChromeDriver();
            var homePage = new HomePage(driver);
            homePage.NavigateTo();
        }

        [When(@"I filter by “(.*)”")]
        public void WhenIFilterBy(string filter)
        {
            var homePage = new HomePage(driver);
            homePage.filterBy(filter);
        }
        
        [Then(@"I can see in the test output the mean value of each product")]
        public void ThenICanSeeInTheTestOutputTheMeanValueOfEachProduct()
        {
            var homePage = new HomePage(driver);
            ReadOnlyCollection<IWebElement> _products = homePage.returnAllElementsFiltered();
            int numberOfProducts = _products.Count();
            //FeedbackCristianPopescu
            //you need to do an average of products prices: e.g. Laptops.
            output.WriteLine("total number of filtered products: " + numberOfProducts); // nu vad exact unde ai afisat media. Vad doar numarul total de produse
                                                                                        // trebuie corectat aici si afisat media nu numarul  total de produse

        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
        }
    }
}