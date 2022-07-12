using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace BaseWebUIAutomation.PageObjects
{
    //Page related geniric functions
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void Navigate(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void RefreshPage()
        {
            Driver.Navigate().Refresh();
        }

        public void CloseBrowser()
        {
            Driver.Close();
        }

        public string GetPageUrl()
        {
            return Driver.Url;
        }

        public bool IsPageLoaded(By pageLocator)
        {
            try
            {
                return Driver.FindElement(pageLocator).Displayed;
            }
            catch (Exception)
            {
                Console.WriteLine($@"No page found with locator: {pageLocator}");
                throw;
            }
        }

       
    }
}
