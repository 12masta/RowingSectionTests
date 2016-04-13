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



    }
}
