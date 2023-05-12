using System.IO;
using System.Net.Http;
using System.Text;
using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

namespace Flurl.Http.Xml.Tests.Factories
{
    public class EchoHttpClientFactory : DefaultHttpClientFactory
    {
        private static string HttpRequestBodyToString(HttpRequest httpRequest)
        {
            string result;

            httpRequest.EnableBuffering();

            using (var reader = new StreamReader(httpRequest.Body, Encoding.UTF8, true, 1024, true))
            {
                result = reader.ReadToEnd();
            }

            httpRequest.Body.Position = 0;

            return result;
        }

        private HttpClient GetClient()
        {
            //var builder = new WebHostBuilder().Configure(app =>
            //{
            //    app.Use(async (context, next) =>
            //    {
            //        string requestBody = HttpRequestBodyToString(context.Request);
            //        await context.Response.WriteAsync(requestBody);
            //    });
            //});

            //var server = new TestServer(builder) { AllowSynchronousIO = true };
            //return server.CreateClient();

            return null;
        }

        public override HttpClient CreateHttpClient(HttpMessageHandler handler) => GetClient();
    }
}