using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Phosphate.Event;

public sealed class Property<T>(T? initialValue) : INotifyPropertyChanged
{
    public Property() : this(default)
    {
    }

    public T? Value
    {
        get => initialValue;
        set
        {
            initialValue = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}