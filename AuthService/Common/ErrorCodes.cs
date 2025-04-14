namespace AuthService.Common
{
    public static class ErrorCodes
    {
        public const string Unknown = "UNKNOWN";
        public const string Validation = "VALIDATION_ERROR";
        public const string NotFound = "NOT_FOUND";
        public const string InternalServer = "INTERNAL_SERVER_ERROR";
        public const string AuthenticationFailed = "AUTH_FAILED";
    }
}
