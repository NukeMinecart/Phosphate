using System.Windows;
using System.Windows.Controls;
using Phosphate.Cache;

namespace Phosphate.View.Pages;

public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();

        ThemeSwitch.IsChecked = CacheObjects.SettingsCache.GetValue(CacheKeys.DarkTheme, true, CacheObjects.BooleanConverter);
        ContrastSwitch.IsChecked = CacheObjects.SettingsCache.GetValue(CacheKeys.HighContrast, false, CacheObjects.BooleanConverter);
        UpdateSettings.Update();

    }

    private void ChangeHighContrast(object sender, RoutedEventArgs e)
    {
        CacheObjects.SettingsCache.AddValue(CacheKeys.HighContrast, ContrastSwitch.IsChecked!.Value);
        UpdateSettings.Update();

    }

    private void ChangeTheme(object sender, RoutedEventArgs e)
    {
        CacheObjects.SettingsCache.AddValue(CacheKeys.DarkTheme, ThemeSwitch.IsChecked!.Value);
        UpdateSettings.Update();
    }
}