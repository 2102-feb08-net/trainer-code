using System;

namespace EmailApp.DataAccess.EfModel
{
    public class Message
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTimeOffset OrigDate { get; set; }
        public int FromId { get; set; }
        public int? ToId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsDeleted { get; set; }

        public Account From { get; set; }
        public Account To { get; set; }
    }
}
