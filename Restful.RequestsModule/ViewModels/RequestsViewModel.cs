using Prism.Regions;
using Restful.Core.Errors;
using Restful.Core.Extensions;
using Restful.Core.Requests;
using Restful.Core.Requests.Models;
using Restful.Core.Users;
using Restful.Core.ViewModels;
using Restful.Global.Constant;
using Restful.RequestsModule.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace Restful.RequestsModule.ViewModels
{
    public partial class RequestsViewModel : RegionViewModelBase
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IRequestBL _requestBL;
        private readonly IApplicationUserService _applicationUserService;

        private Guid _collectionId;
        public ObservableCollection<Request> _requests { get; set; }

        #region Constructor
        public RequestsViewModel(
            IRegionManager regionManager,
            IErrorHandler errorHandler,
            IRequestBL requestBL,
            IApplicationUserService applicationUserService) : base(regionManager)
        {
            _errorHandler = errorHandler;
            _requestBL = requestBL;
            _applicationUserService = applicationUserService;

        }
        #endregion

        #region OnNavigatedTo
        /// <inheritdoc/>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                // Pull out the Collection ID //
                if (_requests is null || _requests.Count == 0)
                    LoadRequestsAsync()
                        .AwaitTask(TaskCompleted, HandleException);
            }
            catch (Exception ex) { _errorHandler.DisplayExceptionMessage(ex); }

        }
        #endregion

        #region LoadRequestsAsync
        /// <summary>
        /// For Now Simulate the Loading of the Requests from the API
        /// </summary>
        /// <returns></returns>
        private async Task LoadRequestsAsync()
        {
            try
            {
                _requests = new ObservableCollection<Request>();
                if (IsBusy) return;
                IsBusy = true;
                if (_collectionId != Guid.Empty)
                    _requests = await _requestBL.GetAllRequestsByCollectionIdAsync(_collectionId) as ObservableCollection<Request>;
                else
                    _requests = await _requestBL
                        .GetAllRequestsByUserIdAsync(_applicationUserService.GetApplicationUserGuid()) as ObservableCollection<Request>;
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
