using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Phosphate.Cache;
using TextBox = Wpf.Ui.Controls.TextBox;

namespace Phosphate.View.Pages;

public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();

        ThemeSwitch.IsChecked = CacheObjects.SettingsCache.GetValue(SettingKeys.DarkTheme, true, CacheObjects.BooleanConverter);
        ContrastSwitch.IsChecked = CacheObjects.SettingsCache.GetValue(SettingKeys.HighContrast, false, CacheObjects.BooleanConverter);
        ScanSwitch.IsChecked =
            CacheObjects.SettingsCache.GetValue(SettingKeys.RescanOnReload, false, CacheObjects.BooleanConverter);
        InitialScanField.Text = CacheObjects.SettingsCache.GetValue(SettingKeys.InitialDirectory, "C:/", CacheObjects.StringConverter);
        InitialScanField.BorderBrush = null;
    }

    private void ChangeHighContrast(object sender, RoutedEventArgs e)
    {
        CacheObjects.SettingsCache.AddValue(SettingKeys.HighContrast, ContrastSwitch.IsChecked!.Value);
        UpdateSettings.Update();
    }

    private void ChangeTheme(object sender, RoutedEventArgs e)
    {
        CacheObjects.SettingsCache.AddValue(SettingKeys.DarkTheme, ThemeSwitch.IsChecked!.Value);
        UpdateSettings.Update();
    }

    private void ChangeScanOnReload(object sender, RoutedEventArgs e)
    {
        CacheObjects.SettingsCache.AddValue(SettingKeys.RescanOnReload, ScanSwitch.IsChecked!.Value);
        CacheLoader.SaveSettingValuesFromCache();
    }

    private void ValidateFilePath(object sender, TextChangedEventArgs e)
    {
        if (!Directory.Exists(InitialScanField.GetLineText(0)))
            InitialScanField.BorderBrush = new LinearGradientBrush(Colors.DarkRed, Colors.Crimson, 45);
        else
        {
            InitialScanField.BorderBrush = null;
            CacheObjects.SettingsCache.AddValue(SettingKeys.InitialDirectory, InitialScanField.Text);
            CacheLoader.SaveSettingValuesFromCache();
        }
    }
}