using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreFundamentals.Validation
{
    public class TestingValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var logger = (ILogger<TestingValidationAttribute>)validationContext.GetService(typeof(ILogger<TestingValidationAttribute>));
            logger.LogDebug(MethodBase.GetCurrentMethod().Name);

            return ValidationResult.Success;
        }
    }
}
