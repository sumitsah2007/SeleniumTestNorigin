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
    public class TVODPurchaseTests
    {
       IWebDriver driver;
       ReadOnlyCollection<IWebElement> elements;
       string itemname = string.Empty;
       string itemdetails = string.Empty;

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

            Thread.Sleep(3000);
            ControlAccessMethods.LogIn(driver, "nortest1", "nortest1");
            Thread.Sleep(3000);
            
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                elements = null;
                string itemname = string.Empty;
                string itemdetails = string.Empty;
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
      
        }

        [Test]
        public void TVOD_100_8_3()
        {
            //Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();
          
            driver.FindElement(By.Id("tvod")).Click();
         
            driver.FindElement(By.XPath("//div[@id='tvod']/div[2]/div/div[2]/div/div/div/div/div[3]/div/div/div/div")).Click();

            Assert.AreEqual("Taken", driver.FindElement(By.CssSelector("div.title")).Text);
            driver.Navigate().Back();
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*SEE ALL[\\s\\S]*$"));
        }
        
        [Test]
        public void TVOD_100_8_4()
          {
            
            HomePage.SelectLeftMenuItem(driver,LocatorType.XPath,"//div[@id='zone-top']/div/div/div" ,LocatorType.Id,"tvod");

            Thread.Sleep(3000);
            //selecting a purchasable item
            for (int count = 0; count <= driver.FindElements(By.ClassName("gallery-content")).Count; count++)
            {
                itemdetails = driver.FindElements(By.ClassName("gallery-content"))[count].Text;
                driver.FindElements(By.ClassName("gallery-content"))[count].Click();

                Thread.Sleep(2000);
                try
                {

                    driver.FindElement(By.CssSelector("div.button.purchase-button > div.content")).Click();

                    break;
                }
                catch (ElementNotVisibleException)
                {
                    itemdetails = string.Empty;
                    driver.Navigate().Back();
                    Thread.Sleep(3000);

                }

            }

            Thread.Sleep(3000);

            driver.FindElement(By.CssSelector("div.button.purchase-button > div.content")).Click();

            Thread.Sleep(3000);

            Assert.AreEqual(true,driver.FindElement(By.CssSelector("div.pricing-option > div.value")).Text.Length!=0);
            
          }

        [Test]
        public void TVOD_100_8_5()
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("tvod")).Click();
            Thread.Sleep(3000);
            for (int count = 0; count <= driver.FindElements(By.ClassName("gallery-content")).Count; count++)
            {
                itemdetails = driver.FindElements(By.ClassName("gallery-content"))[count].Text;
                driver.FindElements(By.ClassName("gallery-content"))[count].Click();

                Thread.Sleep(2000);
                try
                {

                    driver.FindElement(By.CssSelector("div.button.purchase-button > div.content")).Click();

                    break;
                }
                catch (ElementNotVisibleException)
                {
                    itemdetails = string.Empty;
                    driver.Navigate().Back();
                    Thread.Sleep(3000);

                }

            }

            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//div[2]/div[2]/div[5]/div")).Click();
            
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector("div.button.confirm-button > div.content")).Click();
            Thread.Sleep(2000);
            Assert.AreEqual(true, driver.FindElement(By.CssSelector("div.loading-error-message > div.error-message")).Text.Contains("Pin validation failed. Please try again"));
           
             
        }

        [Test]
        public void TVOD_100_8_6()
        {
            Thread.Sleep(3000);
           driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();
           Thread.Sleep(3000);
           driver.FindElement(By.Id("tvod")).Click();
           Thread.Sleep(3000);
           for (int count = 0; count <= driver.FindElements(By.ClassName("gallery-content")).Count; count++)
           {
               itemdetails = driver.FindElements(By.ClassName("gallery-content"))[count].Text;
               driver.FindElements(By.ClassName("gallery-content"))[count].Click();

               Thread.Sleep(2000);
               try
               {

                   driver.FindElement(By.CssSelector("div.button.purchase-button > div.content")).Click();

                   break;
               }
               catch (ElementNotVisibleException)
               {
                   itemdetails = string.Empty;
                   driver.Navigate().Back();
                   Thread.Sleep(3000);

               }

           }

           Thread.Sleep(2000);


           Thread.Sleep(2000);
           driver.FindElement(By.XPath("//div[2]/div[2]/div[5]/div")).Click();
           Thread.Sleep(2000);
           driver.FindElement(By.CssSelector("div.form-row > input.textbox")).SendKeys("1245");
           Thread.Sleep(2000);
           driver.FindElement(By.CssSelector("div.button.confirm-button > div.content")).Click();
           Thread.Sleep(2000);
           Assert.AreEqual(true, driver.FindElement(By.CssSelector("div.loading-error-message > div.error-message")).Text.Contains("Invalid pin"));

       }

        [Test]
        public void TVOD_100_8_7()
        {

            Thread.Sleep(3000);
            
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();

            Thread.Sleep(3000);
            driver.FindElement(By.Id("tvod")).Click();

            Thread.Sleep(3000);

           

            for (int count = 0; count < driver.FindElements(By.ClassName("gallery-content")).Count; count++)
            {
                itemdetails = driver.FindElements(By.ClassName("gallery-content"))[count].Text;
                driver.FindElements(By.ClassName("gallery-content"))[count].Click();

                Thread.Sleep(2000);
                try
                {

                    driver.FindElement(By.CssSelector("div.button.purchase-button > div.content")).Click();

                    break;
                }
                catch (ElementNotVisibleException)
                {
                    itemdetails = string.Empty;
                    driver.Navigate().Back();
                    Thread.Sleep(3000);

                }
           
            }
            
           

            //loop thorugh the items to find a purchasable item
            

            Thread.Sleep(3000);

            itemname = itemdetails.Split('\r')[0];

            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div[2]/div[2]/div[5]/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector("div.form-row > input.textbox")).SendKeys("1111");
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector("div.button.confirm-button > div.content")).Click();
            Thread.Sleep(20000);
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("purchases")).Click();

            Thread.Sleep(3000);
            Assert.AreEqual(true,driver.FindElement(By.ClassName("purchase-list")).Text.Contains(itemname));
}
           
    }
}
