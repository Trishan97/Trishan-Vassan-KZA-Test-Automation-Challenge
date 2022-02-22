using FrontendAutomatedTests.Context;
using NUnit.Framework;
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
    public class CreateScenariosSteps : IDisposable
    {
        private readonly SearchContext _searchContext;
        private readonly WebdriverContext _webDriverContext;


        private readonly ScenarioContext _scenarioContext;

        public CreateScenariosSteps(SearchContext searchContext, WebdriverContext webDriverContext, ScenarioContext scenarioContext)
        {
            _searchContext = searchContext;
            _webDriverContext = webDriverContext;
            _scenarioContext = scenarioContext;

        }


        [Given(@"the user clicks on the ""(.*)"" option")]
        public void GivenTheUserClicksOnTheOption(string createBoard)
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(15));

            var createBoardButton = _webDriverContext.driver.FindElement(By.XPath($"//span[text()='{createBoard}']"));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//span[text()='{createBoard}']")));
            createBoardButton.Click();

            Thread.Sleep(3000);

        }

        [When(@"the ""(.*)"" modal appears")]
        public void WhenTheModalAppears(string createBoard)
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(15));

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[text()='{createBoard}']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//label[text()='Background']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//label[text()='Visibility']")));

        }

        [When(@"the user fills in all required fields")]
        public void WhenTheUserFillsInAllRequiredFields()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(15));

            var boardName = "Frontend Created Board " + DateTime.Now.ToLongTimeString();
            _scenarioContext["boardName"] = boardName;

            var boardTitle = _webDriverContext.driver.FindElement(By.XPath("//form/div[1]/label/input"));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//form/div[1]/label/input")));
            boardTitle.SendKeys(boardName);

            var createBoardButton = _webDriverContext.driver.FindElement(By.XPath("//button[text()='Create']"));
            wait.Until(ExpectedConditions.ElementToBeClickable(createBoardButton));
            createBoardButton.Click();

        }

        [When(@"the user does not fill in all required fields")]
        public void WhenTheUserDoesNotFillInAllRequiredFields()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(15));

            var boardColour = _webDriverContext.driver.FindElement(By.XPath("//*[@id='background-picker']/ul[2]/li[2]/button"));
            wait.Until(ExpectedConditions.ElementToBeClickable(boardColour));
            boardColour.Click();

           

        }


        [Then(@"the user is taken to the newly created board")]
        public void ThenTheUserIsTakenToTheNewlyCreatedBoard()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(15));
            Thread.Sleep(3000);

            var expadNav = _webDriverContext.driver.FindElement(By.XPath("//div/div[1]/nav/div[2]/div/button"));
            wait.Until(ExpectedConditions.ElementToBeClickable(expadNav));
            expadNav.Click();

            var boardName = (string)_scenarioContext["boardName"];

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//h1[text()='{boardName}']")));

        }

        

       
        [Then(@"the user is unable to create a new Board")]
        public void ThenTheUserIsUnableToCreateANewBoard()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(15));

            wait.Until(ExpectedConditions.ElementExists(By.XPath("//p[text()='Board title is required']")));

            var createBoardButton = _webDriverContext.driver.FindElement(By.XPath("//button[text()='Create']"));
            Assert.False(createBoardButton.Enabled);
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
