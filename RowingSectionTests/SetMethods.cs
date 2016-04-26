using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowingSectionTests
{
    public static class SetMethods
    {
        //enter text method
        public static void EnterText(this IWebElement element, string value)
        {
            element.SendKeys(value);
        }

        //click on button, checkbox, option
        public static void Clicks(this IWebElement element)
        {
            element.Click();
        }

        //clicks on button, checkbox, option
        public static void ActionsClicks(this IWebElement element)
        {
            Actions actions = new Actions(PropertiesCollection.driver);
            actions.MoveToElement(element).Click().Perform();
        }


        //selecting dropdown control
        public static void SelectDropDowm(this IWebElement element, string value)
        {
            new SelectElement(element).SelectByText(value);
        }
    }
}
