using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace AwesomeLists.Services.Filters
{
    public sealed class LoggerExceptionHandlingFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilterAttribute> _logger;

        public LoggerExceptionHandlingFilter(ILogger<ExceptionFilterAttribute> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "error occured");
        }
    }
}
