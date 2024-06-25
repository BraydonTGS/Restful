using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Restful.Global.Constant;
using Restful.RequestsModule.ViewModels;
using Restful.RequestsModule.Views;

namespace Restful.RequestsModule
{
    /// <summary>
    /// Request Module - Register Types, Views, ViewModels, BL 
    /// </summary>
    public class ModuleRequestsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<RegionManager>();
            regionManager.RegisterViewWithRegion(Regions.RequestsTreeRegion, typeof(RequestsTreeView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Navigation //
            containerRegistry.RegisterForNavigation<RequestsView, RequestsViewModel>();
            containerRegistry.RegisterForNavigation<RequestsTreeView, RequestsTreeViewModel>();
            containerRegistry.RegisterForNavigation<RequestDetailsView, RequestDetailsViewModel>();
        }
    }
}