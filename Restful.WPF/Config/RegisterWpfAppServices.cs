using Prism.Ioc;
using Restful.Core.Errors;
using Restful.Core.Services;
using Restful.WPF.Theme;

namespace Restful.WPF.Config
{
    public static class RegisterWpfAppServices
    {
        public static void RegisterWpfServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IThemeService, ThemeService>();
            containerRegistry.RegisterSingleton<IErrorHandler, ErrorHandler>();
            containerRegistry.RegisterSingleton<IFileExportService, FileExportService>();
        }
    }
}
