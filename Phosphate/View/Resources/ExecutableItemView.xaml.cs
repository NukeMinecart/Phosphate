using System.Windows;
using System.Windows.Controls;
using Phosphate.Launcher.Launch;

namespace Phosphate.View.Resources;

public class ExecutableItemView : Control
{
    public static readonly DependencyProperty DisplayExecutableItemProperty = DependencyProperty.Register(
        nameof(DisplayExecutableItem),
        typeof(ExecutableItem),
        typeof(ExecutableItemView),
        new PropertyMetadata(null)
    ); 

    public ExecutableItem DisplayExecutableItem
    {
        get => (ExecutableItem)GetValue(DisplayExecutableItemProperty);
        set => SetValue(DisplayExecutableItemProperty, value);
    }
}