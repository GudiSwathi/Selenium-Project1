using MercuryTours.CoreHelpers;
using OpenQA.Selenium;

namespace MercuryTours.PageObjects
{
    public class MercuryToursHomePage : MercuryBasePage
    {
        private readonly By signOnLink = By.XPath("/html/body/div/table/tbody/tr/td[2]/table/tbody/tr[2]/td/table/tbody/tr/td[1]/a");
        public MercuryToursHomePage(SeleniumActions seleniumActions) : base(seleniumActions)
        {
        }
        public SignOnPage NavigateToSignOnPage()
        {
            SeleniumActions.Click(signOnLink);
            return new SignOnPage(SeleniumActions);
        }



    }
}
