using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SeleniumTestForWiki
{
    [TestFixture]
    public class WikiTests
    {
        #region Consts

        private static string WIKI_URL = "https://en.wikipedia.org/wiki/Main_Page";
        private static string SEARCH_KEYWORD_SELENIUM = "Selenium (software)";
        private static string SEARCH_KEYWORD_AAXXZZ = "AAXXZZ";
        private static string SEARCH_INPUT_ID = "searchInput";
        private static string FIRST_HEADING_ID = "firstHeading"; 
        private static string DOESNT_EXIST_CLASSNAME = " mw-search-createlink";

        #endregion

        #region Private Fields

        private IWebDriver _driver;

        #endregion

        #region SetUp & TearDown

        [SetUp]
        public void InitTests()
        {
            // new Chrome auto-window opens
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void FinalizeTests()
        {
            // close current Chrome window
            _driver.Quit();
        }

        #endregion

        #region Tests

        [Test]
        public void DoesPageExist_Test()
        {
            GoToPageWithAKeyword(SEARCH_KEYWORD_SELENIUM);

            IWebElement firstHeading = _driver.FindElement(By.Id(FIRST_HEADING_ID));

            Assert.AreEqual(SEARCH_KEYWORD_SELENIUM, firstHeading.Text);
        }

        [Test]
        public void DoesPageAbsent_Test()
        {
            GoToPageWithAKeyword(SEARCH_KEYWORD_AAXXZZ);
            string dneText = $"The page \"{SEARCH_KEYWORD_AAXXZZ}\" does not exist.";

            IWebElement firstHeading = _driver.FindElement(By.ClassName(DOESNT_EXIST_CLASSNAME));
            
            Assert.IsTrue(firstHeading.Text.Contains(dneText));
        }

        #endregion

        #region Private Methods

        private void GoToPageWithAKeyword(string keyWord)
        {
            // open Wiki main page
            _driver.Navigate().GoToUrl(WIKI_URL);

            // searching of specific page by entering data and pressing OK
            IWebElement searchInput = _driver.FindElement(By.Id(SEARCH_INPUT_ID));
            searchInput.SendKeys(keyWord);
            searchInput.SendKeys(Keys.Enter);
        }


        #endregion

    }
}
