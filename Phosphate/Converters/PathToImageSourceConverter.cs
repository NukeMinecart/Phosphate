using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using Phosphate.Launcher.Launch;

namespace Phosphate.Converters;

public class PathToImageSourceConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
        {
            var iconPath = (string)value!;
            return ImageConverter.ToImageSource(ExecutableItem.GetIcon(new FileInfo(iconPath), new Size(100, 100)));
        }

        var path = (string)parameter!;
        return ImageConverter.ToImageSource(ExecutableItem.GetIcon(new FileInfo(path), new Size(100, 100)));
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}