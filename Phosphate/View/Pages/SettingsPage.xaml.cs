using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Phosphate.Cache;
using Phosphate.Converters;
using Phosphate.Validation;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using static Phosphate.Cache.SettingKeys;

namespace Phosphate.View.Pages;

public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();
        
        ThemeSelector.SelectedIndex = CacheObjects.SettingsCache.GetValue(ThemeIndex,
            ThemeToIndexConverter.ConvertThemeToIndex(ApplicationTheme.Dark), CacheObjects.IntegerConverter);
        
        RescanSwitch.IsChecked =
        CacheObjects.SettingsCache.GetValue(RescanOnReload, false, CacheObjects.BooleanConverter);
        
        InitialScanField.Text = CacheObjects.SettingsCache.GetValue(InitialDirectory, "C:/", CacheObjects.StringConverter);
        InitialScanField.Icon = new SymbolIcon(SymbolRegular.CheckmarkCircle24);
    }

    private void ChangeTheme(object source, SelectionChangedEventArgs args)
    {
        //TODO do a queue implementation of file saving and searching and dont exit the program until queue is empty
        var selectedIndex = ((ComboBox)source).SelectedIndex;
        CacheObjects.SettingsCache.AddValue(ThemeIndex, selectedIndex);

        ApplicationThemeManager.Apply(ThemeToIndexConverter.ConvertIndexToTheme(selectedIndex));
    }
    
    private void ChangeScanOnReload(object sender, RoutedEventArgs e)
    {
        CacheObjects.SettingsCache.AddValue(RescanOnReload, RescanSwitch.IsChecked!.Value);
    }
    
    private void ValidateFilePath(object sender, TextChangedEventArgs e)
    {
        if (Validators.ValidateDirectory(InitialScanField.GetLineText(0)))
        {
            CacheObjects.SettingsCache.AddValue(InitialDirectory, InitialScanField.Text);
            InitialScanField.Icon = new SymbolIcon(SymbolRegular.CheckmarkCircle24);
        }
        else
        {
            InitialScanField.Icon = new SymbolIcon(SymbolRegular.ErrorCircle24);
        }
    }
}