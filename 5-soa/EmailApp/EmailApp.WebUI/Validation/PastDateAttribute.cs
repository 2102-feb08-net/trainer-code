using System;
using System.ComponentModel.DataAnnotations;

namespace EmailApp.WebUI.Validation
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTimeOffset?)validationContext.ObjectInstance;

            if (date.HasValue && date < DateTimeOffset.Now)
            {
                return new ValidationResult("Date cannot be in the future");
            }

            return ValidationResult.Success;
        }
    }
}