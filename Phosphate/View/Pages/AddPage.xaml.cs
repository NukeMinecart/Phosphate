using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Phosphate.Cache;
using Phosphate.Files.FileScanner;
using Wpf.Ui.Controls;

namespace Phosphate.View.Pages;

public partial class AddPage : Page
{
    public AddPage()
    {
        InitializeComponent();
        
        if (CacheObjects.ExecutableItemCache.Value != null)
            AddSearchItems();
        
        else
            CacheObjects.ExecutableItemCache.PropertyChanged += (_, _) => Application.Current.Dispatcher.Invoke(AddSearchItems);
    }


    private void AddSearchItems()
    {
        AutoSuggestBox.OriginalItemsSource =
            CacheObjects.ExecutableItemCache.Value!.Select(file => file.FullName).ToList();
        AutoSuggestBox.Focus();
        AutoSuggestBox.IsSuggestionListOpen = true;
    }
}