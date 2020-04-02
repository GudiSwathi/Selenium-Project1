using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercuryTours.CoreHelpers
{
    /// <summary>
    /// Supplies a set of common conditions that can be waited for the web elements
    /// https://github.com/DotNetSeleniumTools/DotNetSeleniumExtras/blob/master/src/WaitHelpers/ExpectedConditions.cs
    /// </summary>
    /// <example>
    /// <code>
    /// IWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
    /// IWebElement element = wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("foo")));
    /// </code>
    /// </example>
    public static class WebElementExpectedConditions
    {
        /// <summary>
        /// An expectation for checking that an element is either invisible or not present on the DOM.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns><see langword="true"/> if the element is not displayed; otherwise, <see langword="false"/>.</returns>
        public static Func<IWebDriver, bool> InvisibilityOfElement(By locator)
        {
            return (driver) =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return !element.Displayed;
                }
                catch (NoSuchElementException)
                {
                        // Returns true because the element is not present in DOM. The
                        // try block checks if the element is present but is invisible.
                        Console.WriteLine($"The specified element {locator.ToString()} is not present in the DOM");
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                        // Returns true because stale element reference implies that element
                        // is no longer visible.
                        Console.WriteLine($"The specified element {locator.ToString()} is no longer visible");
                    return true;
                }
            };
        }

        /// <summary>
        /// An expectation for checking whether the given frame is available to switch
        /// to. If the frame is available it switches the given driver to the
        /// specified frame.
        /// </summary>
        /// <param name="locator">Locator for the Frame</param>
        /// <returns><see cref="IWebDriver"/></returns>
        public static Func<IWebDriver, IWebDriver> FrameToBeAvailableAndSwitchToIt(By locator)
        {
            return (driver) =>
            {
                try
                {
                    var frameElement = driver.FindElement(locator);
                    return driver.SwitchTo().Frame(frameElement);
                }
                catch (NoSuchFrameException)
                {
                    return null;
                }
            };
        }

    }
}

