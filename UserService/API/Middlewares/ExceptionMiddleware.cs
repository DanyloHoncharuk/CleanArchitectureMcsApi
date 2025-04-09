using System.Diagnostics;
using UserService.API.Common;
using UserService.Common;

namespace UserService.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var traceId = Activity.Current?.TraceId.ToString();
            var errorCode = ErrorCodes.InternalServer;
            var message = "An unexpected error occurred.";

            switch (exception)
            {
                case ArgumentException:
                    message = exception.Message;
                    errorCode = ErrorCodes.Validation;
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case KeyNotFoundException:
                    message = exception.Message;
                    errorCode = ErrorCodes.NotFound;
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            return context.Response.WriteAsJsonAsync(new ErrorResponse()
            {
                Message = message,
                ErrorCode = errorCode,
                TraceId = traceId
            });
        }
    }
}
