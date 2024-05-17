using System.IO;

namespace Phosphate;

public static class Config
{
    public static readonly FileInfo ConfigDirectory = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Phosphate"));
    public static readonly FileInfo ConfigFile = new(Path.Combine(ConfigDirectory.FullName, "config.json"));
}