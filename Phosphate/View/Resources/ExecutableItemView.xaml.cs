using System.Windows;
using System.Windows.Controls;

namespace Phosphate.View.Resources;

public class ExecutableItemView : Control
{
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
    
    public static readonly DependencyProperty ControlWidthProperty = DependencyProperty.Register(
        nameof(ControlWidth),
        typeof(int),
        typeof(ExecutableItemView),
        new PropertyMetadata(null)
    );
    
    public static readonly DependencyProperty ControlHeightProperty = DependencyProperty.Register(
        nameof(ControlHeight),
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
    
    public int ControlWidth
    {
        get => (int)GetValue(ControlWidthProperty);
        set => SetValue(ControlWidthProperty, value);
    }
    public int ControlHeight
    {
        get => (int)GetValue(ControlHeightProperty);
        set => SetValue(ControlHeightProperty, value);
    }
    
    public string? IconPath
    {
        get => (string?)GetValue(IconPathProperty);
        set => SetValue(IconPathProperty, value);
    }
}