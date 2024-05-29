using System.Windows;
using System.Windows.Controls;

namespace Phosphate.View.Resources;

public class Setting : Control
{
    public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(
        nameof(HeaderText),
        typeof(string),
        typeof(Setting),
        new PropertyMetadata(null)
    );

    public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
        nameof(Content),
        typeof(object),
        typeof(Setting),
        new PropertyMetadata(null)
    );

    public string? HeaderText
    {
        get => (string?)GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }
}