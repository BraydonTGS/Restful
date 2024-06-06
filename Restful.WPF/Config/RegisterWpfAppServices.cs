using Prism.Ioc;
using Restful.Core.Errors;
using Restful.WPF.Theme;
using Restful.WPF.ViewModels;
using Restful.WPF.Views;

namespace Restful.WPF.Config
{
    /// <summary>
    /// Register WPF Application Services
    /// </summary>
    public static class RegisterWpfAppServices
    {
        public static void RegisterWpfServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ComingSoonView>();
            containerRegistry.RegisterForNavigation<WelcomeView, WelcomeViewModel>();

            containerRegistry.RegisterSingleton<IThemeService, ThemeService>();
        }
    }
}
