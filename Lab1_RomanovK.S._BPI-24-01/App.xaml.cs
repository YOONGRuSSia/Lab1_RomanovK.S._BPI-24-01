using System;
using System.Windows;

namespace Lab1_RomanovK.S._BPI_24_01
{
    public enum AppTheme { Light, Dark }

    public partial class App : Application
    {
        /// <summary>
        /// Текущая тема приложения. По умолчанию — светлая.
        /// </summary>
        public static AppTheme CurrentTheme { get; private set; } = AppTheme.Light;

        public static void ApplyTheme(AppTheme theme)
        {
            CurrentTheme = theme;

            string themeFile = theme == AppTheme.Dark
                ? "Themes/DarkTheme.xaml"
                : "Themes/LightTheme.xaml";

            var newTheme = new ResourceDictionary
            {
                Source = new Uri(themeFile, UriKind.Relative)
            };

            var dicts = Current.Resources.MergedDictionaries;

            // Удаляем старую тему (все словари с "Theme" в пути)
            for (int i = dicts.Count - 1; i >= 0; i--)
            {
                if (dicts[i].Source != null &&
                    dicts[i].Source.OriginalString.Contains("Theme"))
                {
                    dicts.RemoveAt(i);
                }
            }

            // Вставляем новую тему ПЕРВОЙ (до Styles.xaml)
            dicts.Insert(0, newTheme);
        }
    }
}