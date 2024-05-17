using System.Windows;
using System.Windows.Controls;
using Phosphate.Cache;
using Wpf.Ui.Appearance;

namespace Phosphate.View.Pages;

public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();

        ThemeSwitch.IsChecked = CacheObjects.SettingsCache.GetValue(CacheKeys.DarkTheme, true);
        ContrastSwitch.IsChecked = CacheObjects.SettingsCache.GetValue(CacheKeys.HighContrast, false);
        CacheLoader.SaveValuesToCache();
    }

    private void ChangeHighContrast(object sender, RoutedEventArgs e)
    {
        ApplicationThemeManager.Apply(ContrastSwitch.IsChecked!.Value
            ? ApplicationTheme.HighContrast
            : ThemeSwitch.IsChecked!.Value 
                ? ApplicationTheme.Dark
                : ApplicationTheme.Light);
        CacheObjects.SettingsCache.AddValue(CacheKeys.HighContrast, ContrastSwitch.IsChecked!.Value);
    }

    private void ChangeTheme(object sender, RoutedEventArgs e)
    {
        ApplicationThemeManager.Apply(ThemeSwitch.IsChecked!.Value 
            ? ApplicationTheme.Dark
            : ApplicationTheme.Light);
        
        CacheObjects.SettingsCache.AddValue(CacheKeys.DarkTheme, ThemeSwitch.IsChecked!.Value);
    }
}