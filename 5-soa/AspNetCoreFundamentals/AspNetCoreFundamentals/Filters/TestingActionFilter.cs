using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AspNetCoreFundamentals.Filters
{
    public class TestingActionFilter : IActionFilter
    {
        private readonly ILogger<TestingActionFilter> _logger;

        public TestingActionFilter(ILogger<TestingActionFilter> logger) => _logger = logger;

        public void OnActionExecuting(ActionExecutingContext context) =>
            _logger.LogDebug(MethodBase.GetCurrentMethod().Name);

        public void OnActionExecuted(ActionExecutedContext context) =>
            _logger.LogDebug(MethodBase.GetCurrentMethod().Name);
    }
}
