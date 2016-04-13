using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowingSectionTests
{
    class HomePageObjects
    {
        public HomePageObjects()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        [FindsBy(How = How.Id, Using = "loginLink")]
        public IWebElement btnToLogin { get; set; }

        public LoginPageObjects ToLoginPage()
        {
            btnToLogin.Clicks();
        
            return new LoginPageObjects();
        }

    }
}
