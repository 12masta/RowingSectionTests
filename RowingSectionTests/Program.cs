using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace RowingSectionTests
{
    class Program
    {
        static void Main(string[] args)
        {         
        }       
    }

    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    public class TestClass<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        [OneTimeSetUp]
        public void CreateDriver()
        {
            try
            {
                PropertiesCollection.driver = new TWebDriver();
                Console.WriteLine("Opened browser");
                PropertiesCollection.driver.Url = "http://localhost:81/";
                Console.WriteLine("Opened URL");
                PropertiesCollection.driver.Manage().Window.Maximize();
                //initialize test data from excel sheet
               

            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.ToString());
            }                                
        }

        [OneTimeTearDown]
        public void FixtureTearDown()
        {
            if (PropertiesCollection.driver != null) PropertiesCollection.driver.Quit();
        }

        [TearDown]
        public void TearDown()
        {
            //Take screen on failure
            if (TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Failed))
            {
                string fileName = Regex.Replace(TestContext.CurrentContext.Test.FullName + "_" + DateTime.Now.ToString(), "[^a-z0-9\\-_]+", "_", RegexOptions.IgnoreCase);
                ((ITakesScreenshot)PropertiesCollection.driver).GetScreenshot().SaveAsFile(@"c:\users\bolec\documents\visual studio 2015\Projects\RowingSectionTests\RowingSectionTests\Screenshots\" + fileName + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        //will always passed
        [Test]
        public void OpenHomePage()
        {           
            HomePageObjects homeObj = new HomePageObjects();
        }

        [Test]
        public void Login()
        {
            ExcelLib.PopulateInCollection(@"c:\users\bolec\documents\visual studio 2015\Projects\RowingSectionTests\RowingSectionTests\TestData.xlsx");
            HomePageObjects homeObj = new HomePageObjects();
            LoginPageObjects loginObj = homeObj.ToLoginPage();
            loginObj.Login(ExcelLib.ReadData(1, "UserName"), ExcelLib.ReadData(1, "Password"));

            //checking is URL correct after loggin
            Assert.AreEqual("http://localhost:81/", PropertiesCollection.driver.Url.ToString());
            //checking is login is correct on navbar
            Assert.AreEqual(homeObj.GetUserLoginStringInButton(), ExcelLib.ReadData(1, "UserName").ToLower());
        }


    }

    
}
