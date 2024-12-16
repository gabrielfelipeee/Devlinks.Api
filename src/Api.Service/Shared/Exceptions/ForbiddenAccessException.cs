namespace Api.Service.Shared.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException(string message) : base(message)
        { }
    }
}
