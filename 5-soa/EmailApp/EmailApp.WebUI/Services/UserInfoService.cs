using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace EmailApp.WebUI.Services
{
    public class UserInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInfoService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        //public async Task<object> GetUserInfoAsync()
        //{
        //    var httpContext = _httpContextAccessor.HttpContext;
        //    string accessToken = await httpContext.GetTokenAsync("access_token");
        //    //httpContext.
        //    var request = new HttpRequestMessage(HttpMethod.Get, "");
        //}
    }
}
