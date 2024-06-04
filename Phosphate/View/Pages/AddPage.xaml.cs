using System.IO;
using System.Windows;
using System.Windows.Controls;
using Phosphate.Cache;
using Phosphate.Files.FileScanner;
using Phosphate.Launcher;
using Phosphate.Launcher.Launch;
using Wpf.Ui.Controls;
using Size = System.Drawing.Size;

namespace Phosphate.View.Pages;

public partial class AddPage : Page
{
    public AddPage()
    {
        InitializeComponent();

        if (CacheObjects.ExecutableItemCache.Value != null)
            AddSearchItems();

        else
            CacheObjects.ExecutableItemCache.PropertyChanged +=
                (_, _) => Application.Current.Dispatcher.Invoke(AddSearchItems);
    }


    private void AddSearchItems()
    {
        ExecutableSuggestBox.OriginalItemsSource =
            CacheObjects.ExecutableItemCache.Value!.Select(file => file.FullName).ToList();
    }

    private void AddExecutableItem(ExecutableItem item)
    {
        
    }

    private void OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        AddExecutableItem(new ExecutableItem(new FileInfo(args.SelectedItem.ToString()!), "Name", new Size(100, 100)));
    }

    private void OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        if (File.Exists(args.QueryText) && AppLauncher.IsExecutableFile(new FileInfo(args.QueryText)))
        {
            AddExecutableItem(new ExecutableItem(new FileInfo(args.QueryText), "Name", new Size(100, 100)));
        }
    }
}