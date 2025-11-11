using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Products.Global
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        public GlobalExceptionFilter()
        {
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            if (context.Exception is InvalidOperationException)
            {
                SetJsonResult(context, StatusCodes.Status400BadRequest, context.Exception.Message, LogLevel.Warning);
            }
            else
            {
                SetJsonResult(context, StatusCodes.Status500InternalServerError, context.Exception.Message, LogLevel.Error);
            }
        }


        private void SetJsonResult(ExceptionContext context, int status, string message, LogLevel logLevel)
        {
            context.Result = new JsonResult(message) { StatusCode = status };
        }

    }
}
