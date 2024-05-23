using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Phosphate.Converters;
using Phosphate.Launcher;

using Image = Wpf.Ui.Controls.Image;

namespace Phosphate.View.Pages;

public partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
    }
    
    private void AddLaunch(FileInfo file, string name)
    {
        var image = new Image
        {
            Stretch = Stretch.Uniform,
            MaxWidth = 100,
            MaxHeight = 100
        };
        
        var icon = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
    
        image.MouseDown += (_, _) => AppLauncher.LaunchExe(file);
    
        if (icon != null)
            image.Source = ImageConverter.ToImageSource(icon);
        
        Grid.SetRow(image, 0);
        Grid.SetColumn(image, 1);
        
        var button = new Button
        {
            Content = name,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        
        button.Click += (_, _) => AppLauncher.LaunchExe(file);
        Grid.SetRow(button, 1);
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
    
        grid.RowDefinitions.Add(row1);
        grid.RowDefinitions.Add(row2);
        grid.RowDefinitions.Add(row3);
    
        LaunchPanel.Children.Add(grid);
    }
}