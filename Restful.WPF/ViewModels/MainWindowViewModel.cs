using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Restful.Core.Errors;
using Restful.Core.Events;
using Restful.Core.Users;
using Restful.Core.ViewModels;
using Restful.Global.Constant;
using Restful.WPF.Theme;
using System;

namespace Restful.WPF.ViewModels
{
    public partial class MainWindowViewModel : RegionViewModelBase
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IThemeService _themeService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IErrorHandler _errorHandler;

        [ObservableProperty]
        private string _username;
        public DelegateCommand ThemeButtonClicked { get; set; }
        public DelegateCommand<string> MenuItemClicked { get; set; }
        public DelegateCommand<string> AccentButtonClicked { get; set; }

        #region Constructor
        public MainWindowViewModel(
            IRegionManager regionManager,
            IApplicationUserService applicationUserService,
            IThemeService themeService,
            IEventAggregator eventAggregator,
            IErrorHandler errorHandler) : base(regionManager)
        {
            Title = Constants.ApplicationTitle;

            _applicationUserService = applicationUserService;
            _themeService = themeService;
            _eventAggregator = eventAggregator;
            _errorHandler = errorHandler;

            ConfigureDelegateCommands();

            _eventAggregator
                .GetEvent<SetUsernameEvent>()
                .Subscribe(OnSetUsernameEventPublished);
        }
        #endregion

        #region ConfigureDelegateCommands
        /// <summary>
        /// Configure the Delegate Commands
        /// </summary>
        private void ConfigureDelegateCommands()
        {
            MenuItemClicked = new DelegateCommand<string>(OnMenuItemClickedExecuted);
            ThemeButtonClicked = new DelegateCommand(OnThemeButtonClickedExecuted);
            AccentButtonClicked = new DelegateCommand<string>(OnAccentButtonClickedExecuted);
        }
        #endregion

        #region OnMenuItemClickedExecuted
        /// <summary>
        /// Command that is Fired when the User Clicks on a Menu Item in the Context Menu
        /// </summary>
        /// <param name="view"></param>
        private void OnMenuItemClickedExecuted(string view)
        {
            try
            {
                if (!String.IsNullOrEmpty(view))
                    _regionManager.RequestNavigate(Regions.MainContentRegion, view);
            }
            catch (Exception ex) { _errorHandler.DisplayExceptionMessage(ex); }
        }
        #endregion

        #region OnThemeButtonClickedExecuted
        /// <summary>
        /// Change the Theme
        /// </summary>
        private void OnThemeButtonClickedExecuted()
        {
            try
            {
                _themeService.ChangeApplicationTheme();
            }
            catch (Exception ex) { _errorHandler.DisplayExceptionMessage(ex); }
        }
        #endregion

        #region OnAccentButtonClickedExecuted
        /// <summary>
        /// Change the Accent
        /// </summary>
        /// <param name="str"></param>
        private void OnAccentButtonClickedExecuted(string str)
        {
            try
            {
                _themeService.ChangeApplicationAccent(str);
            }
            catch (Exception ex) { _errorHandler.DisplayExceptionMessage(ex); }
        }
        #endregion

        #region OnSetUsernameEventPublished
        /// <summary>
        /// Event that is Triggered when the User Login in Successful
        /// 
        /// We set the Application Title + Username
        /// </summary>
        private void OnSetUsernameEventPublished()
        {
            Username = _applicationUserService.GetApplicationUsername();
            Title = $"{Constants.ApplicationTitle} - {Username}";
        }
        #endregion
    }
}
