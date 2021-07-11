using OpenQA.Selenium;

namespace PlanitAssessment_JohnEdwardSantillan.Pages
{
    public class ShopPage : BasePage
    {
        public ShopPage(IWebDriver webDriver, bool isMobileSite) 
            : base(webDriver, isMobileSite)
        {

        }

        public IWebElement BtnBuyTeddyBear => _driver.FindElement(By.Id("product-1")).FindElement(By.LinkText("Buy"));
        public IWebElement BtnBuyFrog => _driver.FindElement(By.Id("product-2")).FindElement(By.LinkText("Buy"));
        public IWebElement BtnBuyHandmadeDoll => _driver.FindElement(By.Id("product-3")).FindElement(By.LinkText("Buy"));
        public IWebElement BtnBuyBunny => _driver.FindElement(By.Id("product-4")).FindElement(By.LinkText("Buy"));
        public IWebElement BtnBuySmileyBear => _driver.FindElement(By.Id("product-5")).FindElement(By.LinkText("Buy"));
        public IWebElement BtnBuyCow => _driver.FindElement(By.Id("product-6")).FindElement(By.LinkText("Buy"));
        public IWebElement BtnBuyValentineBear => _driver.FindElement(By.Id("product-7")).FindElement(By.LinkText("Buy"));
        public IWebElement BtnBuySmileyFace => _driver.FindElement(By.Id("product-8")).FindElement(By.LinkText("Buy"));

    }
}
