using System.Collections.Generic;
using System.Threading.Tasks;
using EmailApp.Business.TypiCode;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous] // override any auth requirements set on the controller or globally in startup
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<User> result = await _typiCode.GetUsersAsync();

            return Ok(result);
        }
    }
}
