using MercuryTours.CoreHelpers;
using MercuryTours.DataModels;
using OpenQA.Selenium;
using System.Threading;

namespace MercuryTours.PageObjects
{
    public class SignOnPage : MercuryBasePage
    {
        private readonly By userName = By.Name("userName");
        private readonly By password = By.Name("password");
        private readonly By Submit = By.Name("login");
        public SignOnPage(SeleniumActions seleniumActions) : base(seleniumActions)
        {
        }
        public void SignOnTheUser(SignOnInformation signOnInformation)
        {
            SeleniumActions.SendKeys(userName, signOnInformation.UserName);
            SeleniumActions.SendKeys(password, signOnInformation.Password);
            SeleniumActions.Click(Submit);
            Thread.Sleep(3000);
        }
    }
}
