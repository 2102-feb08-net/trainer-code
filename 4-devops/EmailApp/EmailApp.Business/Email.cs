using System;
using System.Collections.Generic;

namespace EmailApp.Business
{
    public class Email
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public DateTimeOffset Sent { get; set; }

        public List<Email> Previous { get; set; }
        public List<Email> Subsequent { get; set; }

        public bool IsSpam()
        {
            return From == "kevin@kevin.com";
        }
    }
}
