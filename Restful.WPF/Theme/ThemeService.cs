using ControlzEx.Theming;
using Restful.WPF.Properties;
using System.Windows;

namespace Restful.WPF.Theme
{
    public class ThemeService : IThemeService
    {
        public void ChangeApplicationTheme()
        {
            var currentTheme = ThemeManager.Current.DetectTheme(Application.Current);
            var accent = currentTheme.ColorScheme;
            if (currentTheme is not null && accent is not null)
            {
                var newThemeName = currentTheme.BaseColorScheme
                    == ThemeBase.LightBase ? $"{ThemeBase.DarkBase}.{accent}" : $"{ThemeBase.LightBase}.{accent}";

                ThemeManager.Current.ChangeTheme(Application.Current, newThemeName);

                SaveCurrentThemeSetting(newThemeName);
            }
        }

        public void ChangeApplicationAccent(string accent)
        {
            var currentTheme = ThemeManager.Current.DetectTheme(Application.Current);

            if (currentTheme is not null && accent is not null)
            {
                var newThemeName = $"{currentTheme.BaseColorScheme}.{accent}";
                ThemeManager.Current.ChangeTheme(Application.Current, newThemeName);

                SaveCurrentThemeSetting(newThemeName);
            }
        }

        public void SaveCurrentThemeSetting(string themeName)
        {
            Settings.Default.Theme = themeName;
            Settings.Default.Save();
        }

        public string LoadCurrentThemeSettings()
        {
            var userTheme = Settings.Default.Theme;
            if (string.IsNullOrEmpty(userTheme))
            {
                userTheme = $"{ThemeBase.DarkBase}.{ThemeAccents.DefaultAccent}";
                Settings.Default.Theme = userTheme;
                Settings.Default.Save();
            }
            return userTheme;
        }
    }
}
