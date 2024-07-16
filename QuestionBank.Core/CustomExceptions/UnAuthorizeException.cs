namespace TouchStudy.Core.CustomExceptions
{
    public class UnAuthorizeException : Exception
    {
        public const int StatusCode = 403;
        public UnAuthorizeException(string message) : base(message)
        {
        }
    }
}
