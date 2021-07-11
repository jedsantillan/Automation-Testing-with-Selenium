using NUnit.Framework;
using OpenQA.Selenium;
using Protractor;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;

namespace PlanitAssessment_JohnEdwardSantillan.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver webDriver, bool isMobileSite) : base(webDriver, isMobileSite)
        {

        }

        public IWebElement StartShoppingBtn => _driver.FindElement(By.LinkText("Start Shopping »"));

        public void ClickStartShoppingBtn() => StartShoppingBtn.Click();
    }
}
