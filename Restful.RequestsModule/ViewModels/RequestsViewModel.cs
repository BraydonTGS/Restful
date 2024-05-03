using Prism.Regions;
using Restful.Core.Constant;
using Restful.Core.ViewModels;
using Restful.RequestsModule.Views;
using System;
using System.Threading.Tasks;

namespace Restful.RequestsModule.ViewModels
{
    public partial class RequestsViewModel : RegionViewModelBase
    {
        public RequestsViewModel(IRegionManager regionManager) : base(regionManager)
        {
        }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            // Load all of the Requests for the Current User //
            LoadRequestsAsync();
            NavigateToRequestsTree();
        }

        #region LoadRequestsAsync
        private async Task LoadRequestsAsync()
        {
            await Task.Delay(1000);
        }
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
