using FrontendAutomatedTests.Context;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;

namespace FrontendAutomatedTests.Steps
{
    [Binding]
    public class GetScenarioSteps : IDisposable
    {
        private readonly SearchContext _searchContext;
        private readonly WebdriverContext _webDriverContext;


        private readonly ScenarioContext _scenarioContext;

        public GetScenarioSteps(SearchContext searchContext, WebdriverContext webDriverContext, ScenarioContext scenarioContext)
        {
            _searchContext = searchContext;
            _webDriverContext = webDriverContext;
            _scenarioContext = scenarioContext;

        }


        [Given(@"the user can see their existing board")]
        public void GivenTheUserCanSeeTheirBoard()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Trishan Vassan KZA Assessment']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Highlights']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Members']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Settings']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[text()='Trello FrontEnd Testing']")));



        }

        [When(@"the user navigates to their existing board")]
        public void WhenTheUserNavigatesToTheirExistingBoard()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(10));
          
            var existingBoard = _webDriverContext.driver.FindElement(By.XPath("//div[text()='Trello FrontEnd Testing']"));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[text()='Trello FrontEnd Testing']")));
            existingBoard.Click();

        }

        [Then(@"the user can see their existing Cards")]
        public void ThenTheUserCanSeeTheirExistingCards()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='To Do Card']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Doing Card']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Blocked Card']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='IN QA Card']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Done Card']")));
        }



        public void Dispose()
        {
            if (_webDriverContext.driver != null)
            {
                _webDriverContext.driver.Dispose();
                _webDriverContext.driver = null;
            }
        }
    }
}
