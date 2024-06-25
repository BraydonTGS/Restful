namespace Restful.Global.Exceptions
{
    /// <summary>
    /// Exception that is thrown when there is an Error Initializing the Database for the Application
    /// </summary>
    public class InitializeDatabaseException : Exception
    {
        public InitializeDatabaseException(string message) : base(message) { }

        public InitializeDatabaseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
