using MercuryTours.CoreHelpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MercuryTours.Tests
{
    [TestFixture]
    public class BaseTests
    {
        private readonly int defaultTimeOutSeconds = 60;
        private IWebDriver webDriver;
        public IAlert alert;
        public SeleniumActions SeleniumActions { get; private set; }

        [SetUp]
        public void TestInitialization()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl("http://newtours.demoaut.com/mercuryregister.php");
            SeleniumActions = new SeleniumActions(webDriver, defaultTimeOutSeconds);
        }

        [TearDown]
        public void TestTearDown()
        {
            webDriver.Quit();
        }
    }
}
