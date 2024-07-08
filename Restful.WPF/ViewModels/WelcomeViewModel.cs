using Prism.Commands;
using Prism.Regions;
using Restful.Core.Errors;
using Restful.Core.Extensions;
using Restful.Core.ViewModels;
using Restful.WPF.Properties;
using System;
using System.Threading.Tasks;

namespace Restful.WPF.ViewModels
{
    public partial class WelcomeViewModel : RegionViewModelBase
    {
        private readonly IErrorHandler _errorHandler;

        public DelegateCommand CloseNotificationsTabCommand { get; set; }

        #region Constructor
        public WelcomeViewModel(
            IRegionManager regionManager,
            IErrorHandler errorHandler) : base(regionManager)
        {
            _errorHandler = errorHandler;

            CloseNotificationsTabCommand = new DelegateCommand(OnCloseNotificationsTabCommandExecuted);
        }
        #endregion

        #region OnNavigatedTo
        /// <inheritdoc/>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            // Load all of the Requests for the Current User //
            LoadNotificationsAsync()
                .AwaitTask(TaskCompleted, HandleException);
        }
        #endregion

        #region LoadRequestsAsync
        /// <summary>
        /// For Now Simulate the Loading of the Requests from the API
        /// </summary>
        /// <returns></returns>
        private async Task LoadNotificationsAsync()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;
                await Task.Delay(3000);
            }
            catch (Exception ex)
            {
                _errorHandler.DisplayExceptionMessage(ex);
                throw;
            }
            finally { IsBusy = false; }
        }

        #endregion

        #region TaskCompleted - Override
        /// <summary>
        /// Action that is Fired when the LoadNotificationsAsync is Finished
        /// </summary>
        protected override void TaskCompleted() { }
        #endregion

        #region TaskCompleted - Override
        /// <summary>
        /// Action that is Fired when the LoadNotificationsAsync Fails and throws an Exception
        /// </summary>
        protected override void HandleException(Exception ex) { _errorHandler.DisplayExceptionMessage(ex); }
        #endregion

        #region OnCloseNotificationsTabCommandExecuted
        /// <summary>
        /// 
        /// </summary>
        private void OnCloseNotificationsTabCommandExecuted()
        {
            Settings.Default.NotificationWindowClosed = true;
            Settings.Default.Save();
        }
        #endregion
    }
}
