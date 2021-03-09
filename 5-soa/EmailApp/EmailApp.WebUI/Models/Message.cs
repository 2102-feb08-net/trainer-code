using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApp.WebUI.Models
{
    public class Message : IValidatableObject
    {
        public int Id { get; set; }

        public string From { get; set; }

        public DateTimeOffset Date { get; set; }

        [Required]

        public string Subject { get; set; }

        public string Body { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            //if (string.IsNullOrWhiteSpace(Subject))
            //{
            //    results.Add(new ValidationResult(
            //        $"Subject is required",
            //        new[] { nameof(Subject) }));
            //}
            return results;
        }
    }
}
