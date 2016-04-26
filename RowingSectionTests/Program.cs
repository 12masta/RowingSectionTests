using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Globalization;

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
        [SetUp]
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
                ExcelLib.PopulateInCollection(@"c:\users\bolec\documents\visual studio 2015\Projects\RowingSectionTests\RowingSectionTests\TestData.xlsx");
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.ToString());
            }                                
        }

        [OneTimeTearDown]
        public void FixtureTearDown()
        {            
            //homeObj.Logoff();
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
        public void LoginIncorrectCredentials()
        {
            HomePageObjects homeObj = new HomePageObjects();
            LoginPageObjects loginObj = homeObj.ToLoginPage();
            loginObj.Login(ExcelLib.ReadData(2, "UserName"), ExcelLib.ReadData(2, "Password"));

            //checking is URL correct after loggin
            Assert.AreEqual("http://localhost:81/Account/Login", PropertiesCollection.driver.Url.ToString());
            //checking is login is correct on navbar
            Assert.AreEqual("Niepoprawny login lub hasło.", loginObj.ErrorMessage());
        }

        //login with correct credentials will login to acc
        [Test]
        public void Login()
        {
            HomePageObjects homeObj = new HomePageObjects();
            LoginPageObjects loginObj = homeObj.ToLoginPage();
            loginObj.Login(ExcelLib.ReadData(1, "UserName"), ExcelLib.ReadData(1, "Password"));

            //checking is URL correct after loggin
            Assert.AreEqual("http://localhost:81/", PropertiesCollection.driver.Url.ToString());
            //checking is login is correct on navbar
            Assert.AreEqual(homeObj.GetUserLoginStringInButton().ToLower(), ExcelLib.ReadData(1, "UserName").ToLower());
        }


        [Test]
        public void AddContest()
        {
            Login();
            HomePageObjects homeObj = new HomePageObjects();
            homeObj.ShowDropdownList();
            ContestListPageObjects contestListObj = homeObj.GoToContestListPage();
            ContestAddPageObject contestAddPageObj = contestListObj.AddContest();
            contestListObj = contestAddPageObj.CreateContest(ExcelLib.ReadData(1, "ContestName"), ExcelLib.ReadData(1, "ContestDate").Remove(10), ExcelLib.ReadData(1, "ContestDescription"));
            contestListObj = contestListObj.SearchContest(ExcelLib.ReadData(1, "ContestName"));

            Assert.AreEqual(contestListObj.GetNameOfContest(), ExcelLib.ReadData(1, "ContestName"));
            Assert.AreEqual(contestListObj.GetDateOfContest(), ExcelLib.ReadData(1, "ContestDate").Remove(10));
            Assert.AreEqual(contestListObj.GetDescriptionOfContest(), ExcelLib.ReadData(1, "ContestDescription"));
        }

        [Test]
        public void DeleteContest()
        {
            Login();
            HomePageObjects homeObj = new HomePageObjects();
            homeObj.ShowDropdownList();
            ContestListPageObjects contestListObj = homeObj.GoToContestListPage();
            contestListObj = contestListObj.SearchContest(ExcelLib.ReadData(1, "ContestName"));
            ContestConfirmDeletePageObjects contestConfirmDelObj = contestListObj.DeleteContest();
            contestListObj = contestConfirmDelObj.ConfirmDelete();
            contestListObj = contestListObj.SearchContest(ExcelLib.ReadData(1, "ContestName"));

            Assert.AreEqual(contestListObj.GetNameOfContest(), "");
            Assert.AreEqual(contestListObj.GetDateOfContest(), "");
            Assert.AreEqual(contestListObj.GetDescriptionOfContest(), "");
        }
    }  
}
