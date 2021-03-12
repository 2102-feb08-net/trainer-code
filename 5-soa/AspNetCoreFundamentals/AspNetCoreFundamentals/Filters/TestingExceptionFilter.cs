using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AspNetCoreFundamentals.Filters
{
    public class TestingExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<TestingExceptionFilter> _logger;

        public TestingExceptionFilter(ILogger<TestingExceptionFilter> logger) => _logger = logger;

        public void OnException(ExceptionContext context) =>
            _logger.LogDebug(MethodBase.GetCurrentMethod().Name);
    }
}
