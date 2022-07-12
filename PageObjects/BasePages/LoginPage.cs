using BaseWebUIAutomation.Support;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace BaseWebUIAutomation.PageObjects
{
    internal class LoginPage : BasePage
    {
      

        public LoginPage(IWebDriver driver) : base(driver)
        {

        }

        public void LogIn()
        {
            Driver.Navigate().GoToUrl(TestConfiguration.GetSectionAndValue("Settings", "url"));
           
        }



       

    }
}
