using MercuryTours.CoreHelpers;
using MercuryTours.DataModels;
using MercuryTours.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MercuryTours.Tests
{
    [TestFixture]
    public class SampleDataDrivenTesting : BaseTests
    { 
        [Test]
        public void MercuryToursRegistrationTest()
        {
            List<RegistrationInformation> registrationInformation = GetTestData.GetRegistrationInformation();
            RegistrationInformation requiredRegistrationInfo = registrationInformation.
                Where(x => x.ContactInformation.FirstName.Equals("swathi",StringComparison.InvariantCultureIgnoreCase)).Select(x => x).FirstOrDefault();

            MercuryRegistrationPage mercuryRegistrationPage = new MercuryRegistrationPage(SeleniumActions);
            bool isFooterAvailable = mercuryRegistrationPage.CheckIfFooterTextIsDisplayed("© 2005, Mercury Interactive (v. 011003-1.01-058)");
            Assert.IsTrue(isFooterAvailable, "Footer text is not available");

            mercuryRegistrationPage.RegisterTheFirstUser(requiredRegistrationInfo);

            //bool isRegistrationTextAvailable = mercuryRegistrationPage.CheckIfRegistrationTextIsDisplayed("Thank you for registering. You may now sign-in using the user name and password you've just entered.");
            //Assert.IsTrue(isRegistrationTextAvailable, "Registration text is not available");
            MercuryToursHomePage homeLink = mercuryRegistrationPage.NavigateToHomePage();
        }

        public void MercuryToursRegistrationTest2()
        {
            List<RegistrationInformation> registrationInformation = GetTestData.GetRegistrationInformation();
            RegistrationInformation requiredRegistrationInfo = registrationInformation.
                Where(x => x.ContactInformation.FirstName.Equals("Buddi", StringComparison.InvariantCultureIgnoreCase)).Select(x => x).FirstOrDefault();

            MercuryRegistrationPage mercuryRegistrationPage = new MercuryRegistrationPage(SeleniumActions);
            bool isFooterAvailable = mercuryRegistrationPage.CheckIfFooterTextIsDisplayed("© 2005, Mercury Interactive (v. 011003-1.01-058)");
            Assert.IsTrue(isFooterAvailable, "Footer text is not available");

            mercuryRegistrationPage.RegisterTheFirstUser(requiredRegistrationInfo);

            //bool isRegistrationTextAvailable = mercuryRegistrationPage.CheckIfRegistrationTextIsDisplayed("Thank you for registering. You may now sign-in using the user name and password you've just entered.");
            //Assert.IsTrue(isRegistrationTextAvailable, "Registration text is not available");
            MercuryToursHomePage homeLink = mercuryRegistrationPage.NavigateToHomePage();
        }

        [Test]
        public void MercuryHomePageTest()
        {
            MercuryToursHomePage mercuryToursHomePage = new MercuryToursHomePage(SeleniumActions);
            SignOnPage signOnPage = mercuryToursHomePage.NavigateToSignOnPage();
            Thread.Sleep(3000);

             SignOnInformation signOnInformation = GetTestData.GetSignOnInformation()
                .Where(x => x.UserName.Equals("Buddi", StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x).ToList().First();
            signOnPage.SignOnTheUser(signOnInformation);
            Console.WriteLine("This test execution is finished");
        }
    }
}
