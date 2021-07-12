using OpenQA.Selenium;
using Protractor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PlanitAssessment_JohnEdwardSantillan.Pages
{
    public class ContactPage : BasePage
    {
        public ContactPage(IWebDriver webDriver, bool isMobileSite) 
            : base(webDriver, isMobileSite)
        {

        }

        public IWebElement BtnSubmit => _driver.FindElement(By.LinkText("Submit"));
        public IWebElement ForenameTextBox => _driver.FindElement(By.Id("forename"));
        public IList<IWebElement> ForenameErr => _driver.FindElements(By.Id("forename-err"));
        public IWebElement EmailTextBox => _driver.FindElement(By.Id("email"));
        public IList<IWebElement> EmailErr => _driver.FindElements(By.Id("email-err"));
        public IWebElement MessageTextBox => _driver.FindElement(By.Id("message"));
        public IList<IWebElement> MessageErr => _driver.FindElements(By.Id("message-err"));

        public IWebElement SuccessAlert => _driver.FindElement(By.ClassName("alert-success"));
       

        public void ClickSubmit() => BtnSubmit.Click();

    }
}
