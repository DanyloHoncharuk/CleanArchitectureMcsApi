using System.Diagnostics;
using AuthService.API.Common;
using AuthService.Common;
using AuthService.Application.Exceptions;

namespace AuthService.API.Middlewares
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
            var statusCode = StatusCodes.Status500InternalServerError;

            switch (exception)
            {
                case ArgumentException:
                    message = exception.Message;
                    errorCode = ErrorCodes.Validation;
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
                case KeyNotFoundException:
                    message = exception.Message;
                    errorCode = ErrorCodes.NotFound;
                    statusCode = StatusCodes.Status404NotFound;
                    break;
                case AuthenticationException:
                    message = exception.Message;
                    errorCode = ErrorCodes.AuthenticationFailed;
                    statusCode = StatusCodes.Status401Unauthorized;
                    break;
            }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsJsonAsync(new ErrorResponse
            {
                Success = false,
                Message = message,
                ErrorCode = errorCode,
                TraceId = traceId,
                Data = null
            });
        }

    }
}
