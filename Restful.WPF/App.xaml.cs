using ControlzEx.Theming;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Restful.RequestsModule;
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
            ThemeManager.Current.ChangeTheme(this, "Dark.Red");
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleRequestsModule>();
        }
    }
}
