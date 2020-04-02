using MercuryTours.CoreHelpers;
using OpenQA.Selenium;
using System.Linq;
using System.Threading;

namespace MercuryTours.PageObjects
{
    public class MercuryRegistrationPage : MercuryBasePage
    {
        private readonly By firstName = By.Name("firstName");
        private readonly By lastName = By.Name("lastName");
        private readonly By phoneNumber = By.Name("phone");
        private readonly By email = By.Name("userName");
        private readonly By address1 = By.Name("address1");
        private readonly By address2 = By.Name("address2");
        private readonly By city = By.Name("city");
        private readonly By state = By.Name("state");
        private readonly By postalCode = By.Name("postalCode");
        private readonly By country = By.Name("country");
        private readonly By userName = By.Name("email");
        private readonly By password = By.Name("password");
        private readonly By confirmPassword = By.Name("confirmPassword");
        private readonly By Submit = By.Name("register");
        private readonly By FooterText = By.ClassName("footer");
   //     private readonly By RegistrationText = By.XPath("/html/body/div/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr/td[2]/table/tbody/tr[3]/td/p[1]/font/b");
        private readonly By homePageLink = By.XPath("/html/body/div/table/tbody/tr/td[1]/table/tbody/tr/td/table/tbody/tr/td/table/tbody/tr[1]/td[2]/font/a");

        public MercuryRegistrationPage(SeleniumActions seleniumActions) : base(seleniumActions)
        {
        }

        public bool CheckIfFooterTextIsDisplayed(string webPagefooterText)
        {
            SeleniumActions.WaitUntilTextIsVisible(webPagefooterText);
            string text = SeleniumActions.Text(FooterText);
            return text == webPagefooterText ? true : false;
        }

        public void RegisterTheFirstUser(RegistrationInformation registrationInformation)
        {
            SeleniumActions.SendKeys(firstName, registrationInformation.ContactInformation.FirstName);
            SeleniumActions.SendKeys(lastName, registrationInformation.ContactInformation.LastName);
            SeleniumActions.SendKeys(phoneNumber, registrationInformation.ContactInformation.Phone);
            SeleniumActions.SendKeys(email, registrationInformation.ContactInformation.Email);

            var addressInfo = registrationInformation.MailingInformation.Address.Split(',').ToList();
            SeleniumActions.SendKeys(address1, addressInfo[0]);
            SeleniumActions.SendKeys(address2, addressInfo[1]);

            SeleniumActions.SendKeys(city, registrationInformation.MailingInformation.City);
            SeleniumActions.SendKeys(state, registrationInformation.MailingInformation.State);
            SeleniumActions.SendKeys(postalCode, registrationInformation.MailingInformation.PostalCode);

            SeleniumActions.SelectByTextFromDropDown(country, registrationInformation.MailingInformation.Country);
            Thread.Sleep(3000);

            SeleniumActions.SendKeys(userName, registrationInformation.UserInformation.UserName);
            SeleniumActions.SendKeys(password, registrationInformation.UserInformation.Password);
            SeleniumActions.SendKeys(confirmPassword, registrationInformation.UserInformation.ConfirmPassword);
            SeleniumActions.Click(Submit);

            Thread.Sleep(5000);
        }

        //public bool CheckIfRegistrationTextIsDisplayed(string thankYouForRegistering)
        //{
        //    SeleniumActions.WaitUntilTextIsVisible(thankYouForRegistering);
        //    string text = SeleniumActions.Text(RegistrationText);
        //    return text == thankYouForRegistering ? true : false;
        //}

        public MercuryToursHomePage NavigateToHomePage()
        {
            SeleniumActions.Click(homePageLink);
            return new MercuryToursHomePage(SeleniumActions);
        }
       
    }
}
