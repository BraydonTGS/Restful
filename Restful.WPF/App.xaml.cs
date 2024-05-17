using ControlzEx.Theming;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Restful.Core.Constant;
using Restful.Core.Errors;
using Restful.RequestsModule;
using Restful.SettingsModule;
using Restful.UserModule;
using Restful.WPF.Config;
using Restful.WPF.Theme;
using Restful.WPF.Views;
using System;
using System.Windows;

namespace Restful.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            // Handle non-UI thread exceptions
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            LoadApplicationTheme();
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterWpfAppServices.RegisterWpfServices(containerRegistry);
        }

        protected override void OnInitialized()
        {
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate(Regions.MainContentRegion, nameof(WelcomeView));
            base.OnInitialized();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleRequestsModule>();
            moduleCatalog.AddModule<ModuleUserModule>();
            moduleCatalog.AddModule<ModuleSettingsModule>();
        }

        private void LoadApplicationTheme()
        {
            var themeService = Container.Resolve<IThemeService>();
            var savedTheme = themeService.LoadCurrentThemeSettings();
            ThemeManager.Current.ChangeTheme(this, savedTheme);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowExceptionDetails(e.ExceptionObject as Exception);
        }

        private void ShowExceptionDetails(Exception ex)
        {
            var errorHandler = Container.Resolve<IErrorHandler>();
            errorHandler.DisplayExceptionMessage(ex);
        }
    }
}
