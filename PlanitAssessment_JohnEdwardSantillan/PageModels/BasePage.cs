using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using PlanitAssessment_JohnEdwardSantillan.Interfaces;

namespace PlanitAssessment_JohnEdwardSantillan.PageModels
{
    public abstract class BasePage : IBasePage
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
        public IWebElement CartMenu => _driver.FindElement(By.Id("nav-cart"));
        public IWebElement NavBarMenu => _driver.FindElement(By.CssSelector(".btn-navbar"));

        
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
