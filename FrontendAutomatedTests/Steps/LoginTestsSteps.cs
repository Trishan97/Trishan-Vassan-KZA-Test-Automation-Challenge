using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;
using FrontendAutomatedTests.Context;
using System.Threading;
using FrontendAutomatedTests.Consts;


namespace FrontendAutomatedTests.Steps
{
    [Binding]
    public class LoginTestsSteps : IDisposable
    {
        private readonly SearchContext _searchContext;
        private readonly WebdriverContext _webDriverContext;


        public LoginTestsSteps(SearchContext searchContext, WebdriverContext webDriverContext)
        {
            _searchContext = searchContext;
            _webDriverContext = webDriverContext;
        }



        [Given(@"the user has navigated to Trello's Website")]
        public void GivenTheUserHasNavigatedToTrelloSWebsite()
        {
            string trelloURL = UIParamValues.TrelloUrl;
            _webDriverContext.driver.Navigate().GoToUrl(trelloURL);
            Assert.IsTrue(_webDriverContext.driver.Title.ToLower().Contains("trello"));

        }

        [When(@"the user logs in with valid credentials")]
        public void WhenTheUserLogsInWithValidCredentials()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(10));

            var emailInput = _webDriverContext.driver.FindElement(By.XPath("//*[@id='user']"));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='user']")));
            emailInput.SendKeys(UIParamValues.ValidUserEmail);

            var atlassianloginButton = _webDriverContext.driver.FindElement(By.XPath("//*[@id='login']"));
            atlassianloginButton.Click();
            Thread.Sleep(5000);

            var paswordInput = _webDriverContext.driver.FindElement(By.XPath("//*[@id='password']"));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='password']")));
            paswordInput.SendKeys(UIParamValues.ValidUserPassword);

            var loginButton = _webDriverContext.driver.FindElement(By.XPath("//span[text()='Log in']"));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Log in']")));
            loginButton.Click();


        }

        [Then(@"the user should be logged in and taken to the Trello Homepage")]
        public void ThenTheUserShouldBeLoggedInAndTakenToTheTrelloHomepage()
        {

            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h3[text()='YOUR WORKSPACES']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[text()='Most popular templates']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Boards']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Templates']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Home']")));


        }

        [When(@"the user attempts to log in with invalid credentials")]
        public void WhenTheUserAttemptsToLogInWithInvalidCredentials()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(10));

            var emailInput = _webDriverContext.driver.FindElement(By.XPath("//*[@id='user']"));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='user']")));
            emailInput.SendKeys(UIParamValues.InvalidUserEmail);

            var loginButton = _webDriverContext.driver.FindElement(By.XPath("//*[@id='login']"));
            loginButton.Click();
            
        }

        [Then(@"the user is not logged in and is shown an error message")]
        public void ThenTheUserIsNotLoggedInAndIsShownAnErrorMessage()
        {
            WebDriverWait wait = new WebDriverWait(_webDriverContext.driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[text()=\"There isn\'t an account for this username\"]")));

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
