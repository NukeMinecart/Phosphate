using System.Windows.Controls;
using Phosphate.Cache;
using Phosphate.Converters;
using Wpf.Ui.Appearance;
using static Phosphate.Cache.SettingKeys;

namespace Phosphate.View.Pages;

public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();
        DataContext = this;
        
        // ScanSwitch.IsChecked =
        //     CacheObjects.SettingsCache.GetValue(SettingKeys.RescanOnReload, false, CacheObjects.BooleanConverter);
        // InitialScanField.Text = CacheObjects.SettingsCache.GetValue(SettingKeys.InitialDirectory, "C:/", CacheObjects.StringConverter);
        // InitialScanField.BorderBrush = null;

        ThemeSelector.SelectedIndex = CacheObjects.SettingsCache.GetValue(ThemeIndex,
            ThemeToIndexConverter.ConvertThemeToIndex(ApplicationTheme.Dark), CacheObjects.IntegerConverter);
    }

    private void ChangeTheme(object source, SelectionChangedEventArgs args)
    {
        //TODO do a queue implementation of file saving and searching and dont exit the program until queue is empty
        var selectedIndex = ((ComboBox)source).SelectedIndex;
        CacheObjects.SettingsCache.AddValue(ThemeIndex, selectedIndex);

        ApplicationThemeManager.Apply(ThemeToIndexConverter.ConvertIndexToTheme(selectedIndex));
    }
    
    // private void ChangeScanOnReload(object sender, RoutedEventArgs e)
    // {
    //     CacheObjects.SettingsCache.AddValue(SettingKeys.RescanOnReload, ScanSwitch.IsChecked!.Value);
    //     SaveSettingValues();
    // }
    //
    // private void ValidateFilePath(object sender, TextChangedEventArgs e)
    // {
    //     if (!Directory.Exists(InitialScanField.GetLineText(0)))
    //         InitialScanField.BorderBrush = new LinearGradientBrush(Colors.DarkRed, Colors.Crimson, 45);
    //     else
    //     {
    //         InitialScanField.BorderBrush = null;
    //         CacheObjects.SettingsCache.AddValue(SettingKeys.InitialDirectory, InitialScanField.Text);
    //         SaveSettingValues();
    //     }
    // }
}