using System.IO;

namespace Phosphate;

public static class Config
{
    public static readonly FileInfo ConfigDirectory = new(Path.Combine(Directory.GetCurrentDirectory(), "config"));
    public static readonly FileInfo ConfigFile = new(Path.Combine(ConfigDirectory.FullName, "config.json"));

    public static readonly FileInfo CacheDirectory = new(Path.Combine(Directory.GetCurrentDirectory(), "cache"));
    public static readonly FileInfo ExecutableCacheFile = new(Path.Combine(CacheDirectory.FullName, "fileLoc.json"));


    public static readonly FileInfo ItemDirectory = new(Path.Combine(Directory.GetCurrentDirectory(), "items"));
}