using OpenQA.Selenium;
using Protractor;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace PlanitAssessment_JohnEdwardSantillan.Pages
{
    public class CartPage : BasePage
    {
        public CartPage(IWebDriver webDriver, bool isMobileSite)
            : base(webDriver, isMobileSite)
        {

        }

        public IList<IWebElement> CartItems => _driver.FindElements(By.ClassName("cart-item"));

        public IWebElement TotalPrice => _driver.FindElement(By.ClassName("total"));

        public double GetProductPrice(IWebElement cartItem)
        {
            IWebElement tempElement = cartItem.FindElement(By.CssSelector(".ng-binding:nth-child(2)"));
            var price = GetNumbersFromText(tempElement.Text);

            return TryParseText<double>(price); ;
        }

        public double GetProductSubtotal(IWebElement cartItem)
        {
            IWebElement tempElement = cartItem.FindElement(By.CssSelector(".ng-binding:nth-child(4)"));
            var subTotal = GetNumbersFromText(tempElement.Text);

            return TryParseText<double>(subTotal);
        }

        public int GetProductQuantity(IWebElement cartItem)
        {
            var temp = cartItem.FindElement(By.Name("quantity"));

            return TryParseText<int>(temp.GetAttribute("value"));
        }

        public double CalculateSubTotal(double unitPrice, int unitQuantity) => Math.Round(unitPrice * unitQuantity, 2);


    }
}
