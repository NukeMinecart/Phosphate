using System.IO;

namespace Phosphate;

public static class Config
{
    public static readonly FileInfo ConfigDirectory = new(Path.Combine(Directory.GetCurrentDirectory(), "config"));
    public static readonly FileInfo ConfigFile = new(Path.Combine(ConfigDirectory.FullName, "config.json"));
}