using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AspNetCoreFundamentals.Filters
{
    public class TestingResultFilter : IResultFilter
    {
        private readonly ILogger<TestingResultFilter> _logger;

        public TestingResultFilter(ILogger<TestingResultFilter> logger) => _logger = logger;

        public void OnResultExecuting(ResultExecutingContext context) =>
            _logger.LogDebug(MethodBase.GetCurrentMethod().Name);

        public void OnResultExecuted(ResultExecutedContext context) =>
            _logger.LogDebug(MethodBase.GetCurrentMethod().Name);
    }
}
