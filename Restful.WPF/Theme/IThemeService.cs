﻿namespace Restful.WPF.Theme
{
    public interface IThemeService
    {
        void ChangeApplicationTheme();
        void ChangeApplicationAccent(string accent);
        void SaveCurrentThemeSetting(string themeName);
        string LoadCurrentThemeSettings();
    }
}