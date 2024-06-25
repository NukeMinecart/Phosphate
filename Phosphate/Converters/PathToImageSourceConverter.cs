using System.Globalization;
using System.IO;
using System.Windows.Data;
using Phosphate.Launcher.Launch;
using Size = System.Drawing.Size;

namespace Phosphate.Converters;

public class PathToImageSourceConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        //todo templated bindings do not directly convert to strings
        var iconPath = (string)value!;
        var path = (string)parameter!;
        
        if (string.IsNullOrWhiteSpace(path) && string.IsNullOrWhiteSpace(iconPath)) return null;

        return ImageConverter.ToImageSource(string.IsNullOrWhiteSpace(iconPath) ? 
            ExecutableItem.GetIcon(new FileInfo(iconPath), new Size(100, 100)) : 
            ExecutableItem.GetIcon(new FileInfo(path), new Size(100, 100)));
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}