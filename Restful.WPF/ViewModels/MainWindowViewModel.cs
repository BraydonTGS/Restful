using ControlzEx.Theming;
using Prism.Commands;
using Prism.Regions;
using Restful.Core.Constant;
using Restful.Core.Constants;
using Restful.Core.ViewModels;
using Restful.WPF.Views;
using System;
using System.Windows;

namespace Restful.WPF.ViewModels
{
    public partial class MainWindowViewModel : RegionViewModelBase
    {
        public DelegateCommand<string> MenuItemClicked { get; set; }
        public DelegateCommand<object> ThemeButtonClicked { get; set; }

        public MainWindowViewModel(IRegionManager regionManager) : base(regionManager)
        {
            Title = Constants.ApplicationTitle;
            MenuItemClicked = new DelegateCommand<string>(OnMenuItemClickedExecuted);
            ThemeButtonClicked = new DelegateCommand<object>(OnThemeButtonClickedExecuted);
        }

        private void OnMenuItemClickedExecuted(string view)
        {

            if (!String.IsNullOrEmpty(view))
                _regionManager.RequestNavigate(Regions.MainContentRegion, view);
        }

        private void OnThemeButtonClickedExecuted(object obj)
        {

            // Detect current theme and change accordingly
            var currentTheme = ThemeManager.Current.DetectTheme(Application.Current);
            if (currentTheme != null)
            {
                var newThemeName = currentTheme.BaseColorScheme == "Light" ? "Dark.Red" : "Light.Red";
                ThemeManager.Current.ChangeTheme(Application.Current, newThemeName);
            }

        }

    }
}
