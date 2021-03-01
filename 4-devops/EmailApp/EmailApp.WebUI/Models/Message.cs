using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApp.WebUI.Models
{
    public class Message
    {
        public string From { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Subject { get; set; }
    }
}
