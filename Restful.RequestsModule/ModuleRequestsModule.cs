using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Restful.Core.Constant;
using Restful.RequestsModule.ViewModels;
using Restful.RequestsModule.Views;

namespace Restful.RequestsModule
{
    public class ModuleRequestsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<RegionManager>();
            regionManager.RegisterViewWithRegion(Regions.RequestsTreeRegion, typeof(RequestsTreeView));
            regionManager.RegisterViewWithRegion(Regions.RequestDetailsRegion, typeof(RequestDetailsView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<RequestsView, RequestsViewModel>();
            containerRegistry.RegisterForNavigation<RequestsTreeView, RequestsTreeViewModel>();
            containerRegistry.RegisterForNavigation<RequestDetailsView, RequestDetailsViewModel>();
        }
    }
}