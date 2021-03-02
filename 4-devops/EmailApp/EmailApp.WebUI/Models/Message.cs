using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApp.WebUI.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string From { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
