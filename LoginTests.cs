using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using NUnit.Framework;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace SeleniumTestNorigin
{
    [TestFixture]
    public class LoginTests
    {
        public IWebElement element;
        public IWebElement element1;
        public bool isLoggedIn;
        public OleDbDataAdapter DbAdap;
        public DataTable dt;
        public  Dictionary<string, string> configs = new Dictionary<string, string>();

        public void GetConfigurations()
        {
           /* OleDbConnection DbCon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Selenium\\LoginTests.xlsx;Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"");

            DbAdap = new OleDbDataAdapter("SELECT * FROM [Configurations$]", DbCon);
            dt = new DataTable();
            DbAdap.Fill(dt);


            foreach (DataRow row in dt.Rows)
            {

                configs[row["Key"].ToString()] = row["Value"].ToString();



            }
             */

            configs["BaseURL"]="http://norigin-dev2.noriginmedia.com/norigin/";
            configs["LeftMenuIcon"]="//div[@id='zone-top']/div/div/div";
            configs["LoginMenuIcon"]="#login > div.content > div.wrapper";
            configs["UserNameTextBox"]="username";
            configs["PasswordTextBox"]="password";
            configs["LoginButton"]="button.button.login-button";
            configs["DeviceNameTextBox"]="device-name";
            configs["DeviceNameConfirmation"]="button.button.confirm-button";
            configs["LogOutMenuIcon"]="";
            configs["LoginFailText"]="The username or password you entered is incorrect";
            configs["username"] = "910000001";
            configs["password"] = "12345678";
            configs["wrongpassword"] = "xxxxxxxx";
            configs["wrongusername"] = "XXXXXXXXXX";
           

        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        private bool existsElement(By by,IWebDriver driver)
        {
            try
            {
                driver.FindElement(by);
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
            return true;
        }

       
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            GetConfigurations();


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
          
            driver.Url = configs["BaseURL"];
            baseURL = configs["BaseURL"];
            verificationErrors = new StringBuilder();
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
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void BadLoginTest_UserNameWithSpaces()
        {



            //find the side menu
            Thread.Sleep(6000);

            element = driver.FindElement(By.XPath(configs["LeftMenuIcon"]));
            if (element.Displayed == true)
                element.Click();

            Thread.Sleep(1000);
            //click on login
            if (existsElement(By.CssSelector(configs["LoginMenuIcon"]), driver))
            {
                element = driver.FindElement(By.CssSelector(configs["LoginMenuIcon"]));
                if (element.Displayed == true)
                    element.Click();
                else
                {
                    Console.WriteLine("Already LoggedIn");
                    isLoggedIn = true;
                }
            }
            else
            {
                Console.WriteLine("Already LoggedIn");
                isLoggedIn = true;
            }
            Thread.Sleep(1000);

            if (isLoggedIn == false)
            {

                element = driver.FindElement(By.Id(configs["UserNameTextBox"]));
                element.SendKeys(" "+configs["username"]+" ");
                element = driver.FindElement(By.Id(configs["PasswordTextBox"]));
                element.SendKeys(configs["password"]);


                //for device registration
                element = driver.FindElement(By.CssSelector(configs["LoginButton"]));
                element.Click();


                Thread.Sleep(1000);

                if (existsElement(By.Id(configs["DeviceNameTextBox"]), driver))
                {


                    if (driver.FindElement(By.Id(configs["DeviceNameTextBox"])).Displayed == true)
                    {

                        driver.FindElement(By.Id(configs["DeviceNameTextBox"])).SendKeys("Testing");
                        driver.FindElement(By.CssSelector(configs["DeviceNameConfirmation"])).Click();

                    }

                }


                Thread.Sleep(6000);
                if (existsElement(By.CssSelector(configs["LoginButton"]), driver))
                {
                    Console.WriteLine("Login Unsuccessful ");
                  
                    Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                     ss.SaveAsFile(@"C:\Selenium\TestSnapsShots\" + GetCurrentMethod() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                }
                else
                {
                    Console.WriteLine("Login Successful ");
                    driver.FindElement(By.XPath(configs["LeftMenuIcon"])).Click();
                    Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                    ss.SaveAsFile(@"C:\Selenium\TestSnapsShots\" + GetCurrentMethod() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }

            Assert.AreEqual(true, existsElement(By.CssSelector(configs["LoginButton"]), driver));
        }
        
        [Test]
        public void BadLogin_WrongPasswordLoginTest()
        {
            
           

            //find the side menu
            Thread.Sleep(6000);
            
            element = driver.FindElement(By.XPath(configs["LeftMenuIcon"]));
            if (element.Displayed == true)
                element.Click();

            Thread.Sleep(1000);
            //click on login
            if (existsElement(By.CssSelector(configs["LoginMenuIcon"]), driver))
            {
                element = driver.FindElement(By.CssSelector(configs["LoginMenuIcon"]));
                if (element.Displayed == true)
                    element.Click();
                else
                {
                    Console.WriteLine("Already LoggedIn");
                    isLoggedIn = true;
                }
            }
            else
            {
                Console.WriteLine("Already LoggedIn");
                isLoggedIn = true;
            }
            Thread.Sleep(1000);
            //throw new Exception();

            if (isLoggedIn == false)
            {

                element = driver.FindElement(By.Id(configs["UserNameTextBox"]));
                element.SendKeys(configs["username"]);
                element = driver.FindElement(By.Id(configs["PasswordTextBox"]));
                element.SendKeys("1234567890");


                //for device registration
                element = driver.FindElement(By.CssSelector(configs["LoginButton"]));
                element.Click();


                Thread.Sleep(3000);

                if (existsElement(By.Id(configs["DeviceNameTextBox"]),driver))
                {
                    
                 
                    if (driver.FindElement(By.Id(configs["DeviceNameTextBox"])).Displayed == true)
                    {

                        driver.FindElement(By.Id(configs["DeviceNameTextBox"])).SendKeys("Testing");
                        driver.FindElement(By.CssSelector(configs["DeviceNameConfirmation"])).Click();

                    }

                }
                

                Thread.Sleep(3000);
                if (existsElement(By.CssSelector(configs["LoginButton"]), driver))
                {
                    Console.WriteLine("Login Unsuccessful ");
                    Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                     ss.SaveAsFile(@"C:\Selenium\TestSnapsShots\" + GetCurrentMethod() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                }
                else
                {
                    Console.WriteLine("Login Successful ");
                }
            }
            Assert.AreEqual(true, existsElement(By.CssSelector(configs["LoginButton"]), driver));
        }
        [Test]
        public void BadLogin_WrongUserNameLoginTest()
        {



            //find the side menu
            Thread.Sleep(6000);

            element = driver.FindElement(By.XPath(configs["LeftMenuIcon"]));
            if (element.Displayed == true)
                element.Click();

            Thread.Sleep(1000);
            //click on login
            if (existsElement(By.CssSelector(configs["LoginMenuIcon"]), driver))
            {
                element = driver.FindElement(By.CssSelector(configs["LoginMenuIcon"]));
                if (element.Displayed == true)
                    element.Click();
                else
                {
                    Console.WriteLine("Already LoggedIn");
                    isLoggedIn = true;
                }
            }
            else
            {
                Console.WriteLine("Already LoggedIn");
                isLoggedIn = true;
            }
            Thread.Sleep(1000);

            if (isLoggedIn == false)
            {

                element = driver.FindElement(By.Id(configs["UserNameTextBox"]));
                element.SendKeys("XXXXXXXX");
                element = driver.FindElement(By.Id(configs["PasswordTextBox"]));
                element.SendKeys(configs["password"]);


                //for device registration
                element = driver.FindElement(By.CssSelector(configs["LoginButton"]));
                element.Click();


                Thread.Sleep(3000);

                if (existsElement(By.Id(configs["DeviceNameTextBox"]), driver))
                {


                    if (driver.FindElement(By.Id(configs["DeviceNameTextBox"])).Displayed == true)
                    {

                        driver.FindElement(By.Id(configs["DeviceNameTextBox"])).SendKeys("Testing");
                        driver.FindElement(By.CssSelector(configs["DeviceNameConfirmation"])).Click();

                    }

                }


                Thread.Sleep(3000);
                if (existsElement(By.CssSelector(configs["LoginButton"]), driver))
                {
                    Console.WriteLine("Login Unsuccessful ");
                    Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                     ss.SaveAsFile(@"C:\Selenium\TestSnapsShots\" + GetCurrentMethod() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                }
                else
                {
                    Console.WriteLine("Login Successful ");
                }
            }
            Assert.AreEqual(true, existsElement(By.CssSelector(configs["LoginButton"]), driver));
        }
        [Test]
        public void BadLogin_EmptyUserNameAndPasswordTest()
        {



            //find the side menu
            Thread.Sleep(6000);

            element = driver.FindElement(By.XPath(configs["LeftMenuIcon"]));
            if (element.Displayed == true)
                element.Click();

            Thread.Sleep(1000);
            //click on login
            if (existsElement(By.CssSelector(configs["LoginMenuIcon"]), driver))
            {
                element = driver.FindElement(By.CssSelector(configs["LoginMenuIcon"]));
                if (element.Displayed == true)
                    element.Click();
                else
                {
                    Console.WriteLine("Already LoggedIn");
                    isLoggedIn = true;
                }
            }
            else
            {
                Console.WriteLine("Already LoggedIn");
                isLoggedIn = true;
            }
            Thread.Sleep(1000);

            if (isLoggedIn == false)
            {

                element = driver.FindElement(By.Id(configs["UserNameTextBox"]));
                element.SendKeys("");
                element = driver.FindElement(By.Id(configs["PasswordTextBox"]));
                element.SendKeys("");


                //for device registration
                element = driver.FindElement(By.CssSelector(configs["LoginButton"]));
                element.Click();


                Thread.Sleep(3000);

                if (existsElement(By.Id(configs["DeviceNameTextBox"]), driver))
                {


                    if (driver.FindElement(By.Id(configs["DeviceNameTextBox"])).Displayed == true)
                    {

                        driver.FindElement(By.Id(configs["DeviceNameTextBox"])).SendKeys("Testing");
                        driver.FindElement(By.CssSelector(configs["DeviceNameConfirmation"])).Click();

                    }

                }


                Thread.Sleep(3000);
                if (existsElement(By.CssSelector(configs["LoginButton"]), driver))
                {
                    Console.WriteLine("Login Unsuccessful ");
                    Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                     ss.SaveAsFile(@"C:\Selenium\TestSnapsShots\" + GetCurrentMethod() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                }
                else
                {
                    Console.WriteLine("Login Successful ");
                }
            }
            Assert.AreEqual(true, existsElement(By.CssSelector(configs["LoginButton"]), driver));
        }
        [Test]
        public void BadLogin_XSSTest()
        {



            //find the side menu
            Thread.Sleep(6000);

            element = driver.FindElement(By.XPath(configs["LeftMenuIcon"]));
            if (element.Displayed == true)
                element.Click();

            Thread.Sleep(3000);
            //click on login
            if (existsElement(By.CssSelector(configs["LoginMenuIcon"]), driver))
            {
                element = driver.FindElement(By.CssSelector(configs["LoginMenuIcon"]));
                if (element.Displayed == true)
                    element.Click();
                else
                {
                    Console.WriteLine("Already LoggedIn");
                    isLoggedIn = true;
                }
            }
            else
            {
                Console.WriteLine("Already LoggedIn");
                isLoggedIn = true;
            }
            Thread.Sleep(3000);

            if (isLoggedIn == false)
            {

                element = driver.FindElement(By.Id(configs["UserNameTextBox"]));
                element.SendKeys("<IMG SRC=javascript:alert(String.fromCharCode(88,83,83))>");
                element = driver.FindElement(By.Id(configs["PasswordTextBox"]));
                element.SendKeys(configs["password"]);


                //for device registration
                element = driver.FindElement(By.CssSelector(configs["LoginButton"]));
                element.Click();


                Thread.Sleep(3000);

                if (existsElement(By.Id(configs["DeviceNameTextBox"]), driver))
                {


                    if (driver.FindElement(By.Id(configs["DeviceNameTextBox"])).Displayed == true)
                    {

                        driver.FindElement(By.Id(configs["DeviceNameTextBox"])).SendKeys("Testing");
                        driver.FindElement(By.CssSelector(configs["DeviceNameConfirmation"])).Click();

                    }

                }


                Thread.Sleep(3000);
                if (existsElement(By.CssSelector(configs["LoginButton"]), driver))
                {
                    Console.WriteLine("Login Unsuccessful ");
                    Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                     ss.SaveAsFile(@"C:\Selenium\TestSnapsShots\" + GetCurrentMethod() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                }
                else
                {
                    Console.WriteLine("Login Successful ");
                }
            }
            Assert.AreEqual(true, existsElement(By.CssSelector(configs["LoginButton"]), driver));
        }
        [Test]
        public void BadLogin_1000CharsUserName()
        {



            //find the side menu
            Thread.Sleep(6000);

            element = driver.FindElement(By.XPath(configs["LeftMenuIcon"]));
            if (element.Displayed == true)
                element.Click();

            Thread.Sleep(1000);
            //click on login
            if (existsElement(By.CssSelector(configs["LoginMenuIcon"]), driver))
            {
                element = driver.FindElement(By.CssSelector(configs["LoginMenuIcon"]));
                if (element.Displayed == true)
                    element.Click();
                else
                {
                    Console.WriteLine("Already LoggedIn");
                    isLoggedIn = true;
                }
            }
            else
            {
                Console.WriteLine("Already LoggedIn");
                isLoggedIn = true;
            }
            Thread.Sleep(1000);

            if (isLoggedIn == false)
            {

                element = driver.FindElement(By.Id(configs["UserNameTextBox"]));
                element.SendKeys("Casos de Éxito Un caso de éxito es un buen ejemplo del modelo de trabajo de Acuntia en sus clientes, que es la comprensión de la necesidad del negocio y la adaptación y personalización de las tecnologías más novedosas  a ese reto tecnológico.Algunos casos pueden ser los siguientes:TELEVISIO DE CATALUNYA TV3, necesitaba una nueva infraestructura de red que le permitiera la digitalización y migración a IP de todos los procesos de producción audiovisual, incluyendo, edición, archivo y emisión de archivos de vídeo.El objetivo principal del proyecto ha sido poder disponer de una única infraestructura de red que soportase todos los procesos y protocolos que se utilizan a diario en TV3, tanto aplicaciones ofimáticas como aplicaciones de producción audiovisual. Este objetivo se ha conseguido con la nueva red, gracias a la cual los usuarios, desde sus estaciones de trabajo, pueden utilizar y acceder a los contenidos audiovisuales en las mismas condiciones en que se utilizan las herramientas ofimáticas, y ser capaces así de generar, catalogar, buscar, editar y enviar a emisión o subir a Internet cualquier contenido.Gracias a la capacidad y rendimiento de la infraestructura de red de Enterasys, el personal de TV3 puede manejar contenido audiovisual en ficheros que superan por término medio los 25 gigabytes, y que");
                element = driver.FindElement(By.Id(configs["PasswordTextBox"]));
                element.SendKeys(configs["password"]);


                //for device registration
                element = driver.FindElement(By.CssSelector(configs["LoginButton"]));
                element.Click();


                Thread.Sleep(1000);

                if (existsElement(By.Id(configs["DeviceNameTextBox"]), driver))
                {


                    if (driver.FindElement(By.Id(configs["DeviceNameTextBox"])).Displayed == true)
                    {

                        driver.FindElement(By.Id(configs["DeviceNameTextBox"])).SendKeys("Testing");
                        driver.FindElement(By.CssSelector(configs["DeviceNameConfirmation"])).Click();

                    }

                }


                Thread.Sleep(1000);
                if (existsElement(By.CssSelector(configs["LoginButton"]), driver))
                {
                    Console.WriteLine("Login Unsuccessful ");
                    Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                     ss.SaveAsFile(@"C:\Selenium\TestSnapsShots\" + GetCurrentMethod() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                }
                else
                {
                    Console.WriteLine("Login Successful ");
                }
            }
            Assert.AreEqual(true, existsElement(By.CssSelector(configs["LoginButton"]), driver));
        }
        [Test]
        public void CorrectLoginTest_Regular()
        {



            //find the side menu
            Thread.Sleep(6000);

            element = driver.FindElement(By.XPath(configs["LeftMenuIcon"]));
            if (element.Displayed == true)
                element.Click();

            Thread.Sleep(1000);
            //click on login
            if (existsElement(By.CssSelector(configs["LoginMenuIcon"]), driver))
            {
                element = driver.FindElement(By.CssSelector(configs["LoginMenuIcon"]));
                if (element.Displayed == true)
                    element.Click();
                else
                {
                    Console.WriteLine("Already LoggedIn");
                    isLoggedIn = true;
                }
            }
            else
            {
                Console.WriteLine("Already LoggedIn");
                isLoggedIn = true;
            }
            Thread.Sleep(1000);

            if (isLoggedIn == false)
            {

                element = driver.FindElement(By.Id(configs["UserNameTextBox"]));
                element.SendKeys(configs["username"]);
                element = driver.FindElement(By.Id(configs["PasswordTextBox"]));
                element.SendKeys(configs["password"]);


                //for device registration
                element = driver.FindElement(By.CssSelector(configs["LoginButton"]));
                element.Click();


                Thread.Sleep(1000);

                if (existsElement(By.Id(configs["DeviceNameTextBox"]), driver))
                {


                    if (driver.FindElement(By.Id(configs["DeviceNameTextBox"])).Displayed == true)
                    {

                        driver.FindElement(By.Id(configs["DeviceNameTextBox"])).SendKeys("Testing");
                        driver.FindElement(By.CssSelector(configs["DeviceNameConfirmation"])).Click();

                    }

                }


                Thread.Sleep(1000);
                if (existsElement(By.CssSelector(configs["LoginButton"]), driver))
                {
                    Console.WriteLine("Login Unsuccessful ");
                    Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                    ss.SaveAsFile(@"C:\Selenium\TestSnapsShots\" + GetCurrentMethod() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                }
                else
                {
                    Console.WriteLine("Login Successful ");
                    driver.FindElement(By.XPath(configs["LeftMenuIcon"])).Click();
                    Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                    ss.SaveAsFile(@"C:\Selenium\TestSnapsShots\" + GetCurrentMethod() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            Assert.AreEqual(false, existsElement(By.CssSelector(configs["LoginButton"]), driver));

        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
