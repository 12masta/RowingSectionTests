using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowingSectionTests
{
    class LoginPageObjects
    {
        public LoginPageObjects()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        [FindsBy(How = How.Id, Using = "UserName")]
        public IWebElement txtLogin { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement txtPassword { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='loginForm']/form/div/div[4]/div/input")]
        public IWebElement btnLogin { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='loginForm']/form/div/div[1]")]
        public IWebElement msgError { get; set; }

        public string ErrorMessage()
        {
            return msgError.Text;
        }

        public HomePageObjects Login(string name, string password)
        {
            txtLogin.EnterText(name);
            txtPassword.EnterText(password);
            btnLogin.Clicks();

            return new HomePageObjects();
        }
    }
}
