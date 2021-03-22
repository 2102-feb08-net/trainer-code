using System;
using System.Collections.Generic;

namespace EmailApp.DataAccess.EfModel
{
    public class Account
    {
        public int Id { get; set; }
        public string Address { get; set; }

        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
    }
}
