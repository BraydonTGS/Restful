using Prism.Regions;

namespace Restful.Core.ViewModels
{
    public partial class RegionViewModelBase : ViewModelBase, INavigationAware
    {
        protected IRegionManager _regionManager { get; private set; }
        public RegionViewModelBase(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;
        public virtual void OnNavigatedFrom(NavigationContext navigationContext) { }
        public virtual void OnNavigatedTo(NavigationContext navigationContext) { }
        public void RequestNavigate(string regionName, string viewName, NavigationParameters? parameters = null) 
        { 
            var uri = new Uri(viewName, UriKind.Relative);
            _regionManager.RequestNavigate(regionName, uri, parameters);    
        }
    }
}
