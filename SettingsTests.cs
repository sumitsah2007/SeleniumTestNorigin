using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.IO;


namespace SeleniumTestNorigin
{   
    [TestFixture]
    public class SettingsTests
    {
        IWebDriver driver;
        [SetUp]
        public void SetupTest()
        {

            string pathToCurrentUserProfiles = Environment.ExpandEnvironmentVariables("%APPDATA%") + @"\Mozilla\Firefox\Profiles"; // Path to profile
            string[] pathsToProfiles = Directory.GetDirectories(pathToCurrentUserProfiles, "*.default", SearchOption.TopDirectoryOnly);
            if (pathsToProfiles.Length != 0)
            {
                FirefoxProfile profile = new FirefoxProfile(pathsToProfiles[0]);
                profile.SetPreference("browser.tabs.loadInBackground", false); // set preferences you need
                driver = new FirefoxDriver(new FirefoxBinary(), profile, TimeSpan.FromSeconds(3000));
            }
            else
            {
                driver = new FirefoxDriver();
            }

            driver.Url = "http://norigin-test2.noriginmedia.com/norigin/";

            Thread.Sleep(3000);
            ControlAccessMethods.LogIn(driver, "470000001", "12345678");
            Thread.Sleep(3000);
            Thread.Sleep(3000);
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {


                //driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }

        }
        [Test]
        public void Settings_100_12_4()
        {
            HomePage.SelectLeftMenuItem(driver, LocatorType.XPath, "//div[@id='zone-top']/div/div/div", LocatorType.Id, "settings");
            driver.FindElement(By.CssSelector("span.option.off"));
            if (driver.FindElement(By.CssSelector("span.option.off")).GetAttribute("class").Contains("selected"))
            {
                driver.FindElement(By.CssSelector("span.option.on")).Click();
                driver.FindElement(By.CssSelector("div.confirm-button.button > div.content")).Click();
                driver.FindElement(By.CssSelector("div.confirm-button.button > div.content")).Click();
                ControlAccessMethods.LogOut(driver);
                Thread.Sleep(3000);
                ControlAccessMethods.LogIn(driver, "470000001", "12345678");
                HomePage.SelectLeftMenuItem(driver, LocatorType.XPath, "//div[@id='zone-top']/div/div/div", LocatorType.Id, "settings");
                Assert.AreEqual(true, driver.FindElement(By.CssSelector("span.option.on")).GetAttribute("class").Contains("selected"));
            }
            else
            {


                driver.FindElement(By.CssSelector("span.option.off")).Click();
                ControlAccessMethods.LogOut(driver);
                Thread.Sleep(3000);
                ControlAccessMethods.LogIn(driver, "470000001", "12345678");
                HomePage.SelectLeftMenuItem(driver, LocatorType.XPath, "//div[@id='zone-top']/div/div/div", LocatorType.Id, "settings");
                Assert.AreEqual(true, driver.FindElement(By.CssSelector("span.option.off")).GetAttribute("class").Contains("selected"));
            
            
            }

        
        }

        [Test]
        public void Settings_100_12_5()
        {
            HomePage.SelectLeftMenuItem(driver, LocatorType.XPath, "//div[@id='zone-top']/div/div/div", LocatorType.Id, "settings");
            Assert.AreEqual(true, driver.FindElement(By.ClassName("devices")).Text.Contains("automation"));
        
        }

        [Test]
        public void Settings_100_12_6()
        {
            HomePage.SelectLeftMenuItem(driver, LocatorType.XPath, "//div[@id='zone-top']/div/div/div", LocatorType.Id, "settings");
           
            ReadOnlyCollection<IWebElement> lists=driver.FindElements(By.ClassName("list"));

            ReadOnlyCollection<IWebElement> values = lists[2].FindElements(By.ClassName("value"));
            Assert.AreEqual(true, values[0].Text.Length!=0);

        }

        [Test]
        public void Settings_100_12_7()
        {
            HomePage.SelectLeftMenuItem(driver, LocatorType.XPath, "//div[@id='zone-top']/div/div/div", LocatorType.Id, "settings");

            ReadOnlyCollection<IWebElement> lists = driver.FindElements(By.ClassName("list"));

            ReadOnlyCollection<IWebElement> values = lists[2].FindElements(By.ClassName("value"));
            Assert.AreEqual("nortest1", values[1].Text);


        }

       
    }
}
