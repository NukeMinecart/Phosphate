using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Phosphate.Converters;

public static class ImageConverter
{
    public static ImageSource ToImageSource(Icon? icon)
    {
        if (icon != null)
        {
            //Todo create one for every file type (.png, .jpg, etc)
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }

        return new BitmapImage();
    }
}