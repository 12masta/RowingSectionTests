using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowingSectionTests
{
    public static class GetMethods
    {
        public static string GetText(this IWebElement element)
        {
            return element.GetAttribute("value");
        }


        public static string GetTextDromDDL(this IWebElement element)
        {
            return new SelectElement(element).AllSelectedOptions.SingleOrDefault().Text;
        }

        public static string returnTableCellValue(this IWebElement element, string tableRowIdentifier, int targetCellIndex /*string TableID, string TableRowIdentifier, int targetCellIndex*/)
        {
            string cellValue = string.Empty;
            try
            {
                IWebElement baseTable = element;
                // gets all table rows
                ICollection<IWebElement> rows = baseTable.FindElements(By.TagName("tr"));
                // for every row
                foreach (IWebElement row in rows)
                {
                    if (row.FindElement(By.XPath("//span[text()='" + tableRowIdentifier + "']")).Displayed)
                    {
                        Console.WriteLine("row identifier found!");
                        IWebElement key = row.FindElement(By.XPath("//td[" + targetCellIndex + "]"));
                        IWebElement keySpan = key.FindElement(By.TagName("span"));
                        cellValue = keySpan.Text;
                    }
                }
                return cellValue;
            }

            catch (Exception ex)
            {
                Console.WriteLine("returnTableCellValue exception: " + ex.ToString());
                return string.Empty;
            }
        }    
    }
}
