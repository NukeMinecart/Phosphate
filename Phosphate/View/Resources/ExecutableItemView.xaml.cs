using System.Windows;
using System.Windows.Controls;

namespace Phosphate.View.Resources;

public class ExecutableItemView : Control
{
    public ExecutableItemView()
    {
        DataContext = this;
    }

    public static readonly DependencyProperty DisplayNameProperty = DependencyProperty.Register(
        nameof(DisplayName),
        typeof(string),
        typeof(ExecutableItemView),
        new PropertyMetadata(null)
    ); 
    
    public static readonly DependencyProperty ExePathProperty = DependencyProperty.Register(
        nameof(ExePath),
        typeof(string),
        typeof(ExecutableItemView),
        new PropertyMetadata(null)
    ); 
    
    public static readonly DependencyProperty IconWidthProperty = DependencyProperty.Register(
        nameof(IconWidth),
        typeof(int),
        typeof(ExecutableItemView),
        new PropertyMetadata(null)
    );
    
    public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register(
        nameof(IconHeight),
        typeof(int),
        typeof(ExecutableItemView),
        new PropertyMetadata(null)
    );
    
    public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register(
        nameof(IconPath),
        typeof(string),
        typeof(ExecutableItemView),
        new PropertyMetadata(null)
    ); 

    public string DisplayName
    {
        get => (string)GetValue(DisplayNameProperty);
        set => SetValue(DisplayNameProperty, value);
    }
    
    public string ExePath
    {
        get => (string)GetValue(ExePathProperty);
        set => SetValue(ExePathProperty, value);
    }
    
    public int IconWidth
    {
        get => (int)GetValue(IconWidthProperty);
        set => SetValue(IconWidthProperty, value);
    }
    public int IconHeight
    {
        get => (int)GetValue(IconHeightProperty);
        set => SetValue(IconHeightProperty, value);
    }
    
    public string? IconPath
    {
        get => (string?)GetValue(IconPathProperty);
        set => SetValue(IconPathProperty, value);
    }
}