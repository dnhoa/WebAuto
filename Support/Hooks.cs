using System;
using System.IO;
using System.Reflection;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace BaseWebUIAutomation.Support
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        public IWebDriver _driver;
        

        public Hooks(IObjectContainer objectContainer)
        {
            
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "TestResults"));

            //Initialise extend report before test starts
            /*var htmlreporter = new ExtentHtmlReporter(@"C:\extentreport\extentreport");
            htmlreporter.Config.Theme = AventStack.ExtentReports.Reporter.ConfigurableReporter();*/
        }

        [BeforeScenario(Order = 0)]
        public void BeforeScenario()
        {
            GetDriver();
            _objectContainer.RegisterInstanceAs(_driver);

        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                TakeScreenshot(scenarioContext);
            }

            _driver?.Dispose();
        }

        /// <summary>
        /// Take screenshot of the page when test script is failing
        /// </summary>
        /// <param name="scenarioContext"></param>
        private void TakeScreenshot(ScenarioContext scenarioContext)
        {
            try
            {
                Screenshot ss = ((ITakesScreenshot) _driver).GetScreenshot();
                ss.SaveAsFile(Path.Combine(Environment.CurrentDirectory, $"{scenarioContext.ScenarioInfo.Title}.png"),
                    ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Create and initialize driver
        /// </summary>
        /// <returns></returns>

        public IWebDriver GetDriver()
        {
            // Get browser to be used for testing from appsettings.json
            var browser = TestConfiguration.GetSectionAndValue("BrowserOptions", "Browser");
            if (_driver == null)
            {
                switch (browser)
                {
                    case "chrome":
                        ChromeOptions chromeOptions = new ChromeOptions();
                        chromeOptions.AddArgument("--window-size=1920,1080");
                                                
                       

                       
                        chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
                        chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");



                        // Get value for headless option from appsettings.json

                        var headless = TestConfiguration.GetSectionAndValue("BrowserOptions", "Headless");

                        if (headless == "true")
                        {
                            chromeOptions.AddArgument("--headless");
                        }

                        _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),chromeOptions);
                        
                        break;

                    case "firefox":
                        FirefoxOptions firefoxOptions = new FirefoxOptions();
                        firefoxOptions.AddArgument("--window-size=1920,1080");

                        var foxheadless = TestConfiguration.GetSectionAndValue("BrowserOptions", "FoxHeadless");

                        if (foxheadless == "true")
                        {
                            firefoxOptions.AddArgument("--headless");
                        }

                        _driver = new FirefoxDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), firefoxOptions);
                        break;

                }

                try
                {
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                    _driver.Manage().Cookies.DeleteAllCookies();
                    _driver.Manage().Window.Maximize();
                    _objectContainer.RegisterInstanceAs(_driver);
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message + " Driver failed to initialize");
                }
            }
            

            return _driver;
        }
    }
}