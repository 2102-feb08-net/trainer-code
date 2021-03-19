using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApp.Business.TypiCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailApp.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypicodeController : ControllerBase
    {
        private readonly TypiCodeService _typiCode;

        public TypicodeController(TypiCodeService typiCode)
        {
            _typiCode = typiCode;
        }

        [HttpGet("users")]
        [Authorize] // auto respond with a 401 if you're not logged in
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<User> result = await _typiCode.GetUsersAsync();

            return Ok(result);
        }
    }
}
