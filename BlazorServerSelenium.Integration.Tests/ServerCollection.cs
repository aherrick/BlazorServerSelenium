using BlazorServerSelenium.Integration.Tests.Helpers;
using Xunit;

namespace BlazorServerSelenium.Integration.Tests
{
    [CollectionDefinition("selenium")]
    public class ServerCollection : ICollectionFixture<SeleniumServerFactory<Startup>>
    {
    }
}