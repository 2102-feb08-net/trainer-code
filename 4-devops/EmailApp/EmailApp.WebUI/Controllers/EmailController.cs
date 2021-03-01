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
        // each method here ("action method") will respond to one type of AJAX request from the
        // app, and return a JsonResult (will be serialized to json by ASP.NET)
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
    }
}
