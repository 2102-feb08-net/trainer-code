using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EmailApp.Business;
using EmailApp.DataAccess;
using EmailApp.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailApp.WebUI.Controllers
{
    [ApiController]
    public class EmailController : ControllerBase
    {
        // each method here ("action method") will respond to one type of AJAX request
        // from the app, and optionally return an object (will be serialized to
        // json by ASP.NET and System.Text.Json in the response body)

        private readonly IMessageRepository _messageRepository;
        private readonly IInboxCleaner _inboxCleaner;
        private readonly ITimeProvider _timeProvider;

        public EmailController(IMessageRepository messageRepository, IInboxCleaner inboxCleaner, ITimeProvider timeProvider)
        {
            _messageRepository = messageRepository;
            _inboxCleaner = inboxCleaner;
            _timeProvider = timeProvider;
        }

        // distinguish what HTTP method (GET, POST, etc.) this will accept, and, what URL
        [HttpGet("api/inbox")]
        public async Task<IEnumerable<Message>> GetInbox()
        {
            var messages = await _messageRepository.ListAsync();
            return messages.Select(e => new Message
            {
                Body = e.Body,
                Date = e.Sent,
                From = e.From,
                Id = e.Id, 
                Subject = e.Subject
            });
        }


        [HttpGet("api/users/{user}/inbox")]
        [Authorize]
        public async Task<IActionResult> GetUserInbox(string user)
        {
            if (User.Identity.Name != user)
            {
                return Forbid("not authorized to view another user's inbox");
            }

            var messages = await _messageRepository.ListAsync();
            return Ok(messages.Select(e => new Message
            {
                Body = e.Body,
                Date = e.Sent,
                From = e.From,
                Id = e.Id,
                Subject = e.Subject
            }));
        }

        [HttpGet("api/messages/{id}")]
        public async Task<Message> GetMessage(int id)
        {
            var email = await _messageRepository.GetAsync(id);
            return new Message
            {
                Body = email.Body,
                Date = email.Sent,
                From = email.From,
                Id = email.Id,
                Subject = email.Subject
            };
        }

        // "model binding" (useful feature of ASP.NET)
        // will deserialize data in the request body (JSON text)
        // into the action method parameters.
        [HttpPost("api/outbox")]
        public async Task<IActionResult> SendMessage(Message message, string option)
        {
            // could do custom server-side validation right here

            var email = new Email
            {
                Body = message.Body,
                Sent = message.Date,
                From = message.From,
                Subject = message.Subject
            };
            var createdEmail = await _messageRepository.CreateAsync(email);
            var result = new Message
            {
                Body = createdEmail.Body,
                Date = createdEmail.Sent,
                From = createdEmail.From,
                Subject = createdEmail.Subject
            };
            // if you put something wrong here for action name or parameters,
            // this wil throw an exception during result execution. exceptions thrown after the response
            //   has started to be written to, asp.net just truncates the response
            //      this is sometimes interpreted by the browser as a CORS error (who knows why)
            return CreatedAtAction(nameof(GetMessage), new { id = createdEmail.Id }, result);
        }

        [HttpPost("api/clean-inbox")]
        public async Task CleanInbox()
        {
            await _inboxCleaner.CleanInboxAsync();
        }

        [NonAction]
        public void HelperMethod()
        {
        }
    }
}
