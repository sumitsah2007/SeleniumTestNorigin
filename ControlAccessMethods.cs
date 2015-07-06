using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTestNorigin
{
    public enum LocatorType
    {
        Id,
        Name,
        CssSelector,
        ClassName,
        XPath,
        PartialLinkText,
        TagName
    }

    public class ControlAccessMethods
    {
        //Send Text input based on locator 
        public static void EnterText(IWebDriver driver, LocatorType locatortype, string elementlocator, string value)
        {
            switch (locatortype)
            {
                case LocatorType.Id: driver.FindElement(By.Id(elementlocator)).SendKeys(value); break;
                case LocatorType.Name: driver.FindElement(By.Name(elementlocator)).SendKeys(value); break;
                case LocatorType.CssSelector: driver.FindElement(By.CssSelector(elementlocator)).SendKeys(value); break;
                case LocatorType.ClassName: driver.FindElement(By.ClassName(elementlocator)).SendKeys(value); break;
                case LocatorType.XPath: driver.FindElement(By.XPath(elementlocator)).SendKeys(value); break;
                case LocatorType.PartialLinkText: driver.FindElement(By.PartialLinkText(elementlocator)).SendKeys(value); break;
                case LocatorType.TagName: driver.FindElement(By.TagName(elementlocator)).SendKeys(value); break;

            }


        }

        //Method to check if existence of an element on current page
        private static bool ExistsElement(IWebDriver driver, LocatorType locatortype, string locator)
        {
            try
            {
                switch (locatortype)
                {
                    case LocatorType.Id: driver.FindElement(By.Id(locator)); break;
                    case LocatorType.Name: driver.FindElement(By.Name(locator)); break;
                    case LocatorType.CssSelector: driver.FindElement(By.CssSelector(locator)); break;
                    case LocatorType.ClassName: driver.FindElement(By.ClassName(locator)); break;
                    case LocatorType.XPath: driver.FindElement(By.XPath(locator)); break;
                    case LocatorType.PartialLinkText: driver.FindElement(By.PartialLinkText(locator)); break;
                    case LocatorType.TagName: driver.FindElement(By.TagName(locator)); break;

                }

            }
            catch (NoSuchElementException e)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        //Method to click on a CheckBox/Button
        public static void Click(IWebDriver driver, LocatorType locatortype, string elementlocator)
        {
            switch (locatortype)
            {
                case LocatorType.Id: driver.FindElement(By.Id(elementlocator)).Click(); break;
                case LocatorType.Name: driver.FindElement(By.Name(elementlocator)).Click(); break;
                case LocatorType.CssSelector: driver.FindElement(By.CssSelector(elementlocator)).Click(); break;
                case LocatorType.ClassName: driver.FindElement(By.ClassName(elementlocator)).Click(); break;
                case LocatorType.XPath: driver.FindElement(By.XPath(elementlocator)).Click(); break;
                case LocatorType.PartialLinkText: driver.FindElement(By.PartialLinkText(elementlocator)).Click(); break;
                case LocatorType.TagName: driver.FindElement(By.TagName(elementlocator)).Click(); break;

            }
            Thread.Sleep(2000);
        }

        //Method to select value in a drop down control
        public static void SelectDropDown(IWebDriver driver, LocatorType locatortype, string elementlocator, string value)
        {
            switch (locatortype)
            {
                case LocatorType.Id: new SelectElement(driver.FindElement(By.Id(elementlocator))).SelectByText(value); break;
                case LocatorType.Name: new SelectElement(driver.FindElement(By.Name(elementlocator))).SelectByText(value); break;
                case LocatorType.CssSelector: new SelectElement(driver.FindElement(By.CssSelector(elementlocator))).SelectByText(value); break;
                case LocatorType.ClassName: new SelectElement(driver.FindElement(By.ClassName(elementlocator))).SelectByText(value); break;
                case LocatorType.XPath: new SelectElement(driver.FindElement(By.XPath(elementlocator))).SelectByText(value); break;
                case LocatorType.PartialLinkText: new SelectElement(driver.FindElement(By.PartialLinkText(elementlocator))).SelectByText(value); break;
                case LocatorType.TagName: new SelectElement(driver.FindElement(By.TagName(elementlocator))).SelectByText(value); break;

            }
        }

        //LogIn
        public static void LogIn(IWebDriver driver, string username, string password)
        {

            HomePage.SelectLeftMenuItem(driver, LocatorType.XPath, "//div[@id='zone-top']/div/div/div", LocatorType.Id, "login");
            Thread.Sleep(2000);
            ControlAccessMethods.EnterText(driver, LocatorType.Id, "username", username);
            ControlAccessMethods.EnterText(driver, LocatorType.Id, "password", password);
            ControlAccessMethods.Click(driver, LocatorType.CssSelector, "button.button.login-button");
        }

        public static void LogOut(IWebDriver driver)
        {
            HomePage.SelectLeftMenuItem(driver, LocatorType.XPath, "//div[@id='zone-top']/div/div/div", LocatorType.Id, "logout");
        }
    }
}
