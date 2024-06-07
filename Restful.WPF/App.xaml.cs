using ControlzEx.Theming;
using Prism.DryIoc;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Restful.Core.Config;
using Restful.Core.Errors;
using Restful.Core.Events;
using Restful.Global.Constant;
using Restful.RequestsModule;
using Restful.SettingsModule;
using Restful.UserModule;
using Restful.UserModule.Views;
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
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        private IErrorHandler _errorHandler;

        private bool _userVerified = false;
        private LoginWindow _loginWindow;

        #region CreateShell
        /// <summary>
        /// Creates the Application Shell
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            // Handle non-UI thread exceptions
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            LoadAppResources();
            LoadApplicationTheme();

            return Container.Resolve<MainWindow>();
        }
        #endregion

        #region LoadAppResources
        /// <summary>
        /// Load any Dependencies that the App Class Needs
        /// </summary>
        private void LoadAppResources()
        {
            _regionManager = Container.Resolve<IRegionManager>();
            _eventAggregator = Container.Resolve<IEventAggregator>();
            _errorHandler = Container.Resolve<IErrorHandler>();
        }
        #endregion

        #region OnInitialized
        /// <summary>
        /// After the Application Shell is Created we Need to Initialize the MainWindow
        /// 
        /// Subscribe to the LoginSuccessEvent
        /// 
        /// Load the Login Window
        /// 
        /// If the User Logs in Successfully - The Subscribed Event will be Published 
        /// 
        /// Initialize the MainWindow - Or Shutdown
        /// </summary>
        protected override void OnInitialized()
        {
            _loginWindow = Container.Resolve<LoginWindow>();

            _eventAggregator
                .GetEvent<LoginSuccessEvent>()
                .Subscribe(OnLoginSuccessEventPublished);

            _loginWindow.ShowDialog();

            if (_userVerified)
            {
                _regionManager.RequestNavigate(Regions.MainContentRegion, nameof(WelcomeView));
                base.OnInitialized();
            }
            else
                Current.Shutdown();
        }
        #endregion

        #region RegisterTypes
        /// <summary>
        /// Register any Dependencies Required for the Restful.WPF Project
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterWpfAppServices.RegisterWpfServices(containerRegistry);
            RegisterCoreAppServices.RegisterCoreServices(containerRegistry);
        }
        #endregion

        #region ConfigureModuleCatalog
        /// <summary>
        /// Configure the Module Catalog for the Application Shell
        /// </summary>
        /// <param name="moduleCatalog"></param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleRequestsModule>();
            moduleCatalog.AddModule<ModuleUserModule>();
            moduleCatalog.AddModule<ModuleSettingsModule>();
        }
        #endregion

        #region OnLoginSuccessEventPublished
        /// <summary>
        /// Event that is Published when the User Login is Successful
        /// </summary>
        private void OnLoginSuccessEventPublished(bool success)
        {
            if (success)
            {
                _userVerified = true;
                _loginWindow.Close();
            }
        }
        #endregion

        #region LoadApplicationTheme
        /// <summary>
        /// Load the User's Application Theme
        /// </summary>
        private void LoadApplicationTheme()
        {
            var themeService = Container.Resolve<IThemeService>();
            var savedTheme = themeService.LoadCurrentThemeSettings();
            ThemeManager.Current.ChangeTheme(this, savedTheme);
        }
        #endregion

        #region CurrentDomain_UnhandledException
        /// <summary>
        /// Event that is Triggered when there is an Unhandled Exception in the Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _errorHandler.DisplayExceptionMessage(e.ExceptionObject as Exception, "Unhandled Exception Triggered!");
        }
        #endregion
    }
}
