using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Linq;
using System.ComponentModel;
using System.Globalization;

namespace PlanitAssessment_JohnEdwardSantillan.Pages
{
    public abstract class BasePage
    {
        private protected IWebDriver _driver;
        private protected bool _isMobileSite;

        public BasePage(IWebDriver webDriver, bool isMobileSite)
        {
            _driver = webDriver;
            _isMobileSite = isMobileSite;

            SetWindowSize(_isMobileSite);
        }

        public IWebElement HomeMenu => _driver.FindElement(By.LinkText("Home"));
        public IWebElement ShopMenu => _driver.FindElement(By.LinkText("Shop"));
        public IWebElement ContactMenu => _driver.FindElement(By.LinkText("Contact"));
        public IWebElement LoginMenu => _driver.FindElement(By.LinkText("Login"));
        public IWebElement CartMenu => _driver.FindElement(By.PartialLinkText("Cart"));
        public IWebElement NavBarMenu => _driver.FindElement(By.CssSelector(".btn-navbar"));


        public string GetElementAttribute(IWebElement element, string attribute)
        {
            return element.GetAttribute(attribute);
        }

        public void Click(IWebElement button)
        {
            button.Click();
        }

        public void Click(IWebElement button, int clicks)
        {
            int iterator = 0;

            do
            {
                button.Click();
                iterator++;
            }
            while (iterator < clicks);

        }

        public string GetNumbersFromText(string input)
        {
            return new string(input.Where(c => char.IsDigit(c) || c.Equals('.')).ToArray());
        }

        public T TryParseText<T>(string text)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFromString(null, CultureInfo.InvariantCulture, text);
        }

        public void ClickNavBarMenu() => NavBarMenu.Click();

        public void ClickHomeMenu()
        {
            if (_isMobileSite)
            {
                ClickNavBarMenu();
            }

            HomeMenu.Click();
        }

        public void ClickShopMenu()
        {
            if (_isMobileSite)
            {
                ClickNavBarMenu();
            }

            ShopMenu.Click();
        }

        public void ClickContactMenu()
        {
            if (_isMobileSite)
            {
                ClickNavBarMenu();
            }

            ContactMenu.Click();
        }

        public void ClickLoginMenu()
        {
            if (_isMobileSite)
            {
                ClickNavBarMenu();
            }

            LoginMenu.Click();
        }

        public void ClickCartMenu()
        {
            if (_isMobileSite)
            {
                ClickNavBarMenu();
            }

            CartMenu.Click();
        }


        private void SetWindowSize(bool isMobile)
        {
            if (isMobile)
            {
                _driver.Manage().Window.Size = new Size(360, 640);
            }
            else
            {
                _driver.Manage().Window.FullScreen();
            }
        }
    }
}
