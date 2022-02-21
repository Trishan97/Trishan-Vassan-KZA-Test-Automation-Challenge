using System;
using TechTalk.SpecFlow;
using FrontendAutomatedTests.Context;
using TechTalk.SpecFlow.Infrastructure;
using System.IO;
using OpenQA.Selenium;

namespace FrontendAutomatedTests.StepDefinitions
{
    [Binding]
    public class FrontendTestHooks 

    {
        private readonly SearchContext _searchContext;
        private readonly WebdriverContext _webDriverContext;

        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;


        public FrontendTestHooks(SearchContext searchContext, WebdriverContext webDriverContext, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _searchContext = searchContext;
            _webDriverContext = webDriverContext;
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _specFlowOutputHelper.WriteLine("Browser Launched");
            _webDriverContext.driver.Manage().Window.Maximize();

        }



        [AfterScenario("NegativeTest")]
        public void AfterStep()
        {
            var filename = Path.ChangeExtension("FailedTestScreenshot", "png");

            //screenshotTaker.GetScreenshot().SaveAsFile(filename);
            var screenshot = ((ITakesScreenshot)_webDriverContext.driver).GetScreenshot();
            screenshot.SaveAsFile(filename, ScreenshotImageFormat.Png);

            _specFlowOutputHelper.AddAttachment(filename);


        }

       
        

    }
}
