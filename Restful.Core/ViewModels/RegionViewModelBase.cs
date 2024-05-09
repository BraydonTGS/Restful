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

        #region IsNavigationTarget
        /// <summary>
        /// Determines whether the view associated with the specified navigation context should handle the navigation request.
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns>true if this view should handle the navigation request; otherwise, false.</returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;
        #endregion

        #region OnNavigatedFrom
        /// <summary>
        /// Called when the navigator that this view is attached to navigates away from the view.
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext) { }
        #endregion

        #region OnNavigatedTo
        /// <summary>
        /// Called when the navigator that this view is attached to navigates to the view.
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext) { }
        #endregion

        #region RequestNavigate
        /// <summary>
        /// Requests navigation to a specified view within a specified region.
        /// </summary>
        /// <param name="regionName">The name of the region to navigate to.</param>
        /// <param name="viewName">The name of the view to navigate to.</param>
        /// <param name="parameters">Optional navigation parameters that may be passed to the target view.</param>
        public void RequestNavigate(string regionName, string viewName, NavigationParameters? parameters = null)
        {
            var uri = new Uri(viewName, UriKind.Relative);
            _regionManager.RequestNavigate(regionName, uri, parameters);
        }
        #endregion
    }
}
