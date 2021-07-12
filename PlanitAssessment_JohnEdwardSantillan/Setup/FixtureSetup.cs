using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Protractor;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PlanitAssessment_JohnEdwardSantillan.Setup
{
    public class FixtureSetup
    {
        public IWebDriver driver;
        public NgWebDriver ngWebDriver;
        
        // Configs
        public string siteURL = "http://jupiter.cloud.planittesting.com";
        public readonly bool isMobileSite = false;  // change to true if intent is to test mobile site
        public readonly int threadSleep = 500;     // delay in milliseconds. This is to allow the website to load elements
        public readonly TimeSpan pageTimeout = TimeSpan.FromSeconds(30);


        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(siteURL);

            ngWebDriver = new NgWebDriver(driver);
            ngWebDriver.Manage().Timeouts().PageLoad = pageTimeout;
            
            ngWebDriver.WaitForAngular();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }
    }
}
