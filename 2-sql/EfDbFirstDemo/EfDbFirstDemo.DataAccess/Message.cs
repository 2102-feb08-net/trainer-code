using System;
using System.Collections.Generic;

#nullable disable

namespace EfDbFirstDemo.DataAccess
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public virtual Person Receiver { get; set; }
        public virtual Person Sender { get; set; }
    }
}
