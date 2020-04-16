using BlazorServerSelenium;
using BlazorServerSelenium.Integration.Tests;
using BlazorServerSelenium.Integration.Tests.Helpers;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SupplyChainCloud.UI.Integration.Tests
{
    public class LoginTests : BaseTest
    {
        public LoginTests(SeleniumServerFactory<Startup> server, ITestOutputHelper outputHelper) : base(server, outputHelper)
        {
        }

        /// <summary>
        /// Tests that cookie acceptance modal is visble on first login and disappears after accepting cookies.
        /// </summary>
        /// <param name="browserName">The browser to run the test in.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Theory]
        [InlineData("Chrome")]

        // TODO: Andrew, I'm supressing this for now, does this need to be async for testing purposes?
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task Login(string browserName)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            SetBrowser(browserName);

            Browser.Navigate().GoToUrl("https://localhost:44377");
        }
    }
}