using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Phosphate.Cache;
using Phosphate.Converters;
using Phosphate.Files;
using Phosphate.Validation;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using static Phosphate.Cache.SettingKeys;

namespace Phosphate.View.Pages;

public partial class SettingsPage : Page
{
    private static readonly SymbolIcon ValidIcon = new(SymbolRegular.CheckmarkCircle24){Foreground = Brushes.LawnGreen};
    private static readonly SymbolIcon InvalidIcon = new(SymbolRegular.ErrorCircle24){Foreground = Brushes.Red};
    public SettingsPage()
    {
        InitializeComponent();
        
        ThemeSelector.SelectedIndex = CacheObjects.SettingsCache.GetValue(ThemeIndex,
            ThemeToIndexConverter.ConvertThemeToIndex(ApplicationTheme.Dark), CacheObjects.IntegerConverter);
        
        RescanSwitch.IsChecked =
        CacheObjects.SettingsCache.GetValue(RescanOnReload, false, CacheObjects.BooleanConverter);
        
        InitialScanField.Text = CacheObjects.SettingsCache.GetValue(InitialDirectory, "C:/", CacheObjects.StringConverter);
        InitialScanField.Icon = ValidIcon;
    }

    private void ChangeTheme(object source, SelectionChangedEventArgs args)
    {
        var selectedIndex = ((ComboBox)source).SelectedIndex;
        CacheObjects.SettingsCache.AddValue(ThemeIndex, selectedIndex);
        CacheLoader.AddSettingsToSaveQueue();

        ApplicationThemeManager.Apply(ThemeToIndexConverter.ConvertIndexToTheme(selectedIndex));
    }
    
    private void ChangeScanOnReload(object sender, RoutedEventArgs e)
    {
        CacheObjects.SettingsCache.AddValue(RescanOnReload, RescanSwitch.IsChecked!.Value);
        CacheLoader.AddSettingsToSaveQueue();
    }

    private void ValidateFilePath(object sender, TextChangedEventArgs e)
    {
        if (Validators.ValidateDirectory(InitialScanField.GetLineText(0)))
        {
            CacheObjects.SettingsCache.AddValue(InitialDirectory, InitialScanField.Text);
            CacheLoader.AddSettingsToSaveQueue();
            InitialScanField.Icon = ValidIcon;
        }
        else
        {
            InitialScanField.Icon = InvalidIcon;
        }
    }
}