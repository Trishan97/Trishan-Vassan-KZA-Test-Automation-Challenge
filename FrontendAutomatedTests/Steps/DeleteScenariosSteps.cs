using FrontendAutomatedTests.Context;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace FrontendAutomatedTests.Steps
{
    [Binding]
    public class DeleteScenariosSteps : IDisposable
    {

        private readonly SearchContext _searchContext;
        private readonly WebdriverContext _webDriverContext;


        private readonly ScenarioContext _scenarioContext;

        public DeleteScenariosSteps(SearchContext searchContext, WebdriverContext webDriverContext, ScenarioContext scenarioContext)
        {
            _searchContext = searchContext;
            _webDriverContext = webDriverContext;
            _scenarioContext = scenarioContext;

        }

        [Then(@"the user is able to close and Delete the newly created board")]
        public void ThenIsAbleToDeleteTheNewlyCreatedBoard()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(30));
            Actions action = new Actions(_webDriverContext.driver);
            var boardName = (string)_scenarioContext["boardName"];
            Thread.Sleep(4000);

            var hoverOverNewBoard = _webDriverContext.driver.FindElement(By.XPath($"//p[text()='{boardName}']"));
            action.MoveToElement(hoverOverNewBoard).Perform();

            var closeBoardActionButton = _webDriverContext.driver.FindElement(By.XPath($"//p[text()='{boardName}']/../div/button"));
            wait.Until(ExpectedConditions.ElementToBeClickable(closeBoardActionButton));
            closeBoardActionButton.Click();

            var closeBoardButton = _webDriverContext.driver.FindElement(By.XPath("//span[text()='Close board...']"));
            wait.Until(ExpectedConditions.ElementToBeClickable(closeBoardButton));
            closeBoardButton.Click();

            var confirmClose = _webDriverContext.driver.FindElement(By.XPath("//button[text()='Close']"));
            wait.Until(ExpectedConditions.ElementToBeClickable(confirmClose));
            confirmClose.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//h1[text()='{boardName} is closed.']")));

            var permanentlyDeleteButton = _webDriverContext.driver.FindElement(By.XPath("//button[text()='Permanently delete board']"));
            wait.Until(ExpectedConditions.ElementToBeClickable(permanentlyDeleteButton));
            permanentlyDeleteButton.Click();

            var confirmDelete = _webDriverContext.driver.FindElement(By.XPath("//button[text()='Delete']"));
            wait.Until(ExpectedConditions.ElementToBeClickable(confirmDelete));
            confirmDelete.Click();

        }


        [Then(@"the user can verify that the board no longer exists")]
        public void ThenUserCanVerifyThatTheBoardNoLongerExists()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(30));
            var boardName = (string)_scenarioContext["boardName"];
            Thread.Sleep(3000);

            var searchBar = _webDriverContext.driver.FindElement(By.XPath("//input[@placeholder='Search']"));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Search']")));
            searchBar.Click();

            var externalSearchButton = _webDriverContext.driver.FindElement(By.XPath("//*[@id='header']/div[4]/div[1]/span[2]/a/span/span"));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='header']/div[4]/div[1]/span[2]/a/span/span")));
            externalSearchButton.Click();
            Thread.Sleep(3000);

            var searchBarInput = _webDriverContext.driver.FindElement(By.XPath("//*[@id='content']/div[1]/div/input"));
            searchBarInput.SendKeys(boardName);

            string noResults = "\"We couldn\'t find any cards or boards that matched your search.\"";
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[text()={noResults}]")));

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
