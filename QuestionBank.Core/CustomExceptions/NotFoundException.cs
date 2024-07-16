namespace TouchStudy.Core.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public const int StatusCode = 404;
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
