namespace Restful.Global.Exceptions
{
    public class UsernameAlreadyRegisteredException : Exception
    {
        public UsernameAlreadyRegisteredException(string message) : base(message) { }

        public UsernameAlreadyRegisteredException(string message, Exception innerException) : base(message, innerException) { }
    }
}
