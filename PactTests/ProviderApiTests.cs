using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using tests.XUnitHelpers;
using Xunit;
using Xunit.Abstractions;

namespace tests
{
    public class ProviderApiTests : IDisposable
    {
        private string _providerUri { get; }
        private string _pactServiceUri { get; }
        private IWebHost _webHost { get; }
        private ITestOutputHelper _outputHelper { get; }

        public ProviderApiTests(ITestOutputHelper output)
        {
            _outputHelper = output;
            _providerUri = "https://apigateway-service-test.clarksons.com";
            _pactServiceUri = "http://localhost:9001";

            _webHost = WebHost.CreateDefaultBuilder()
                .UseUrls(_pactServiceUri)
                .UseStartup<TestStartup>()
                .Build();

            _webHost.Start();
        }

        [Fact]
        public void EnsureProviderApiHonoursPactWithConsumer()
        {
            // Arrange
            config = new PactVerifierConfig
            {

                Outputters = new List<IOutput>
                                {
                                    new XUnitOutput(_outputHelper)
                                },


                Verbose = true
            };
            config.CustomHeader = new KeyValuePair<string, string>("X-Clarksons-Security-Cloud", "UqkejnjkHNM5gPY4VKeLNeoNv2eLUvZL8Di1xDqjc/I1dvdcQO9EGeUg6wYR0+ta+218kbu5Z5GgodKF92WmuGyTZGZ600fAS0OPKZ2kXIiwwqVO+a2apqxIOYLLrJdOFGkw6h6pZ8NurSQdQYvavA==");
       

            //Act / Assert
            pactVerifier = new PactVerifier(config);

            pactVerifier.ProviderState($"{_pactServiceUri}/provider-states")
                .ServiceProvider("fixture_api", _providerUri)
                .HonoursPactWith("EventAPIConsumer")
                .PactUri(@"http://104.214.219.231/pacts/provider/OperationServices/consumer/EventAPIConsumer/latest");
            pactVerifier.Verify();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        public static PactVerifierConfig config;
        public static PactVerifier pactVerifier;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _webHost.StopAsync().GetAwaiter().GetResult();
                    _webHost.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
