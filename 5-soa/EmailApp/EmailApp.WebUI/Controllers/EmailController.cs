using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EmailApp.Business;
using EmailApp.DataAccess;
using EmailApp.WebUI.Models;
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
        public async Task<IEnumerable<Message>> GetInboxAsync()
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

        [HttpGet("api/messages/{id}")]
        public async Task<Message> GetMessageAsync(int id)
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
        public async Task SendMessageAsync(Message message, string option)
        {
            // could do custom server-side validation right here

            var email = new Email
            {
                Body = message.Body,
                Sent = message.Date,
                From = message.From,
                Subject = message.Subject
            };
            _messageRepository.Create(email);
            await _messageRepository.SaveAsync();
        }

        [HttpPost("api/clean-inbox")]
        public async Task CleanInboxAsync()
        {
            await _inboxCleaner.CleanInboxAsync();
        }

        [NonAction]
        public void HelperMethod()
        {
        }
    }
}
