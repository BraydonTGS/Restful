using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Restful.UserModule.ViewModels;
using Restful.UserModule.Views;

namespace Restful.UserModule
{
    public class ModuleUserModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<RegionManager>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Navigation //
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
        }
    }
}