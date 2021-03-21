using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Threading.Tasks;
using EmailApp.Business.TypiCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace EmailApp.IntegrationTests
{
    public class HttpIntegrationTests : IClassFixture<SqliteWebApplicationFactory<WebUI.Startup>>
    {
        private readonly HttpClient _client;

        public HttpIntegrationTests(SqliteWebApplicationFactory<WebUI.Startup> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetUsers_ValidJson()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/typicode/users");
            Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
            // assert on any headers etc
            Assert.StartsWith(MediaTypeNames.Application.Json, response.Content.Headers.ContentType.MediaType);

            var users = await response.Content.ReadFromJsonAsync<List<User>>();
            // relies on data in actual typicode website. we could instead
            // add a mock service in our WebApplicationFactory
            Assert.True(users.Any());
        }

        [Theory]
        [InlineData("/api/mail")]
        [InlineData("/api/mail/1")]
        [InlineData("/api/mailbox/a@b.c")]
        public async Task Get_Unauthorized(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            Assert.Equal(expected: HttpStatusCode.Unauthorized, actual: response.StatusCode);
        }
    }
}
