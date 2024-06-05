using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Phosphate.Cache;
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
    
    private void OnTextChange(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        //TODO add preview frame to view before adding element
        if (File.Exists(args.Text) && AppLauncher.IsExecutableFile(new FileInfo(args.Text)))
            AddFieldGrid.Visibility = Visibility.Visible;
        else
            AddFieldGrid.Visibility = Visibility.Hidden;

    }

    private void OpenIconFile(object sender, RoutedEventArgs e)
    {
        var iconDialog = new OpenFileDialog
        {
            Multiselect = false,
            Filter = "Image Files (*.ico, *.png, *.jpg, *.jpeg)|*.ico;*.png;*.jpg;*.jpeg"
        };
        if (iconDialog.ShowDialog() == true)
        {
            IconFileBox.Text = iconDialog.FileName;
        }
    }
}