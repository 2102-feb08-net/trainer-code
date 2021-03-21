using System;
using System.Collections.Generic;

namespace EmailApp.Business
{
    public class Email
    {
        public Guid Id { get; set; }
        public DateTimeOffset OrigDate { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public List<Email> Previous { get; set; }
        public List<Email> Subsequent { get; set; }

        public bool IsSpam()
        {
            return From == "kevin@kevin.com";
        }
    }
}
