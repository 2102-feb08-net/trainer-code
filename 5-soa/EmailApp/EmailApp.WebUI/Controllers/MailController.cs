using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApp.Business;
using EmailApp.WebUI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EmailApp.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MailController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        // GET api/mail
        [HttpGet]
        public async Task<IEnumerable<Message>> Get()
        {
            var messages = await _messageRepository.ListAsync();
            return messages.Select(e => new Message
            {
                Id = e.Id,
                Date = e.OrigDate,
                From = e.From,
                Subject = e.Subject,
                Body = e.Body
            });
        }

        // GET api/mail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            if (await _messageRepository.GetAsync(id) is not Email email)
            {
                return NotFound();
            }
            return new Message
            {
                Id = email.Id,
                Date = email.OrigDate,
                From = email.From,
                Subject = email.Subject,
                Body = email.Body
            };
        }

        // POST api/mail
        [HttpPost]
        public async Task<IActionResult> SendMessage(MessageInput message)
        {
            var email = new Email
            {
                OrigDate = (DateTimeOffset)message.Date,
                From = message.From,
                Subject = message.Subject,
                Body = message.Body,
            };
            var createdEmail = await _messageRepository.CreateAsync(email);
            var result = new Message
            {
                Id = createdEmail.Id,
                Date = createdEmail.OrigDate,
                From = createdEmail.From,
                Subject = createdEmail.Subject,
                Body = createdEmail.Body
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
