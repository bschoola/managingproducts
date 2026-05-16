using Api.Products.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Products.Global
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            if (context.Exception is InvalidOperationException)
            {
                _logger.LogWarning(context.Exception, "Business rule violation: {Message}", context.Exception.Message);
                SetJsonResult(context, StatusCodes.Status400BadRequest, context.Exception.Message);
            }
            else
            {
                _logger.LogError(context.Exception, "Unhandled exception: {Message}", context.Exception.Message);
                SetJsonResult(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        private static void SetJsonResult(ExceptionContext context, int status, string message)
        {
            context.Result = new JsonResult(new ErrorResponse { Error = message }) { StatusCode = status };
        }
    }
}
