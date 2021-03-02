using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApp.WebUI.Data;
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

        private readonly MessageRepository _messageRepository;

        public EmailController()
        {
            _messageRepository = new MessageRepository();
        }

        // distinguish what HTTP method (GET, POST, etc.) this will accept, and, what URL
        [HttpGet("api/inbox")]
        public IEnumerable<Message> GetInbox()
        {
            return _messageRepository.List();
        }

        [HttpGet("api/message/{id}")]
        public Message GetMessage(int id)
        {
            return _messageRepository.Get(id);
        }

        // "model binding" (useful feature of ASP.NET)
        // will deserialize data in the request body (JSON text)
        // into the action method parameters.
        [HttpPost("api/send-message")]
        public void SendMessage(Message message)
        {
            _messageRepository.Create(message);
        }

        [NonAction]
        public void HelperMethod()
        {
        }
    }
}
