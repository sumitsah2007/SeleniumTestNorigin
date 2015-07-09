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
{   [TestFixture]
    public class AnnoymousAccess
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

            driver.Url = "http://norigin-test.noriginmedia.com/norigin/";

            Thread.Sleep(6000);
           
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {


                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }

        }

        [Test]
        public void AnnoymousAccess_100_1_1()
        {

            HomePage.SelectLeftMenuItem(driver, LocatorType.XPath, "//div[@id='zone-top']/div/div/div", LocatorType.Id, "live");
            Thread.Sleep(3000);
            Assert.AreEqual("LOG IN", driver.FindElement(By.CssSelector("div.header > div.title")).Text);
            Assert.AreEqual("LOG IN", driver.FindElement(By.CssSelector("button.button.login-button")).Text);
        
        
        
        }

        [Test]
        public void AnnoymousAccess_100_1_2()
        {
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();
            Thread.Sleep(2000);
            Assert.AreEqual(true,ControlAccessMethods.ExistsElement(driver, LocatorType.Id, "login"));
        
        
        }

        [Test]
        public void AnnoymousAccess_100_1_3()
        {
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();
            Thread.Sleep(2000);
            Assert.AreEqual(true, ControlAccessMethods.ExistsElement(driver, LocatorType.Id, "guide"));
            driver.FindElement(By.Id("guide")).Click();
            Thread.Sleep(3000);
            Assert.AreEqual(true,ControlAccessMethods.ExistsElement(driver, LocatorType.Id, "guide-now"));
            Assert.AreEqual(true, ControlAccessMethods.ExistsElement(driver, LocatorType.CssSelector, "#horizontal-scroll-view > div.left-button"));
            Assert.AreEqual(true, ControlAccessMethods.ExistsElement(driver, LocatorType.CssSelector, "#horizontal-scroll-view > div.right-button"));
            driver.FindElement(By.CssSelector("#horizontal-scroll-view > div.left-button")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("#horizontal-scroll-view > div.left-button")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("#horizontal-scroll-view > div.left-button")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("#horizontal-scroll-view > div.right-button")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("#horizontal-scroll-view > div.right-button")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("#horizontal-scroll-view > div.right-button")).Click();
            Thread.Sleep(6000);
            


        }

        [Test]
        public void AnnoymousAccess_100_1_4()
        {
            HomePage.SelectLeftMenuItem(driver, LocatorType.XPath, "//div[@id='zone-top']/div/div/div", LocatorType.Id, "guide");
            Thread.Sleep(3000);
           

            string title = string.Empty;

            for (int i = 0; i < driver.FindElements(By.ClassName("live")).Count; i = i + 10)
            {

                title = (driver.FindElements(By.ClassName("live")))[i].Text.Split('\n')[1];
                driver.FindElements(By.ClassName("live"))[i].Click();
                Thread.Sleep(10000);
                Assert.AreEqual(true, driver.FindElement(By.ClassName("parental-rating")).Text.Length > 10);

                Assert.AreEqual(true, title.Contains(driver.FindElement(By.CssSelector("div.left-column > div.title")).Text));
                Assert.AreEqual(true, ControlAccessMethods.ExistsElement(driver, LocatorType.ClassName, "watch-button"));

                driver.FindElement(By.ClassName("watch-button")).Click();

                Assert.AreEqual("LOG IN", driver.FindElement(By.CssSelector("div.header > div.title")).Text);
                Assert.AreEqual("LOG IN", driver.FindElement(By.CssSelector("button.button.login-button")).Text);

                driver.FindElement(By.ClassName("close-button")).Click();

                driver.Navigate().Back();
                Thread.Sleep(2000);

            }
            
           


        }

        [Test]
        public void AnnoymousAccess_100_1_5()
        {

            Assert.AreEqual(true, ControlAccessMethods.ExistsElement(driver, LocatorType.Id, "catchup"));

        
        }

        [Test]
        public void AnnoymousAccess_100_1_6()
        {

            HomePage.SelectLeftMenuItem(driver, LocatorType.XPath, "//div[@id='zone-top']/div/div/div", LocatorType.Id, "tvod");

            Thread.Sleep(3000);

            string title = string.Empty;

            for (int i = 0; i < driver.FindElements(By.ClassName("gallery-content")).Count; i = i + 5)
            {

                title = driver.FindElements(By.ClassName("gallery-content"))[i].Text.Split('\r')[0];
                driver.FindElements(By.ClassName("gallery-content"))[i].Click();
                Thread.Sleep(1000);
                Assert.AreEqual(true, ControlAccessMethods.ExistsElement(driver, LocatorType.ClassName, "login-button"));
                Assert.AreEqual(title, driver.FindElement(By.CssSelector("div.title")).Text);
                driver.Navigate().Back();
                Thread.Sleep(2000);

            }

        }

        [Test]
        public void AnnoymousAccess_100_1_7()
        {
            HomePage.SelectLeftMenuItem(driver, LocatorType.XPath, "//div[@id='zone-top']/div/div/div", LocatorType.Id, "svod");

            Thread.Sleep(3000);

            string title = string.Empty;

            for (int i = 0; i < driver.FindElements(By.ClassName("gallery-content")).Count; i = i + 5)
            {

                title = driver.FindElements(By.ClassName("gallery-content"))[i].Text.Split('\r')[0];
                driver.FindElements(By.ClassName("gallery-content"))[i].Click();
                Thread.Sleep(1000);
                Assert.AreEqual(true, ControlAccessMethods.ExistsElement(driver, LocatorType.ClassName, "login-button"));
                Assert.AreEqual(title, driver.FindElement(By.CssSelector("div.title")).Text);
                driver.Navigate().Back();
                Thread.Sleep(2000);

            }
        
        
        
        
        }

        [Test]
        public void AnnoymousAccess_100_1_8()
        {
            HomePage.SelectLeftMenuItem(driver, LocatorType.XPath, "//div[@id='zone-top']/div/div/div", LocatorType.Id, "shop");

            Thread.Sleep(3000);

            driver.FindElements(By.ClassName("content-item"))[0].Click();
            string title = string.Empty;

            for (int i = 0; i < driver.FindElements(By.ClassName("gallery-content")).Count; i = i + 5)
            {

                title = driver.FindElements(By.ClassName("gallery-content"))[i].Text.Split('\r')[0];
                driver.FindElements(By.ClassName("gallery-content"))[i].Click();
                Thread.Sleep(1000);
                Assert.AreEqual(true, ControlAccessMethods.ExistsElement(driver,LocatorType.ClassName,"login-button")) ;
                Assert.AreEqual(title, driver.FindElement(By.CssSelector("div.title")).Text);
                driver.Navigate().Back();
                Thread.Sleep(2000);

            }





        }

        
        
    }

}
