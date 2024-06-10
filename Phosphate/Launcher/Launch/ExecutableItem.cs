using System.Drawing;
using System.IO;
using System.Text.Json.Serialization;

namespace Phosphate.Launcher.Launch;

public struct ExecutableItem(FileInfo exePath, string name, Size size, string? iconPath = null)
{
    [JsonInclude] public string Name { get; private set; } = name;

    [JsonInclude] private string _exePath = exePath.FullName;

    [JsonInclude] public Size Size { get; private set; } = size;

    [JsonInclude] public string? IconPath = iconPath;
    
    public void Execute()
    {
        AppLauncher.LaunchExe(new FileInfo(_exePath));
    }

    public Icon GetIcon()
    {
        return GetIcon(IconPath != null ? new FileInfo(IconPath) : new FileInfo(_exePath), Size);
    }

    public static Icon GetIcon(FileInfo filePath, Size size)
    {
        //TODO change to get any kind of image file
        return GetIconUnsafe(filePath) ?? new Icon(SystemIcons.WinLogo, size);
    }
    
    private static Icon? GetIconUnsafe(FileInfo filePath)
    {
        return Icon.ExtractAssociatedIcon(filePath.FullName);
    }
}