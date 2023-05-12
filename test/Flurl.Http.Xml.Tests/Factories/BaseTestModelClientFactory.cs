using System.Net.Http;
using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

namespace Flurl.Http.Xml.Tests.Factories
{
    public abstract class BaseTestModelClientFactory : DefaultHttpClientFactory
    {
        private readonly string _responseBody;

        protected BaseTestModelClientFactory(string responseBody)
        {
            _responseBody = responseBody;
        }

        private HttpClient GetClient()
        {
            return base.CreateHttpClient(base.CreateMessageHandler());
        }

        public override HttpClient CreateHttpClient(HttpMessageHandler handler) => GetClient();
    }
}
