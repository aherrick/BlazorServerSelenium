using BlazorServerSelenium.Integration.Tests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;
using Xunit.Abstractions;

namespace BlazorServerSelenium.Integration.Tests
{
    /// <summary>
    /// This class should be inherited in all test classes.
    /// </summary>
    [Collection("selenium")]
    public class BaseTest : IDisposable
    {
        public SeleniumServerFactory<Startup> Factory { get; }

        protected WebDriverWait StandardWait { get; set; }
        protected IWebDriver Browser { get; set; }
        protected ITestOutputHelper Output { get; }

        public BaseTest(SeleniumServerFactory<Startup> factory, ITestOutputHelper outputHelper)
        {
            Output = outputHelper;

            Factory = factory ?? throw new ArgumentNullException(nameof(factory));

            factory.CreateClient();

            Factory.RootUrl = new Uri("https://localhost:44377");
        }

        /// <summary>
        /// Get a browser object based on the name of the browser.
        /// </summary>
        /// <param name="browserName">The browser name being requested.</param>
        public void SetBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Chrome":
                    var chromeOpts = new ChromeOptions();
                    chromeOpts.SetLoggingPreference(LogType.Browser, LogLevel.All);
                    chromeOpts.AddArgument("disable-gpu");
                    chromeOpts.AddArgument("disable-software-rasterizer");
                    chromeOpts.AddArgument("window-size=1366,768");
                    chromeOpts.AddArgument("ignore-certificate-errors");

                    Browser = new ChromeDriver(chromeOpts);
                    break;

                default:
                    throw new Exception("Invalid browser name.");
            }

            if (Browser == null)
            {
                throw new Exception("Browser initialization failed.");
            }

            Browser.Manage().Cookies.DeleteAllCookies();

            StandardWait = new WebDriverWait(Browser, TimeSpan.FromSeconds(60));
        }

        public void Dispose()
        {
        }
    }
}