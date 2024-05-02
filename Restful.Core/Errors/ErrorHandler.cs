using System.Windows;

namespace Restful.Core.Errors
{
    /// <summary>
    /// Used to Handle Errors in the User Interface
    /// </summary>
    public class ErrorHandler : IErrorHandler
    {
        #region DisplayExceptionMessage
        /// <summary>
        /// Display a Basic Error Message when an Exception is Caught
        /// </summary>
        /// <param name="ex"></param>
        public void DisplayExceptionMessage(Exception ex)
        {
            if (ex == null) return;
            MessageBox.Show($"An unexpected error occurred: {ex.Message}",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion
    }
}
