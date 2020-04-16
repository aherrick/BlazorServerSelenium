#if DEBUG
#else
#define NOT_DEBUG //define a symbol specifying a non debug environment
#endif

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BlazorServerSelenium.Integration.Tests.Helpers
{
    public class SeleniumServerFactory<TStartup> : WebApplicationFactory<Startup>
         where TStartup : class
    {
        public Uri RootUrl { get; set; } // Save this use by tests
        public TestServer TestServer { get; private set; }
        public IWebHost HostWeb { get; set; }

        public SeleniumServerFactory()
        {
            WaitForBlockingProcesses();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder
                .UseUrls("https://localhost:44377")
                    .ConfigureKestrel(options =>
                    {
                    });
        }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            HostWeb = builder.Build();
            HostWeb.Start();

            // Fake Server
            TestServer = new TestServer(new WebHostBuilder().UseStartup<MockStartup>());

            return TestServer;
        }

        /// <summary>
        /// Waits to make sure all blocking processes have ended.
        /// </summary>
        /// <remarks>Is only called when application is run in Release mode.</remarks>
        [Conditional("NOT_DEBUG")]
        protected void WaitForBlockingProcesses()
        {
            var blockingProcesses = new List<string>
            {
                "chrome",
                "iisexpress"
            };

            foreach (var proc in Process.GetProcesses())
            {
                if (blockingProcesses.Contains(proc.ProcessName))
                {
                    proc.WaitForExit();
                    break;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                try
                {
                    HostWeb?.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    public class MockStartup
    {
        public void ConfigureServices()
        {
        }

        public void Configure()
        {
        }
    }
}