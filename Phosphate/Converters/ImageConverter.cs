using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Size = System.Drawing.Size;

namespace Phosphate.Converters;

public static class ImageConverter
{
    public static ImageSource ToImageSource(Icon? icon)
    {
        if (icon != null)
        {
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            
            return imageSource;
        }

        return new BitmapImage();
    }
}