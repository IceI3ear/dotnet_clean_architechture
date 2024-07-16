namespace TouchStudy.Core.CustomExceptions
{
	public class AuthenticationException : Exception
    {
        public const int StatusCode = 401;
        public readonly AuthenticationMessage AuthenMessage;
        public AuthenticationException(string message) : base(message)
        {
        }
        public AuthenticationException(AuthenticationMessage authMessage, string detail) : base(detail)
        {
            AuthenMessage = authMessage;
        }
    }

    public enum AuthenticationMessage
    {
        NotAllowedToRegister,
        InvalidGoogleToken
    }
}

