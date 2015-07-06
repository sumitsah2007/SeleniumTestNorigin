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
    public class ParentalControlTests
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
            ControlAccessMethods.LogIn(driver, "470000005", "12345678");
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
        public void ParentalControl_100_13_2()
        {
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("settings")).Click();
            Thread.Sleep(3000);
            IWebElement dropdown = driver.FindElement(By.CssSelector("select.switcher"));
            ReadOnlyCollection<IWebElement> options = dropdown.FindElements(By.TagName("Option"));
            int changedrating = int.Parse(dropdown.GetAttribute("value").Split('@')[0]) + 1;

            
            SelectElement setrating = new SelectElement(dropdown);
            setrating.SelectByText(changedrating.ToString());
            driver.FindElement(By.CssSelector("div.form-row > input.textbox")).SendKeys("1111");

            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("div.button.confirm-button > div.content")).Click();

            Thread.Sleep(2000);
            dropdown = driver.FindElement(By.CssSelector("select.switcher"));
            Assert.AreEqual(changedrating.ToString(), dropdown.GetAttribute("value").Split('@')[0]);
        
        
        }

        [Test]
        public void ParentalControl_100_13_5()
        {

            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("settings")).Click();
            Thread.Sleep(3000);
            IWebElement dropdown = driver.FindElement(By.CssSelector("select.switcher"));
            ReadOnlyCollection<IWebElement> options = dropdown.FindElements(By.TagName("Option"));
            string originalrating = dropdown.GetAttribute("value").Split('@')[0];
            int changedrating = int.Parse(dropdown.GetAttribute("value").Split('@')[0]) + 1;


            SelectElement setrating = new SelectElement(dropdown);
            setrating.SelectByText(changedrating.ToString());
            driver.FindElement(By.CssSelector("div.form-row > input.textbox")).SendKeys("xxxx");
            

            
            driver.FindElement(By.CssSelector("div.button.confirm-button > div.content")).Click();

            Thread.Sleep(2000);
            Assert.AreEqual(true, driver.FindElement(By.CssSelector("div.loading-error-message > div.error-message")).Text.Contains("Invalid pin"));
            driver.FindElement(By.ClassName("close-button"));

            Thread.Sleep(6000);
            ControlAccessMethods.LogOut(driver);
            ControlAccessMethods.LogIn(driver, "nortest1", "nortest1");
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("settings")).Click();
            Thread.Sleep(3000);
            dropdown = driver.FindElement(By.CssSelector("select.switcher"));
            Assert.AreEqual(originalrating, dropdown.GetAttribute("value").Split('@')[0]);
        
        
        
        }

        [Test]
        public void ParentalControl_100_13_6()
        {

            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("settings")).Click();
            Thread.Sleep(3000);
            IWebElement dropdown = driver.FindElement(By.CssSelector("select.switcher"));
            ReadOnlyCollection<IWebElement> options = dropdown.FindElements(By.TagName("Option"));
            string originalrating = dropdown.GetAttribute("value").Split('@')[0];
            int changedrating = int.Parse(dropdown.GetAttribute("value").Split('@')[0]) + 1;


            SelectElement setrating = new SelectElement(dropdown);
            setrating.SelectByText(changedrating.ToString());
            driver.FindElement(By.CssSelector("div.form-row > input.textbox")).SendKeys("");



            driver.FindElement(By.CssSelector("div.button.confirm-button > div.content")).Click();

            Thread.Sleep(2000);
            Assert.AreEqual(true, driver.FindElement(By.CssSelector("div.loading-error-message > div.error-message")).Text.Contains("Pin validation failed. Please try again"));
            driver.FindElement(By.ClassName("close-button"));

            Thread.Sleep(6000);
            ControlAccessMethods.LogOut(driver);
            ControlAccessMethods.LogIn(driver, "nortest1", "nortest1");
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("settings")).Click();
            Thread.Sleep(3000);
            dropdown = driver.FindElement(By.CssSelector("select.switcher"));
            Assert.AreEqual(originalrating, dropdown.GetAttribute("value").Split('@')[0]);
        
        }

        [Test]
        public void ParentalControl_100_13_3()
        {
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();

            Thread.Sleep(3000);
            driver.FindElement(By.Id("tvod")).Click();

            Thread.Sleep(3000);
            ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.ClassName("gallery-content"));

            //loop thorugh the items to find a purchasable item

            Thread.Sleep(3000);

            

            int ratedmovieindex = 0;
            for (int i = 0; i < driver.FindElements(By.ClassName("gallery-content")).Count; i++)
            {
                driver.FindElements(By.ClassName("gallery-content"))[i].Click();

                try
                {


                    if (int.Parse(driver.FindElement(By.CssSelector("div.parental-rating > div.value")).Text) == 15)
                    {
                        ratedmovieindex = i;
                        driver.Navigate().Back();
                        break;
                    }
                }
                catch (Exception e) { }


                driver.Navigate().Back();


            }
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("settings")).Click();
            Thread.Sleep(3000);


            ControlAccessMethods.SelectDropDown(driver, LocatorType.CssSelector, "select.switcher", "14");
            // SelectElement setrating = new SelectElement(dropdown);
            //setrating.SelectByText(changedrating.ToString());
            driver.FindElement(By.CssSelector("div.form-row > input.textbox")).SendKeys("1111");

            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("div.button.confirm-button > div.content")).Click();

            Thread.Sleep(9000);
            ControlAccessMethods.LogOut(driver);
            ControlAccessMethods.LogIn(driver, "nortest1", "nortest1");
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();

            Thread.Sleep(3000);
            driver.FindElement(By.Id("tvod")).Click();

            Thread.Sleep(3000);
            string moviename = driver.FindElements(By.ClassName("gallery-content"))[ratedmovieindex].Text.Split('\r')[0];
            driver.FindElements(By.ClassName("gallery-content"))[ratedmovieindex].Click();


            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("div.form-row > input.textbox")).SendKeys("1111");
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("div.button.confirm-button > div.content")).Click();
            Thread.Sleep(6000);
            Assert.AreEqual(true, driver.FindElement(By.CssSelector("div.title")).Text.Contains(moviename));
          
        
        }

        [Test]
        public void ParentalControl_100_13_7()
        {
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();

            Thread.Sleep(3000);
            driver.FindElement(By.Id("tvod")).Click();

            Thread.Sleep(3000);
            ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.ClassName("gallery-content"));

            //loop thorugh the items to find a purchasable item

            Thread.Sleep(3000);



            int ratedmovieindex = 0;
            for (int i = 0; i < driver.FindElements(By.ClassName("gallery-content")).Count; i++)
            {
                driver.FindElements(By.ClassName("gallery-content"))[i].Click();

                try
                {


                    if (int.Parse(driver.FindElement(By.CssSelector("div.parental-rating > div.value")).Text) < 14 )
                    {
                        ratedmovieindex = i;
                        driver.Navigate().Back();
                        break;
                    }
                }
                catch (Exception e) { }


                driver.Navigate().Back();


            }
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("settings")).Click();
            Thread.Sleep(3000);


            ControlAccessMethods.SelectDropDown(driver, LocatorType.CssSelector, "select.switcher", "14");
            // SelectElement setrating = new SelectElement(dropdown);
            //setrating.SelectByText(changedrating.ToString());
            driver.FindElement(By.CssSelector("div.form-row > input.textbox")).SendKeys("1234");

            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("div.button.confirm-button > div.content")).Click();

            Thread.Sleep(9000);
            ControlAccessMethods.LogOut(driver);
            ControlAccessMethods.LogIn(driver, "nortest1", "nortest1");
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();

            Thread.Sleep(3000);
            driver.FindElement(By.Id("tvod")).Click();

            Thread.Sleep(3000);
            string moviename = driver.FindElements(By.ClassName("gallery-content"))[ratedmovieindex].Text.Split('\r')[0];
            driver.FindElements(By.ClassName("gallery-content"))[ratedmovieindex].Click();


            
            Thread.Sleep(6000);
            Assert.AreEqual(true, driver.FindElement(By.CssSelector("div.title")).Text.Contains(moviename));


        }

        [Test]
        public void ParentalControl_100_13_4()
        {
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();

            Thread.Sleep(3000);
            driver.FindElement(By.Id("tvod")).Click();

            Thread.Sleep(3000);
            ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.ClassName("gallery-content"));

            //loop thorugh the items to find a purchasable item

            Thread.Sleep(3000);


            int agerating = 0;
            int ratedmovieindex = 0;
            for (int i = 0; i < driver.FindElements(By.ClassName("gallery-content")).Count; i++)
            {
                driver.FindElements(By.ClassName("gallery-content"))[i].Click();

                try
                {


                    if (int.Parse(driver.FindElement(By.CssSelector("div.parental-rating > div.value")).Text) < 14)
                    {
                        ratedmovieindex = i;
                        agerating = int.Parse(driver.FindElement(By.CssSelector("div.parental-rating > div.value")).Text);
                        driver.Navigate().Back();
                        break;
                    }
                }
                catch (Exception e) { }


                driver.Navigate().Back();


            }
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("settings")).Click();
            Thread.Sleep(3000);


            ControlAccessMethods.SelectDropDown(driver, LocatorType.CssSelector, "select.switcher", agerating.ToString());
            // SelectElement setrating = new SelectElement(dropdown);
            //setrating.SelectByText(changedrating.ToString());
            driver.FindElement(By.CssSelector("div.form-row > input.textbox")).SendKeys("1234");

            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("div.button.confirm-button > div.content")).Click();

            Thread.Sleep(9000);
            ControlAccessMethods.LogOut(driver);
            ControlAccessMethods.LogIn(driver, "470000005", "12345678");
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//div[@id='zone-top']/div/div/div")).Click();

            Thread.Sleep(3000);
            driver.FindElement(By.Id("tvod")).Click();

            Thread.Sleep(3000);
            string moviename = driver.FindElements(By.ClassName("gallery-content"))[ratedmovieindex].Text.Split('\r')[0];
            driver.FindElements(By.ClassName("gallery-content"))[ratedmovieindex].Click();



            Thread.Sleep(6000);
            Assert.AreEqual(true, driver.FindElement(By.CssSelector("div.title")).Text.Contains(moviename));


        }

    }
}
