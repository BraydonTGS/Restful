namespace Restful.Core.Extensions
{
    /// <summary>
    /// Extension Methods for a Task
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Asynchronously await a task with optional completion and error callbacks.
        /// </summary>
        /// <param name="task"></param>
        public static async void AwaitTask(this Task task, Action? completedCallback = null, Action<Exception>? errorCallBack = null)
        {
            try
            {
                await task;
                completedCallback?.Invoke();
            }
            catch (Exception ex) { errorCallBack?.Invoke(ex); }
        }
    }
}
