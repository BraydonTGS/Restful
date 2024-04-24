using Prism.Commands;
using Prism.Regions;
using Restful.Core.Constant;
using Restful.Core.ViewModels;
using Restful.WPF.Theme;
using System;

namespace Restful.WPF.ViewModels
{
    public partial class MainWindowViewModel : RegionViewModelBase
    {
        private readonly IThemeService _themeService;
        public DelegateCommand<string> MenuItemClicked { get; set; }
        public DelegateCommand ThemeButtonClicked { get; set; }
        public DelegateCommand<string> AccentButtonClicked { get; set; }

        public MainWindowViewModel(IRegionManager regionManager, IThemeService themeService) : base(regionManager)
        {
            Title = Constants.ApplicationTitle;

            _themeService = themeService;

            MenuItemClicked = new DelegateCommand<string>(OnMenuItemClickedExecuted);
            ThemeButtonClicked = new DelegateCommand(OnThemeButtonClickedExecuted);
            AccentButtonClicked = new DelegateCommand<string>(OnAccentButtonClickedExecuted);

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

        private void OnAccentButtonClickedExecuted(string str)
        {
            _themeService.ChangeApplicationAccent(str);
        }

    }
}
