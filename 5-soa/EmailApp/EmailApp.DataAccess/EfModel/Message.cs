using System;

namespace EmailApp.DataAccess.EfModel
{
    public class Message
    {
        // EF Core conventions will do some things automatically

        public int Id { get; set; }

        public int FromId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTimeOffset Date { get; set; }

        // three ways EF Core can represent a relationship
        // between two objects -
        // 1. foreign key property (FromId)
        // 2. navigation property
        // 3. (reverse) navigation property

        // scaffold will make all 3,
        // but in code-first, you can leave as many of them
        // out as you want.
        // (if you leave out the foreign key property,
        //  EF will add it anyway at runtime as a "shadow property")

        public Account From { get; set; }
    }
}
