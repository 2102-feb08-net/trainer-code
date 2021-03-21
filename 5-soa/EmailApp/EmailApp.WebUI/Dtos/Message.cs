using System;

namespace EmailApp.WebUI.Dtos
{
    public class Message
    {
        public Guid Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
