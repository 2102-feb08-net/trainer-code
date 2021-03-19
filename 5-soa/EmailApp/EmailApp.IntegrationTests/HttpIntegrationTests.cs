using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using EmailApp.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace EmailApp.IntegrationTests
{
    public class HttpIntegrationTests : IClassFixture<SqliteWebApplicationFactory<WebUI.Startup>>
    {
        private readonly HttpClient _client;
        private readonly SqliteWebApplicationFactory<WebUI.Startup>
            _factory;

        public HttpIntegrationTests(SqliteWebApplicationFactory<WebUI.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetInbox_ValidJson()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/inbox");
            Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
            // assert on any headers etc
            Assert.StartsWith(MediaTypeNames.Application.Json, response.Content.Headers.ContentType.MediaType);

            var messages = await response.Content.ReadFromJsonAsync<List<Message>>();
            // relies on data in context HasData
            Assert.Equal(2, messages.Count);
            Assert.Equal("qc", messages[0].Subject);
        }
    }
}
