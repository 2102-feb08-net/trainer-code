using System;
using System.ComponentModel.DataAnnotations;
using EmailApp.WebUI.Validation;

namespace EmailApp.WebUI.Dtos
{
    public class MessageInput
    {
        [PastDate]
        public DateTimeOffset? Date { get; set; }

        [Required]
        [EmailAddress]
        public string From { get; set; }

        [EmailAddress]
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
