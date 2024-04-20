using ControlzEx.Theming;
using System.Windows;

namespace Restful.WPF.Theme
{
    public class ThemeService : IThemeService
    {
        public void ChangeApplicationTheme()
        {
            // Detect current theme and change accordingly
            var currentTheme = ThemeManager.Current.DetectTheme(Application.Current);
            var accent = currentTheme.ColorScheme;
            if (currentTheme is not null && accent is not null)
            {
                var newThemeName = currentTheme.BaseColorScheme == "Light" ? $"Dark.{accent}" : $"Light.{accent}";
                ThemeManager.Current.ChangeTheme(Application.Current, newThemeName);
            }
        }

        public void ChangeApplicationAccent(string accent)
        {

        }
    }
}
