using Prism.Regions;
using Restful.Core.ViewModels;

namespace Restful.RequestsModule.ViewModels
{
    public partial class RequestDetailsViewModel : RegionViewModelBase
    {
        public RequestDetailsViewModel(IRegionManager regionManager) : base(regionManager)
        {
        }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }
    }
}
