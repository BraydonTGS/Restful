using ControlzEx.Theming;
using Prism.Commands;
using Prism.Regions;
using Restful.Core.Constant;
using Restful.Core.Constants;
using Restful.Core.ViewModels;
using Restful.WPF.Theme;
using Restful.WPF.Views;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Restful.WPF.ViewModels
{
    public partial class MainWindowViewModel : RegionViewModelBase
    {
        private readonly IThemeService _themeService;
        public DelegateCommand<string> MenuItemClicked { get; set; }
        public DelegateCommand ThemeButtonClicked { get; set; }

        public MainWindowViewModel(IRegionManager regionManager, IThemeService themeService) : base(regionManager)
        {
            Title = Constants.ApplicationTitle;
            MenuItemClicked = new DelegateCommand<string>(OnMenuItemClickedExecuted);
            ThemeButtonClicked = new DelegateCommand(OnThemeButtonClickedExecuted);
           _themeService = themeService;
        }

        private void OnMenuItemClickedExecuted(string view)
        {
            if (!String.IsNullOrEmpty(view))
                _regionManager.RequestNavigate(Regions.MainContentRegion, view);
        }

        private void OnThemeButtonClickedExecuted()
        {
            _themeService.ChangeApplicationTheme();
        }

    }
}
