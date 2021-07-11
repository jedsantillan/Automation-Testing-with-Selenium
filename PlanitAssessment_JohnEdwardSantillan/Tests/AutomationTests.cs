using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PlanitAssessment_JohnEdwardSantillan.Pages;
using PlanitAssessment_JohnEdwardSantillan.Setup;
using Protractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PlanitAssessment_JohnEdwardSantillan.Tests
{
    public class AutomationTests : FixtureSetup
    {
        [SetUp]
        public void Setup()
        {
            ngWebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Navigate().GoToUrl("http://jupiter.cloud.planittesting.com");
        }

        [Test]
        public void TestCase1_ValidateMandatoryFields()
        {
            HomePage home = new HomePage(ngWebDriver, false);
            ContactPage contact = new ContactPage(ngWebDriver, false);

            ngWebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

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
        public void TestCase2_SubmitContact()
        {
            HomePage home = new HomePage(ngWebDriver, false);
            ContactPage contact = new ContactPage(ngWebDriver, false);
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
        public void TestCase3_BuyItems()
        {
            ShopPage shop = new ShopPage(ngWebDriver, false);
            CartPage cart = new CartPage(ngWebDriver, false);

            shop.Click(shop.ShopMenu); 
            shop.Click(shop.BtnBuyCow, 2);
            shop.Click(shop.BtnBuyBunny, 1);

            shop.CartMenu.Text.Should().Contain("3");   // Total of 3 items added to cart

            shop.Click(shop.CartMenu);

            cart.CartItems.Count.Should().Be(2);   // 2 types product added

            string cowImagePath = cart.GetElementAttribute(cart.CartItems[0].FindElement(By.XPath(".//td[@class='ng-binding']//img")), "src");
            cowImagePath.Should().Contain("cow");
            cart.GetProductQuantity(cart.CartItems[0]).Should().Be(2); // 2 Funny Cow added to cart

            string bunnyImagePath = cart.GetElementAttribute(cart.CartItems[1].FindElement(By.XPath(".//td[@class='ng-binding']//img")), "src");
            bunnyImagePath.Should().Contain("bunny");   
            cart.GetProductQuantity(cart.CartItems[1]).Should().Be(1); // 1 Fluffy Bunny added to cart

        }

        [Test]
        public void TestCase4_ValidatePriceCalculation()
        {
            ShopPage shop = new ShopPage(ngWebDriver, false);
            CartPage cart = new CartPage(ngWebDriver, false);

            shop.Click(shop.ShopMenu);

            shop.Click(shop.BtnBuyFrog, 2);
            shop.Click(shop.BtnBuyBunny, 5);
            shop.Click(shop.BtnBuyValentineBear, 3);
            shop.Click(shop.CartMenu);

            cart.CartMenu.Text.Should().Contain("10");  // Total of 10 items added to cart
            cart.CartItems.Count.Should().Be(3);        // 3 types products added

            double frogPrice = cart.GetProductPrice(cart.CartItems[0]);
            double bunnyPrice = cart.GetProductPrice(cart.CartItems[1]);
            double vBearPrice = cart.GetProductPrice(cart.CartItems[2]);

            frogPrice.Should().Be(10.99); //Expected is 10.99 per Stuffed Frog
            bunnyPrice.Should().Be(9.99);  //Expected is 9.99 per Fluffy Bunny    
            vBearPrice.Should().Be(14.99); //Expected is 9.99 per Valentine Bear

            int frogQty = cart.GetProductQuantity(cart.CartItems[0]);
            int bunnyQty = cart.GetProductQuantity(cart.CartItems[1]);
            int vBearQty = cart.GetProductQuantity(cart.CartItems[2]);


            double frogSubTotal = cart.GetProductSubtotal(cart.CartItems[0]);
            double bunnySubTotal = cart.GetProductSubtotal(cart.CartItems[1]);
            double vBearSubTotal = cart.GetProductSubtotal(cart.CartItems[2]);

            cart.CalculateSubTotal(frogPrice, frogQty).Should().Be(frogSubTotal);
            cart.CalculateSubTotal(bunnyPrice, bunnyQty).Should().Be(bunnySubTotal);
            cart.CalculateSubTotal(vBearPrice, vBearQty).Should().Be(vBearSubTotal);

            double totalPrice = cart.TryParseText<double>(cart.GetNumbersFromText(cart.TotalPrice.Text));

            totalPrice.Should().Be((frogSubTotal + bunnySubTotal + vBearSubTotal));

        }
    }
}
