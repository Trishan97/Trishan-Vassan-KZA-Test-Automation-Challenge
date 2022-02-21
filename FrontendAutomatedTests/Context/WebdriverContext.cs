using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendAutomatedTests.Context
{
    public class WebdriverContext
    {
        public ChromeDriver driver;

        public WebdriverContext()
        {
            driver = new ChromeDriver();

           
        }
    }
}
