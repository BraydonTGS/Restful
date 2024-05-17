using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Restful.SettingsModule.ViewModels;
using Restful.SettingsModule.Views;

namespace Restful.SettingsModule
{
    public class ModuleSettingsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<RegionManager>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Navigation //
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        }
    }
}