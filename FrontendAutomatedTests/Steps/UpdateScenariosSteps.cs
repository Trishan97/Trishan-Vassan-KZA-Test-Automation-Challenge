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
    public class UpdateScenariosSteps : IDisposable
    {

        private readonly SearchContext _searchContext;
        private readonly WebdriverContext _webDriverContext;


        private readonly ScenarioContext _scenarioContext;

        public UpdateScenariosSteps(SearchContext searchContext, WebdriverContext webDriverContext, ScenarioContext scenarioContext)
        {
            _searchContext = searchContext;
            _webDriverContext = webDriverContext;
            _scenarioContext = scenarioContext;

        }
        [When(@"the user navigates to their existing update board")]
        public void WhenTheUserNavigatesToTheirExistingUpdateBoard()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(10));

            var existingBoard = _webDriverContext.driver.FindElement(By.XPath("//div[text()='Trello Update Cards']"));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[text()='Trello Update Cards']")));
            existingBoard.Click();
        }
        
        [When(@"the user can see their existing Cards on the update board")]
        public void WhenTheUserCanSeeTheirExistingCardsOnTheUpdateBoard()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Update Card']")));
        }
        
        [Then(@"the user attempts to update an existing Card")]
        public void ThenTheUserAttemptsToUpdateAnExistingCard()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(10));
            Actions action = new Actions(_webDriverContext.driver);

            Thread.Sleep(3000);

            var hoverOverUpdateCard = _webDriverContext.driver.FindElement(By.XPath("//span[text()='Update Card']"));
            action.MoveToElement(hoverOverUpdateCard).Perform();

            var updateCardButton = _webDriverContext.driver.FindElement(By.XPath("//*[@id='board']/div[1]/div/div[2]/a/span"));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='board']/div[1]/div/div[2]/a/span")));
            updateCardButton.Click();

            var updatedCardName = "Frontend Updated Card " + DateTime.Now.ToLongTimeString();
            _scenarioContext["updatedCardName"] = updatedCardName;


            var updateCardNameField = _webDriverContext.driver.FindElement(By.XPath("//*[@id='chrome-container']/div[7]/div/div[1]/div[3]/textarea"));
            updateCardNameField.SendKeys(updatedCardName);


            var saveCard = _webDriverContext.driver.FindElement(By.XPath("//*[@id='chrome-container']/div[7]/div/input"));
            wait.Until(ExpectedConditions.ElementToBeClickable(saveCard));
            saveCard.Click();

            
        }

        [Then(@"the card is successfully updated")]
        public void ThenTheCardIsSuccessfullyUpdated()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(10));
            var updatedCardName = (string)_scenarioContext["updatedCardName"];

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//span[text()='{updatedCardName}']")));

        }
        
        [Then(@"the user updates the card back to the orignal name")]
        public void ThenTheUserUpdatesTheCardBackToTheOrignalName()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(10));
            Actions action = new Actions(_webDriverContext.driver);
            Thread.Sleep(3000);

            var updatedCardName = (string)_scenarioContext["updatedCardName"];


            var hoverOverUpdateCard = _webDriverContext.driver.FindElement(By.XPath($"//span[text()='{updatedCardName}']"));
            action.MoveToElement(hoverOverUpdateCard).Perform();

            var updateCardButton = _webDriverContext.driver.FindElement(By.XPath("//*[@id='board']/div[1]/div/div[2]/a/span"));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='board']/div[1]/div/div[2]/a/span")));
            updateCardButton.Click();

            var originalCardName = "Update Card";

            var updateCardNameField = _webDriverContext.driver.FindElement(By.XPath("//*[@id='chrome-container']/div[7]/div/div[1]/div[3]/textarea"));
            updateCardNameField.SendKeys(originalCardName);


            var saveCard = _webDriverContext.driver.FindElement(By.XPath("//*[@id='chrome-container']/div[7]/div/input"));
            wait.Until(ExpectedConditions.ElementToBeClickable(saveCard));
            saveCard.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//span[text()='{originalCardName}']")));

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
