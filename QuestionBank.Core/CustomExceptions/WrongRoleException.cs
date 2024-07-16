namespace TouchStudy.Core.CustomExceptions
{
    public class WrongRoleException : Exception
    {
        public const int StatusCode = 403;
        public WrongRoleException(string message) : base(message)
        {
        }
    }
}
