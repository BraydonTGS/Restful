using Prism.Regions;
using Restful.Core.Constant;
using Restful.Core.Errors;
using Restful.Core.Extensions;
using Restful.Core.ViewModels;
using Restful.RequestsModule.Views;
using System;
using System.Threading.Tasks;


namespace Restful.RequestsModule.ViewModels
{
    public partial class RequestsViewModel : RegionViewModelBase
    {
        private readonly IErrorHandler _errorHandler;

        #region Constructor
        public RequestsViewModel(
            IRegionManager regionManager,
            IErrorHandler errorHandler) : base(regionManager)
        {
            _errorHandler = errorHandler;
        }
        #endregion

        #region OnNavigatedTo
        /// <summary>
        /// On Navigated To
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            // Load all of the Requests for the Current User //
            LoadRequestsAsync()
                .AwaitTask(TaskCompleted, HandleException);
        }
        #endregion

        #region LoadRequestsAsync
        /// <summary>
        /// For Now Simulate the Loading of the Requests from the API
        /// </summary>
        /// <returns></returns>
        private async Task LoadRequestsAsync() {await Task.Delay(1000); }

        #endregion

        #region TaskCompleted - Override
        /// <summary>
        /// Action that is Fired when the LoadRequestsAsync is Finished
        /// </summary>
        protected override void TaskCompleted() { NavigateToRequestsTree(); }
        #endregion

        #region TaskCompleted - Override
        /// <summary>
        /// Action that is Fired when the LoadRequestsAsync Fails and throws an Exception
        /// </summary>
        protected override void HandleException(Exception ex) { _errorHandler.DisplayExceptionMessage(ex); }
        #endregion

        #region NavigateToRequestsTree
        /// <summary>
        /// Navigate to the Requests Tree View
        /// </summary>
        private void NavigateToRequestsTree()
        {
            RequestNavigate(Regions.RequestsTreeRegion, nameof(RequestsTreeView));
        }
        #endregion
    }
}
