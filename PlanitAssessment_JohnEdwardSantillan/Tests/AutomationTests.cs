using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PlanitAssessment_JohnEdwardSantillan.Models;
using PlanitAssessment_JohnEdwardSantillan.Pages;
using PlanitAssessment_JohnEdwardSantillan.Setup;
using Protractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace PlanitAssessment_JohnEdwardSantillan.Tests
{
    public class AutomationTests : FixtureSetup
    {
        private readonly bool isMobileSite = false;  // change to true if intent is to test mobile site
        private readonly int threadSleep = 500;     // delay in milliseconds. This is to allow the website to load elements

        [SetUp]
        public void Setup()
        {
            ngWebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        [TearDown]
        public void TearDown()
        {
            // navigate back to home page after end of each test case
            driver.Navigate().GoToUrl("http://jupiter.cloud.planittesting.com");
        }


        [Test]
        public void TestCase1_ValidateMandatoryFields()
        {
            HomePage home = new HomePage(ngWebDriver, isMobileSite);
            ContactPage contact = new ContactPage(ngWebDriver, isMobileSite);

            ngWebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            home.ClickContactMenu();
            contact.ClickSubmit();

            Thread.Sleep(threadSleep);

            // Assertion for display of mandatory field validations
            contact.ForenameErr[0].Text.Should().Be("Forename is required");
            contact.EmailErr[0].Text.Should().Be("Email is required");
            contact.MessageErr[0].Text.Should().Be("Message is required");

            contact.SendKeys(contact.EmailTextBox, "test");

            // Assertion for email format
            contact.EmailErr[0].Text.Should().Be("Please enter a valid email");

            contact.SendKeys(contact.ForenameTextBox, "John");
            contact.SendKeys(contact.EmailTextBox, "testuser1@gmail.com");
            contact.SendKeys(contact.MessageTextBox, "validate message for testcase 1");

            // Assertion for mandatory field validations
            contact.ForenameErr.Count.Should().Be(0);
            contact.EmailErr.Count.Should().Be(0);
            contact.MessageErr.Count.Should().Be(0);
        }


        [Test]
        public void TestCase2_SubmitContact()
        {
            HomePage home = new HomePage(ngWebDriver, isMobileSite);
            ContactPage contact = new ContactPage(ngWebDriver, isMobileSite);

            int retestCount = 5;
            int iterator = 1;

            while (iterator <= 5)
            {
                home.ClickContactMenu();
                contact.SendKeys(contact.ForenameTextBox, $"Test Name {iterator}");
                contact.SendKeys(contact.EmailTextBox, $"test{iterator}@gmail.com");
                contact.SendKeys(contact.MessageTextBox, $"submit message for testcase 2. Iteration: {iterator}");

                contact.ClickSubmit();
                contact.SuccessAlert.Text.Should().StartWith("Thanks").And.EndWith("feedback.");
                
                contact.ClickHomeMenu(); // restart to homepage

                Thread.Sleep(threadSleep);
                iterator++;
            }

            iterator.Should().Equals(retestCount);
        }

        [Test]
        public void TestCase3_BuyItems()
        {
            ShopPage shop = new ShopPage(ngWebDriver, isMobileSite);
            CartPage cart = new CartPage(ngWebDriver, isMobileSite);

            shop.ClickShopMenu();
            Thread.Sleep(threadSleep);
            shop.Click(shop.BtnBuyCow, 2);
            shop.Click(shop.BtnBuyBunny, 1);

            if (isMobileSite)
            {
                shop.ClickNavBarMenu();
                shop.CartMenu.Text.Should().Contain("3");   // Total of 3 items added to cart
                shop.ClickNavBarMenu();
            }
            else
            {
                shop.CartMenu.Text.Should().Contain("3");   // Total of 3 items added to cart
            }
            
            shop.ClickCartMenu();

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
            ShopPage shop = new ShopPage(ngWebDriver, isMobileSite);
            CartPage cart = new CartPage(ngWebDriver, isMobileSite);

            shop.ClickShopMenu();
            Thread.Sleep(threadSleep);

            shop.Click(shop.BtnBuyFrog, 2);
            shop.Click(shop.BtnBuyBunny, 5);
            shop.Click(shop.BtnBuyValentineBear, 3);

            shop.ClickCartMenu();

            List<CartItemModel> cartItemsList = cart.GetCartItemsList(cart.CartItems);
            double totalPrice = cart.GetTotalPrice(cart.TotalPrice);

            cartItemsList[0].Price.Should().Be(10.99);  //Expected is 10.99 per Stuffed Frog
            cartItemsList[1].Price.Should().Be(9.99);   //Expected is 10.99 per Fluffy Bunny
            cartItemsList[2].Price.Should().Be(14.99);  //Expected is 10.99 per Valentine Bear

            cart.CalculateSubTotal(cartItemsList[0].Price, cartItemsList[0].Quantity).Should().Be(cartItemsList[0].SubTotal);   // Stuffed Frog
            cart.CalculateSubTotal(cartItemsList[1].Price, cartItemsList[1].Quantity).Should().Be(cartItemsList[1].SubTotal);   // Fluffy Bunny
            cart.CalculateSubTotal(cartItemsList[2].Price, cartItemsList[2].Quantity).Should().Be(cartItemsList[2].SubTotal);   // Valentine Bear

            // Assertion of TotalPrice against Summation of Product Subtotal
            totalPrice.Should().Be(cartItemsList.Sum(x => x.SubTotal)); 

        }
    }
}
