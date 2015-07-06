using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTestNorigin
{
    public class HomePage
    {
        //Navigateto homepage from another page
        public static void NavigateTo(IWebDriver driver, string Url)
        {
            driver.Navigate().GoToUrl(Url);

        }

        //Select Top Menu Item
        public static void SelectLeftMenuItem(IWebDriver driver, LocatorType menuLocatorType, string menulocator, LocatorType menuitemLocatorType, string menuitemLocator)
        {
            
            ControlAccessMethods.Click(driver, menuLocatorType, menulocator);
            ControlAccessMethods.Click(driver, menuitemLocatorType, menuitemLocator);
        }

        //Enter Text in search textbox
        public static void Search(IWebDriver driver, string searchterm, LocatorType locatortype, string searchtextboxlocator)
        {
            ControlAccessMethods.EnterText(driver, locatortype, searchtextboxlocator, searchterm);

        }

      

    }
}
