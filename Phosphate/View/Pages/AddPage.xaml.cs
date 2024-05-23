using System.IO;
using System.Windows.Controls;
using Phosphate.Files.FileScanner;
using Wpf.Ui.Controls;

namespace Phosphate.View.Pages;

public partial class AddPage : Page
{
    public AddPage()
    {
        InitializeComponent();
        //TODO implement in a new thread
        AutoSuggestBox.OriginalItemsSource = new List<string>(FileSearcher.SearchForExe(new FileInfo("C:/")).Select(file => file.FullName));
    }

    private void UpdateSelections(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        
    }
}