using System;

namespace EmailApp.WebUI.Dtos
{
    public class Message
    {
        public int Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
