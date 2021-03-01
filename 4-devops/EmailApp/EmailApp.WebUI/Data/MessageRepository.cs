using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApp.WebUI.Models;

namespace EmailApp.WebUI.Data
{
    public class MessageRepository
    {
        private readonly List<Message> _inbox = new()
        {
            new()
            {
                From = "fred@fred.com",
                Date = DateTimeOffset.Parse("Mon, 01 Mar 2021 12:58:58 -0700"),
                Subject = "qc"
            },
            new()
            {
                From = "kevin@kevin.com",
                Date = DateTimeOffset.Parse("Mon, 01 Mar 2021 13:00:10 -0700"),
                Subject = "RE: qc"
            },
        };

        public IEnumerable<Message> List()
        {
            return _inbox;
        }
    }
}
