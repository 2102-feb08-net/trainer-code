using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApp.Business;
using EmailApp.Business.Exceptions;
using EmailApp.WebUI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmailApp.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUnitOfWork _unitOfWork;

        public MailController(IAuthorizationService authorizationService, IUnitOfWork unitOfWork)
        {
            _authorizationService = authorizationService;
            _unitOfWork = unitOfWork;
        }

        // GET api/mail
        [HttpGet]
        //[Authorize(Roles = "admin")]
        public async Task<IEnumerable<Message>> Get()
        {
            var messages = await _unitOfWork.MessageRepository.ListAsync();
            return messages.Select(e => new Message
            {
                Id = e.Id,
                Date = e.OrigDate,
                From = e.From,
                To = e.To,
                Subject = e.Subject,
                Body = e.Body
            });
        }

        // GET api/mail/f81d4fae-7dec-11d0-a765-00a0c91e6bf6
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(Guid id)
        {
            if (await _unitOfWork.MessageRepository.GetByIdAsync(id) is not Email email)
            {
                return NotFound();
            }

            var allowed = new[] { email.From, email.To }.Where(s => s is not null);
            var authorizationResult = await _authorizationService
                .AuthorizeAsync(user: User, resource: allowed, policyName: "AllowedAddresses");
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            return new Message
            {
                Id = email.Id,
                Date = email.OrigDate,
                From = email.From,
                To = email.To,
                Subject = email.Subject,
                Body = email.Body
            };
        }

        // POST api/mail
        [HttpPost]
        public async Task<IActionResult> SendMessage(MessageInput message)
        {
            var authorizationResult = await _authorizationService
                .AuthorizeAsync(user: User, resource: new[] { message.From }, policyName: "AllowedAddresses");
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var email = new Email
            {
                OrigDate = message.Date ?? DateTimeOffset.Now,
                From = message.From,
                To = message.To,
                Subject = message.Subject,
                Body = message.Body,
            };
            try
            {
                await _unitOfWork.MessageRepository.AddAsync(email);
            }
            catch (OriginatorAddressNotFoundException)
            {
                ModelState.AddModelError(nameof(email.From), $"Originator {email.From} not found.");
                return BadRequest(ModelState);
            }
            catch (DestinationAddressNotFoundException)
            {
                ModelState.AddModelError(nameof(email.To), $"Destination {email.To} not found.");
                return BadRequest(ModelState);
            }
            await _unitOfWork.SaveAsync();
            var result = new Message
            {
                Id = email.Id,
                Date = email.OrigDate,
                From = email.From,
                To = email.To,
                Subject = email.Subject,
                Body = email.Body
            };

            // if you put something wrong here for action name or parameters,
            // this will throw an exception during result execution. exceptions thrown after the response
            //   has started to be written to, asp.net just truncates the response
            //      this is sometimes interpreted by the browser as a CORS error (who knows why)
            return CreatedAtAction(
                actionName: nameof(GetMessage),
                routeValues: new { id = result.Id },
                value: result);
        }
    }
}
