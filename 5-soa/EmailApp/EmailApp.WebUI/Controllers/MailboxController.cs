using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EmailApp.Business;
using EmailApp.WebUI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmailApp.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailboxController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUnitOfWork _unitOfWork;

        public MailboxController(IAuthorizationService authorizationService, IUnitOfWork unitOfWork)
        {
            _authorizationService = authorizationService;
            _unitOfWork = unitOfWork;
        }

        // GET api/mailbox/c@a.test
        [HttpGet("{address}")]
        public async Task<ActionResult<List<Message>>> Get([EmailAddress] string address)
        {
            // authorization filter won't help, because it runs before model binding, and
            // this authorization logic depends on the results of model binding. so we need:
            // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/resourcebased
            // like [Authorize("...")], IAuthorizationService applies policies defined in Startup.ConfigureServices.

            // for DRY, this could be an action filter instead
            var authorizationResult = await _authorizationService
                .AuthorizeAsync(user: User, resource: new[] { address }, policyName: "AllowedAddresses");
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            IEnumerable<Email> messages = await _unitOfWork.MessageRepository.ListByRecipientAsync(address);
            return messages.Select(e => new Message
            {
                Id = e.Id,
                Date = e.OrigDate,
                From = e.From,
                To = e.To,
                Subject = e.Subject,
                Body = e.Body
            }).ToList();
        }

        // POST api/mailbox/c@a.test/clean
        [HttpPost("{address}/clean")]
        public async Task<IActionResult> CleanInbox(string address, [FromServices] InboxCleaner inboxCleaner)
        {
            var authorizationResult = await _authorizationService
                .AuthorizeAsync(user: User, resource: new[] { address }, policyName: "AllowedAddresses");
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            await inboxCleaner.CleanInboxAsync(address);

            return NoContent();
        }
    }
}
