namespace Restful.Global.Exceptions
{
    /// <summary>
    /// Exception that is thrown if the User already has a Given Request that Exists in the Database
    /// </summary>
    public class RequestAlreadyExistsException : Exception
    {
        public RequestAlreadyExistsException(string message) : base(message) { }

        public RequestAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
