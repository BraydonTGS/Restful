using Prism.Regions;
using Restful.Core.Constant;
using Restful.Core.ViewModels;
using Restful.RequestsModule.Views;

namespace Restful.RequestsModule.ViewModels
{
    public partial class RequestsViewModel : RegionViewModelBase
    {
        public RequestsViewModel(IRegionManager regionManager) : base(regionManager)
        {
        }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavigateToRequestsTree();
        }


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
