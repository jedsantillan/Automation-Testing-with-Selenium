using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Protractor;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanitAssessment_JohnEdwardSantillan.Setup
{
    public class FixtureSetup
    {
        public IWebDriver driver;
        public NgWebDriver ngWebDriver;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://jupiter.cloud.planittesting.com");

            ngWebDriver = new NgWebDriver(driver);
            ngWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            ngWebDriver.WaitForAngular();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }
    }
}
