using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowingSectionTests
{
    class ContestConfirmDeletePageObjects
    {
        public ContestConfirmDeletePageObjects()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div/div/form/div/input")]
        public IWebElement btnDelete { get; set; }

        public ContestListPageObjects ConfirmDelete()
        {
            btnDelete.Clicks();

            return new ContestListPageObjects();
        }
    }
}
