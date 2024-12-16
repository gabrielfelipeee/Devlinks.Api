namespace Api.Service.Shared.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message)
        { }
    }
}
