using OpenQA.Selenium;
using PlanitAssessment_JohnEdwardSantillan.Interfaces;
using Protractor;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanitAssessment_JohnEdwardSantillan.PageModels
{
    public class ShopPage : BasePage
    {
        public ShopPage(IWebDriver webDriver, bool isMobileSite) 
            : base(webDriver, isMobileSite)
        {

        }
        public IWebElement BtnBuyFrog => _driver.FindElement(By.Id("product-2")).FindElement(By.LinkText("Buy"));

        public IWebElement BtnBuyBunny => _driver.FindElement(By.Id("product-4")).FindElement(By.LinkText("Buy"));

        public IWebElement BtnBuyCow => _driver.FindElement(By.Id("product-6")).FindElement(By.LinkText("Buy"));

        public IWebElement BtnValentineBear => _driver.FindElement(By.Id("product-7")).FindElement(By.LinkText("Buy"));


        public void ClickBuyFrog() => BtnBuyFrog.Click();
        public void ClickBuyBunny() => BtnBuyBunny.Click();
        public void ClickBuyCow() => BtnBuyCow.Click();
        public void ClickBuyValentineBear() => BtnValentineBear.Click();
    }
}
