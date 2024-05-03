
namespace Restful.Core.Errors
{
    /// <summary>
    /// Defines the Functionality of any IErrorHandler
    /// </summary>
    public interface IErrorHandler
    {
        void DisplayExceptionMessage(Exception ex, string message = "");
    }
}