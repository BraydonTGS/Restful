﻿using ControlzEx.Theming;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Restful.RequestsModule;
using Restful.WPF.Theme;
using Restful.WPF.Views;
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
            LoadApplicationTheme();
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IThemeService, ThemeService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleRequestsModule>();
        }

        private void LoadApplicationTheme()
        {
            var themeService = Container.Resolve<IThemeService>();
            var savedTheme = themeService.LoadCurrentThemeSettings();
            ThemeManager.Current.ChangeTheme(this, savedTheme);
        }
    }
}
