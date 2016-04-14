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

        [FindsBy(How = How.CssSelector, Using = "ul.nav:nth-child(2) > li:nth-child(1) > a:nth-child(1)")]
        public IWebElement btnUserDropdown { get; set; }

        public string GetUserLoginStringInButton()
        {
            string login = btnUserDropdown.Text;
            login = login.Remove(login.Length - 1);
            return string.Join(string.Empty, login.Skip(7));
        }

        public LoginPageObjects ToLoginPage()
        {
            btnToLogin.Clicks();
        
            return new LoginPageObjects();
        }
    }
}
