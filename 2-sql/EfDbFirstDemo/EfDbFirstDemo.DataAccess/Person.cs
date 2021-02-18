using System;
using System.Collections.Generic;

#nullable disable

namespace EfDbFirstDemo.DataAccess
{
    public partial class Person
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime? AccountCreated { get; set; }
    }
}
