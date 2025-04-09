using UserService.Common;

namespace UserService.API.Common
{
    public class ErrorResponse
    {
        public bool Success { get; set; } = false;
        public string? Message { get; set; }
        public string ErrorCode { get; set; } = ErrorCodes.Unknown;
        public string? TraceId { get; set; }
        public object? Data { get; set; } = null;
    }
}
