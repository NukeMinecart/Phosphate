using System.Drawing;
using System.IO;

namespace Phosphate.Launcher.Launch;

public struct ExecutableItem(FileInfo exePath, string name, Size size)
{
    public string Name { get; private set; } = name;

    public Icon Icon
    {
        get;
        private set;
    } = Icon.ExtractAssociatedIcon(exePath.FullName) ?? new Icon(SystemIcons.WinLogo, size);

    public void Execute()
    {
        AppLauncher.LaunchExe(exePath);
    }
}