using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace EmailApp.Business.TypiCode
{
    public class TypiCodeService
    {
        private readonly HttpClient _client;

        public TypiCodeService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, MediaTypeNames.Application.Json);
            _client = client;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/users");
            HttpResponseMessage response = await _client.SendAsync(request);

            // at this point, we've loaded the headers & status code, but not yet the whole body
            //if (response.StatusCode == )
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<User>>();

            return result;
        }
    }
}
