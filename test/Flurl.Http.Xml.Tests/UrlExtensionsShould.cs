using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Flurl.Http.Testing;
using Flurl.Http.Xml.Tests.Factories;
using Flurl.Http.Xml.Tests.Models;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Flurl.Http.Xml.Tests
{
    public class UrlExtensionsShould : TestBase
    {
        private const string REQUEST_BODY_XML = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TestModel xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Number>3</Number>
  <Text>Test</Text>
</TestModel>";

        private void AssertTestModel(TestModel testModel, int expectedNumber, string expectedText)
        {
            Assert.Equal(expectedNumber, testModel.Number);
            Assert.Equal(expectedText, testModel.Text);
        }

        private void AssertXDocument(XDocument document, int expectedNumber, string expectedText)
        {
            Assert.Equal(expectedNumber.ToString(), document?.Element("TestModel")?.Element("Number")?.Value);
            Assert.Equal(expectedText, document?.Element("TestModel")?.Element("Text")?.Value);
        }

        [Fact, Order(3)]
        public async Task GetXmlAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .GetXmlAsync<TestModel>()
                .ConfigureAwait(false);

            AssertTestModel(result, 3, "Test");
        }

        [Fact, Order(3)]
        public async Task GetXDocumentAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .GetXDocumentAsync()
                .ConfigureAwait(false);

            AssertXDocument(result, 3, "Test");
        }

        [Fact, Order(3)]
        public async Task GetXElementsFromXPathAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .GetXElementsFromXPath("/TestModel")
                .ConfigureAwait(false);

            AssertXDocument(result.FirstOrDefault()?.Document, 3, "Test");
        }

        [Fact, Order(3)]
        public async Task GetXElementsFromXPathNamespaceResolverAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .GetXElementsFromXPath("/TestModel", new XmlNamespaceManager(new NameTable()))
                .ConfigureAwait(false);

            AssertXDocument(result.FirstOrDefault()?.Document, 3, "Test");
        }

        [Theory]
        [InlineData(HttpMethodTypes.Post)]
        [InlineData(HttpMethodTypes.Put)]
        public async Task SendXmlToModelAsync(HttpMethodTypes methodType)
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var method = HttpMethodByType[methodType];
            var result = await new Url("https://some.url")
                .SendXmlAsync(method, new TestModel { Number = 3, Text = "Test" })
                .ReceiveXml<TestModel>()
                .ConfigureAwait(false);

            AssertTestModel(result, 3, "Test");
        }

        [Theory]
        [InlineData(HttpMethodTypes.Post)]
        [InlineData(HttpMethodTypes.Put)]
        public async Task SendXmlToXDocumentAsync(HttpMethodTypes methodType)
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var method = HttpMethodByType[methodType];
            var result = await new Url("https://some.url")
                .SendXmlAsync(method, new TestModel { Number = 3, Text = "Test" })
                .ReceiveXDocument()
                .ConfigureAwait(false);

            AssertXDocument(result, 3, "Test");
        }

        [Fact]
        public async Task PostXmlToModelAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXml<TestModel>()
                .ConfigureAwait(false);

            AssertTestModel(result, 3, "Test");
        }

        [Fact]
        public async Task PostXmlToXDocumentAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXDocument()
                .ConfigureAwait(false);

            AssertXDocument(result, 3, "Test");
        }

        [Fact]
        public async Task PutXmlToModelAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .PutXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXml<TestModel>()
                .ConfigureAwait(false);

            AssertTestModel(result, 3, "Test");
        }

        [Fact]
        public async Task PutXmlToXDocumentAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .PutXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXDocument()
                .ConfigureAwait(false);

            AssertXDocument(result, 3, "Test");
        }
    }
}
