using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AspNetCoreFundamentals.Filters
{
    public class TestingResourceFilter : IResourceFilter
    {
        private readonly ILogger<TestingResourceFilter> _logger;

        public TestingResourceFilter(ILogger<TestingResourceFilter> logger) => _logger = logger;

        public void OnResourceExecuting(ResourceExecutingContext context) =>
            _logger.LogDebug(MethodBase.GetCurrentMethod().Name);

        public void OnResourceExecuted(ResourceExecutedContext context) =>
            _logger.LogDebug(MethodBase.GetCurrentMethod().Name);
    }
}
