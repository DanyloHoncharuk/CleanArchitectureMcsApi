using AuthService.Common;

namespace AuthService.Application.Wrappers
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? ErrorCode { get; set; }
        public string? TraceId { get; set; }
        public T? Data { get; set; }

        public static OperationResult<T> SuccessResult(T? data, string? message = null)
        {
            return new()
            {
                Success = true,
                Message = message,
                TraceId = GetTraceId(),
                Data = data
            };
        }

        public static OperationResult<T> Failure(string message, string errorCode = ErrorCodes.Unknown)
        {
            return new()
            {
                Success = false,
                Message = message,
                ErrorCode = errorCode,
                TraceId = GetTraceId()
            };
        }

        private static string? GetTraceId()
        {
            return System.Diagnostics.Activity.Current?.TraceId.ToString();
        }
    }
}
