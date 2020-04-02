using MercuryTours.CoreHelpers;

namespace MercuryTours.PageObjects
{
    public class MercuryBasePage
    {
        protected SeleniumActions SeleniumActions;
        public MercuryBasePage(SeleniumActions seleniumActions)
        {
            SeleniumActions = seleniumActions;
        }
    }
}
