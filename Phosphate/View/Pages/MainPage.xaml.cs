using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Phosphate.Converters;
using Phosphate.Files.Json;
using Phosphate.Launcher;
using Phosphate.Launcher.Launch;
using Image = Wpf.Ui.Controls.Image;
using Size = System.Drawing.Size;

namespace Phosphate.View.Pages;

public partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
        var exeItem =
            new ExecutableItem(
                new FileInfo(@"C:\Users\bradl\OneDrive\Desktop\Stuff\mmc-stable-win32\MultiMC\MultiMC.exe"), "MultiMC",
                new Size(100, 100)); 
        AddLaunch(exeItem);
        
        JsonLoader.SaveValuesToJson(new FileInfo(Path.Combine(Config.ItemDirectory.FullName, exeItem.Name + ".json")), exeItem);

        AddLaunch(JsonLoader.LoadValuesFromJson<ExecutableItem>(new FileInfo(Path.Combine(Config.ItemDirectory.FullName,
            exeItem.Name + ".json"))));
    }
    
    private void AddLaunch(ExecutableItem item)
    {
        var image = new Image
        {
            Stretch = Stretch.Uniform,
            MaxWidth = item.Size.Width,
            MaxHeight = item.Size.Height
        };
        
        image.MouseDown += (_, _) => item.Execute();
        image.Source = ImageConverter.ToImageSource(item.GetIcon());
        
        Grid.SetRow(image, 1);
        Grid.SetColumn(image, 1);
        
        var button = new Button
        {
            Content = item.Name,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        
        button.Click += (_, _) => item.Execute();
        Grid.SetRow(button, 2);
        Grid.SetColumn(button, 1);
        
        var grid = new Grid
        {
            Children = { image, button },
        };
    
        var column1 = new ColumnDefinition
        {
            MinWidth = 8,
            MaxWidth = 8
        };
        
        var column2 = new ColumnDefinition();
        
        var column3 = new ColumnDefinition
        {
            MinWidth = 8,
            MaxWidth = 8
        };
        
        var row0 = new RowDefinition
        {
            MinHeight = 8,
            MaxHeight = 8
        };
        var row1 = new RowDefinition();
        var row2 = new RowDefinition();
        var row3 = new RowDefinition
        {
            MinHeight = 8,
            MaxHeight = 8
        };
        
        grid.ColumnDefinitions.Add(column1);
        grid.ColumnDefinitions.Add(column2);
        grid.ColumnDefinitions.Add(column3);
    
        grid.RowDefinitions.Add(row0);
        grid.RowDefinitions.Add(row1);
        grid.RowDefinitions.Add(row2);
        grid.RowDefinitions.Add(row3);
    
        LaunchPanel.Children.Add(grid);
    }
}