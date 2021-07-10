using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PlanitAssessment_JohnEdwardSantillan.Interfaces;
using PlanitAssessment_JohnEdwardSantillan.PageModels;
using Protractor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace PlanitAssessment_JohnEdwardSantillan.AutomationTests
{
    public class ContactPageTests
    {
        IWebDriver driver;
        NgWebDriver ngWebDriver;
        HomePage home;
        ContactPage contact;


        [SetUp]
        public void Setup()
        {
            /* Alternatively if the chromeDriver does not match with current browser version, kindly use the "chromedriver.exe"
                included in git repo and save in local filepath. Afterwards, kindly specify the local filepath 
                as parameter when instantiating ChromeDriver() object.

            // Sample:
            // chromeDriver = new ChromeDriver(@"C:\driver\")
            */

            // Using Microsoft Edge
            //driver = new EdgeDriver();

            // Using Safari
            //driver = new SafariDriver()

            // Using Firefox
            //driver = new FirefoxDriver();


            driver = new ChromeDriver();
            //driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://jupiter.cloud.planittesting.com");
            
            ngWebDriver = new NgWebDriver(driver);
            ngWebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            ngWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            ngWebDriver.WaitForAngular();


            home = new HomePage(ngWebDriver, false);
            contact = new ContactPage(ngWebDriver, false);

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void ValidateMandatoryFields_FullScreen_TestCase1()
        {
            //HomePage home = new HomePage(ngWebDriver, false);
            //ContactPage contact = new ContactPage(ngWebDriver, false);

            home.ClickContactMenu();
            contact.ClickSubmit();

            // Assertion to check if mandatory field validations are displayed
            contact.ForenameErr[0].Text.Should().Be("Forename is required");
            contact.EmailErr[0].Text.Should().Be("Email is required");
            contact.MessageErr[0].Text.Should().Be("Message is required");

            contact.EmailTextBox.SendKeys("test");

            // Assertion to check if email entered is valid
            contact.EmailErr[0].Text.Should().Be("Please enter a valid email");

            contact.EmailTextBox.Clear();

            contact.ForenameTextBox.SendKeys("John");
            contact.EmailTextBox.SendKeys("testuser1@gmail.com");
            contact.MessageTextBox.SendKeys("validate message for testcase1");

            // Assertion to check if mandatory validations are no longer visible
            contact.ForenameErr.Count.Should().Be(0);
            contact.EmailErr.Count.Should().Be(0);
            contact.MessageErr.Count.Should().Be(0);

        }

        [Test]
        public void ValidateMandatoryFields_MobileScreen_TestCase1()
        {
            //HomePage home = new HomePage(ngWebDriver, true);

            home.ClickContactMenu();
        }


        [Test]
        public void SubmitContact_FullScreen_TestCase2()
        {
            //HomePage home = new HomePage(ngWebDriver, false);
            //ContactPage contact = new ContactPage(ngWebDriver, false);
            int retestCount = 5;
            int iterator = 0;

            while (iterator < 5)
            {
                home.ClickContactMenu();
                contact.ForenameTextBox.SendKeys("John");
                contact.EmailTextBox.SendKeys("test@yahoo.com");
                contact.MessageTextBox.SendKeys("submit message for testcase2");

                contact.ClickSubmit();
                contact.SuccessAlert.Text.Should().StartWith("Thanks").And.EndWith("feedback.");
                
                contact.ClickHomeMenu(); // restart to homepage

                iterator++;
            }

            iterator.Should().Equals(retestCount);
        }

        [Test]
        public void BuyItems_FullScreen_TestCase3()
        {
            IHomePage home = new HomePage(ngWebDriver, false);
            home.ClickShopMenu();

            ShopPage shop = new ShopPage(ngWebDriver, false);

            shop.ClickBuyCow();
            shop.ClickBuyCow();
            shop.ClickBuyBunny();

            shop.ClickCartMenu();

        }
    }
}
