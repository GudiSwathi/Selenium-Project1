using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MercuryTours.CoreHelpers
{
    public class SeleniumActions
    {
        private readonly IWebDriver _driver;
        private readonly int _defaultWaitTime;
        public SeleniumActions(IWebDriver webDriver, int defaultTimeoutSeconds)
        {
            _driver = webDriver;
            _defaultWaitTime = defaultTimeoutSeconds;
        }

        /// <summary>
        /// Get an element by its locator, e.g. Id.
        /// </summary>
        private IWebElement FindElement(By locator)
        {
            WaitUntilElementIsVisible(locator);
            return _driver.FindElement(locator);
        }

        /// <summary>
        /// Finds the elements.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        private ReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            WaitUntilElementIsVisible(locator);
            return _driver.FindElements(locator);
        }

        /// <summary>
        /// Finds the element after waiting for clickability.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private IWebElement FindElementAfterWaitingForClickability(By locator, int index = 0)
        {
            WaitUntilElementClickable(locator);
            return _driver.FindElements(locator)[index];
        }

        /// <summary>
        /// Finds the element after waiting for element exists.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        private IWebElement FindElementIfExists(By locator)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_defaultWaitTime));
            wait.Until(_driver => _driver.FindElements(locator).Count != 0);
            return _driver.FindElement(locator);
        }

        /// <summary>
        /// Click the given element.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="index">The index.</param>
        public void Click(By locator, int index = 0)
        {
            var element = FindElementAfterWaitingForClickability(locator, index);
            element.Click();
        }

        /// <summary>
        /// Retry few attempts and Click the given element.
        /// </summary>
        /// <param name="locator">The locator.</param>
        public bool RetryingFindClick(By locator)
        {
            bool result = false;
            int attempts = 0;
            while (attempts < 2)
            {
                try
                {
                    var element = FindElementAfterWaitingForClickability(locator, 0);
                    element.Click();
                    result = true;
                    break;
                }
                catch (Exception)
                {
                }
                attempts++;
            }
            return result;
        }

        /// <summary>
        /// Send the given text to the given element.
        /// </summary>
        public void SendKeys(By locator, string text)
        {
            var element = FindElementAfterWaitingForClickability(locator);
            element.Clear();
            element.SendKeys(text);
        }

        /// <summary>
        /// Send the given text to the given element without clearing the text.
        /// </summary>
        public void SendKeysWithoutClear(By locator, string text)
        {
            var element = FindElementAfterWaitingForClickability(locator);
            element.SendKeys(text);
        }

        /// <summary>
        /// Send the given text to the given element.
        /// </summary>
        public void ClearText(By locator)
        {
            var element = FindElementAfterWaitingForClickability(locator);
            string text = element.GetAttribute("value");

            if (text.Length > 0)
            {
                for (var i = 0; i < text.Length; i++)
                {
                    element.SendKeys(Keys.Backspace);
                }
            }
        }

        /// <summary>
        /// Get the text of a given element.
        /// </summary>
        public string Text(By locator, int index = 0)
        {
            var element = FindElements(locator)[index];
            return element.Text;
        }

        /// <summary>
        /// Get the child element names as a list of strins
        /// </summary>
        /// <param name="parentElementLocator">The parent element locator.</param>
        /// <param name="childElementLocator">The child element locator.</param>
        /// <returns></returns>
        public List<string> ChildElementNames(By parentElementLocator, By childElementLocator)
        {
            var parentElement = _driver.FindElement(parentElementLocator);
            var childElements = parentElement.FindElements(childElementLocator);
            List<string> names = new List<string>();
            foreach (var item in childElements)
            {
                if (item.Text != null)
                {
                    names.Add(item.Text);
                }
            }
            return names;
        }

        /// <summary>
        /// Get the matching element names as per the locator
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        public List<string> GetMatchingElementNames(By locator)
        {
            var elements = FindElements(locator);
            List<string> names = new List<string>();
            foreach (var item in elements)
            {
                if (item.Displayed && item.Text != null)
                {
                    names.Add(item.Text);
                }
            }
            return names;
        }

        /// <summary>
        /// Selects the spcified text from drop down
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="text">The text.</param>
        public void SelectByTextFromDropDown(By locator, string text)
        {
            var element = FindElementIfExists(locator);
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByText(text.ToUpper());
        }

        /// <summary>
        /// Selects the item from the dropdown by index number.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="index">The index.</param>
        public void SelectByIndexFromDropDown(By locator, int index = 0)
        {
            var element = FindElementIfExists(locator);
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByIndex(index);
        }

        /// <summary>
        /// Waits for the element to be removed from the DOM.
        /// </summary>
        public void WaitUntilElementIsNotPresent(By locator)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_defaultWaitTime));
            wait.Until(_driver => _driver.FindElements(locator).Count == 0);
        }

        /// <summary>
        /// Wait until the element with the given text is visible on the page.
        /// </summary>
        public void WaitUntilElementIsVisible(By locator)
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(_defaultWaitTime))
                .Until(_driver => _driver.FindElement(locator).Displayed);
        }

        /// <summary>
        /// Waits for the supplied text to be removed from the DOM.
        /// </summary>
        /// <param name="pattern"></param>
        public void WaitUntilElementIsNotVisibile(string pattern)
        {
            string locatorText = string.Format("//*[contains(text(),'{0}')]", pattern);
            By locator = By.XPath(locatorText);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_defaultWaitTime));
            wait.Until(WebElementExpectedConditions.InvisibilityOfElement(locator));
        }

        /// <summary>
        /// Waits for the element to be not visible.
        /// </summary>
        public void WaitUntilElementIsNotVisibile(By locator)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_defaultWaitTime));
            wait.Until(WebElementExpectedConditions.InvisibilityOfElement(locator));
        }

        /// <summary>
        /// Waits for the supplied text to be removed from the DOM.
        /// TODO: We don't need this method, it should suffice to use 'WaitUntilElementIsNotVisible(By)`, directly above.
        /// </summary>
        /// <param name="id"></param>
        public void WaitUntilElementIsNotVisibileById(string id)
        {
            By locator = By.Id(id);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_defaultWaitTime));
            wait.Until(_driver => !_driver.FindElement(locator).Displayed);
        }

        /// <summary>
        /// Wait until the given element is clickable.
        /// </summary>
        public void WaitUntilElementClickable(By locator)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_defaultWaitTime));
            wait.Until(_driver => _driver.FindElement(locator).Displayed && _driver.FindElement(locator).Enabled);
            wait.Until(_driver => _driver.FindElement(locator).Location.Equals(_driver.FindElement(locator).Location));
        }

        /// <summary>
        /// Wait until the given element is clickable.
        /// </summary>
        public void WaitUntilElementClickable(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_defaultWaitTime));
            wait.Until(_driver => element.Displayed && element.Enabled);
            wait.Until(_driver => element.Location.Equals(element.Location));
        }

        /// <summary>
        /// Navigate back to previous tab of browser.
        /// </summary>
        public void NavigateBack()
        {
            _driver.Navigate().Back();
        }

        /// <summary>
        /// Wait until the element with the given text is visible on the page.
        /// The default wait time is 15 seconds if no waitTime is supplied.
        /// </summary>
        /// <param name="text">Text of the element to wait for.</param>
        public void WaitUntilTextIsVisible(string text)
        {
            string locatorText = string.Format("//*[contains(text(),'{0}')]", text);
            By locator = By.XPath(locatorText);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_defaultWaitTime));
            wait.Until(_driver => _driver.FindElement(locator).Displayed);
        }

        /// <summary>
        /// Executes the java script.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public object ExecuteJavaScript(string javaScript)
        {
            IJavaScriptExecutor _js = (IJavaScriptExecutor)_driver;
            try
            {
                return (string)_js.ExecuteScript(javaScript);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the index of element.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        public int GetIndexOfElement(By locator, string pattern)
        {
            try
            {
                List<IWebElement> elements = FindElements(locator).ToList();
                return elements.FindIndex(a => Regex.Replace(a.Text, @"[^0-9a-zA-Z:]+", " ").Contains(pattern));
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Switches to main frame.
        /// </summary>
        public void SwitchToMainFrame()
        {
            _driver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Clicks the element using java script i.e. sending the direct java script commands to the browser
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="index">The index.</param>
        public void JavaScriptClick(By locator, int index = 0)
        {
            var element = FindElementAfterWaitingForClickability(locator, index);
            IJavaScriptExecutor _js = (IJavaScriptExecutor)_driver;
            _js.ExecuteScript("arguments[0].click();", element);
        }

        /// <summary>
        /// Gets the cookies from browser.
        /// </summary>
        /// <returns></returns>
        public List<Cookie> GetCookiesFromBrowser()
        {
            return _driver.Manage().Cookies.AllCookies.ToList();
        }

        /// <summary>
        /// Deletes all cookies from browser.
        /// </summary>
        public void DeleteAllCookies()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
        }

        /// <summary>
        /// Add a cookie to browser.
        /// </summary>
        public void AddCookie(string name, string value)
        {
            Cookie cookie = new Cookie(name, value);
            _driver.Manage().Cookies.AddCookie(cookie);
        }

        /// <summary>
        /// Gets the value of a cookie by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Cookie value</returns>
        public string GetCookieValueByName(string name)
        {
            return _driver.Manage().Cookies.GetCookieNamed(name).Value;
        }

        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public string GetAttribute(By locator, string attributeName, int index = 0)
        {
            var element = FindElements(locator)[index];
            return element.GetAttribute(attributeName);
        }

        /// <summary>
        /// Determines whether [is element present] [the specified locator].
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns>
        ///   <c>true</c> if [is element present] [the specified locator]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsElementVisible(By locator)
        {
            try
            {
                WaitUntilElementIsVisible(locator);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether element is NOT visible.
        /// For this method to return true, the element has to be present in DOM but not visible
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns>
        ///   <c>true</c> if [is element NOT visible] [the specified locator]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsElementNotVisible(By locator)
        {
            try
            {
                WaitUntilElementIsNotVisibile(locator);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether the element is Clickable
        /// </summary>
        /// <param name="locator">The element to check for cilckability</param>
        /// <returns>
        ///   <c>true</c> if [is element is clickable] [using the specified locator]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsElementClickable(By locator)
        {
            try
            {
                WaitUntilElementClickable(locator);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether element is NOT present in DOM.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns>
        ///   <c>true</c> if [is element NOT present] [the specified locator]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsElementNotPresent(By locator)
        {
            try
            {
                WaitUntilElementIsNotPresent(locator);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether [is CheckBox selected] [the specified locator].
        /// </summary>
        /// <param name="locator">The locator.</param>
        public void SelectCheckBox(By locator)
        {
            if (!FindElement(locator).Selected)
            {
                Click(locator);
            }
        }

        /// <summary>
        /// Moves to the element specified by locator1 (hovers), then
        /// moves to the element specified by locator2, and clicks (locator2).
        /// Example: There's a menu that's displayed when a mouse hover occurs over
        /// the element specified by locator1, and in that menu, there's a clickable element
        /// specified by locator2.
        /// </summary>
        /// <param name="locator1">The first element, eg: to hover over.</param>
        /// <param name="locator2">The second element, to move to and click.</param>
        public void MoveToElementAndClick(By locator1, By locator2)
        {
            Actions action = new Actions(_driver);
            action.MoveToElement(FindElement(locator1)).Perform();
            action.MoveToElement(FindElement(locator2)).Click().Perform();
        }

        /// <summary>
        /// Moves to the element specified by locator1 (hovers), then
        /// moves to the element specified by locator2, and clicks (locator2).
        /// Example: There's a menu that's displayed when a mouse hover occurs over
        /// the element specified by locator1, and in that menu, there's a clickable element
        /// specified by locator2.
        /// </summary>
        /// <param name="locator1">The first element, eg: to hover over.</param>
        public void MoveToElement(By locator1)
        {
            Actions action = new Actions(_driver);
            action.MoveToElement(FindElement(locator1)).Perform();
        }

        /// <summary>
        /// Gets the selected option.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns>string</returns>
        public string GetSelectedOption(By locator)
        {
            var element = FindElementIfExists(locator);
            SelectElement oSelect = new SelectElement(element);
            return oSelect.SelectedOption.Text;
        }

        /// <summary>
        /// Switch to an iframe after waiting for its availability.
        /// </summary>
        /// <param name="locator"></param>
        public void SwitchToFrame(By locator)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_defaultWaitTime));
            wait.Until(WebElementExpectedConditions.FrameToBeAvailableAndSwitchToIt(locator));
        }

        /// <summary>
        /// Waits until page ready.
        /// </summary>
        public void WaitUntilPageReady()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_defaultWaitTime));
            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }

        /// <summary>
        /// Gets the window handles.
        /// </summary>
        /// <returns>list of windows</returns>
        public List<string> GetWindowHandles()
        {
            return _driver.WindowHandles.ToList();
        }

        /// <summary>
        /// Switches to window.
        /// </summary>
        /// <param name="window">The window.</param>
        public void SwitchToWindow(string window)
        {
            _driver.SwitchTo().Window(window);
        }

        /// <summary>
        /// Gets the current URL.
        /// </summary>
        /// <returns>returns the current url </returns>
        public string GetCurrentURL()
        {
            return _driver.Url;
        }

        /// <summary>
        /// Gets the elements count.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        public int GetElementsCount(By locator)
        {
            return FindElements(locator).Count();
        }

        /// <summary>
        /// Moves to element and click.
        /// </summary>
        /// <param name="locator">The locator.</param>
        public void MoveToElementAndClick(By locator)
        {
            Actions action = new Actions(_driver);
            action.MoveToElement(FindElement(locator)).Click().Perform();
        }

        /// <summary>
        /// Moves to element and click.
        /// </summary>
        public void refreshPage()
        {
            _driver.Navigate().Refresh();
        }

        /// <summary>
        /// Check if the elements are clickable
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        public List<IWebElement> GetElements(By locator)
        {
            List<IWebElement> elements = FindElements(locator).ToList();
            return elements;
        }

        /// <summary>
        /// Determines whether the element is Clickable
        /// </summary>
        /// <param name="element">The element to check for cilckability</param>
        /// <returns>
        ///   <c>true</c> if [is element is clickable] [using the specified locator]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsElementClickable(IWebElement element)
        {
            try
            {
                WaitUntilElementClickable(element);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether [is element visible] [the specified locators].
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="customWaitTime">customWaitTime.</param>
        public bool IsElementVisible(By locator, int customWaitTime)
        {
            try
            {
                WaitUntilElementIsVisible(locator, customWaitTime);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Wait until the element with the given text is visible on the page.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="customWaitTime">The custom wait time.</param>
        public void WaitUntilElementIsVisible(By locator, int customWaitTime)
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(customWaitTime))
            .Until(_driver => _driver.FindElement(locator).Displayed);
        }

        /// <summary>
        /// Moves to element and perform.
        /// </summary>
        /// <param name="locator">The locator.</param>
        public void MoveToElementAndPerform(By locator)
        {
            Actions action = new Actions(_driver);
            action.MoveToElement(FindElement(locator)).Perform();
        }

    }
}

