using OpenQA.Selenium;
using PlanitAssessment_JohnEdwardSantillan.Models;
using PlanitAssessment_JohnEdwardSantillan.Utilities;
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
            var price = CommonHelper.GetNumbersFromText(tempElement.Text);

            return CommonHelper.TryParseText<double>(price); ;
        }

        public double GetProductSubtotal(IWebElement cartItem)
        {
            IWebElement tempElement = cartItem.FindElement(By.CssSelector(".ng-binding:nth-child(4)"));
            var subTotal = CommonHelper.GetNumbersFromText(tempElement.Text);

            return CommonHelper.TryParseText<double>(subTotal);
        }

        public int GetProductQuantity(IWebElement cartItem)
        {
            var temp = cartItem.FindElement(By.Name("quantity"));

            return CommonHelper.TryParseText<int>(temp.GetAttribute("value"));
        }

        public List<CartItemModel> GetCartItemsList(IList<IWebElement> elementList)
        {
            List<CartItemModel> cartItemsList = new List<CartItemModel>();

            if (elementList.Count > 0)
            {
                foreach(IWebElement element in elementList)
                {
                    CartItemModel cartItem = new CartItemModel
                    {
                        Price = GetProductPrice(element),
                        Quantity = GetProductQuantity(element),
                        SubTotal = GetProductSubtotal(element)
                    };

                    cartItemsList.Add(cartItem);
                }
            }

            return cartItemsList;
        }

        public double GetTotalPrice(IWebElement element) => CommonHelper.TryParseText<double>(CommonHelper.GetNumbersFromText(element.Text));


        public double CalculateSubTotal(double unitPrice, int unitQuantity) => Math.Round(unitPrice * unitQuantity, 2);


    }
}
