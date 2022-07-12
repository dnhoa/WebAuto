using BaseWebUIAutomation.PageObjects;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace InfrastructureWebUIAutomation.StepDefinitions
{
    [Binding]
    public sealed class LoginSteps
    {
        
        private readonly LoginPage _loginPage;
        
        public LoginSteps(IWebDriver driver)
        {
            
            _loginPage = new LoginPage(driver);
            
        }

        [Given(@"the user is logged in")]
        public void GivenTheUserIsLoggedIn()
        {
            
            _loginPage.LogIn();
           
        }
    }
}
