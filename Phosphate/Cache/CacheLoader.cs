using System.Collections.Immutable;
using System.IO;
using System.Text.Json;
using Microsoft.Win32.SafeHandles;
using Phosphate.Files.FileScanner;
using Phosphate.Files.Json;
using Phosphate.Launcher.Launch;

namespace Phosphate.Cache;

public static class CacheLoader
{
    public static void LoadValuesIntoCache()
    {
        Directory.CreateDirectory(Config.ConfigDirectory.FullName);
        Directory.CreateDirectory(Config.ItemDirectory.FullName);
        try
        {
            if (!JsonLoader.CreateFile(Config.ConfigFile)) 
                CacheObjects.SettingsCache = JsonLoader.LoadValuesFromJson<CacheObjects.Cache>(Config.ConfigFile);
            foreach (var file in FileSearcher.SearchForFiles(Config.ItemDirectory, "json"))
            {
                var item = JsonLoader.LoadValuesFromJson<ExecutableItem>(file);
                CacheObjects.LaunchItemCache.AddValue(item.Name, item);
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }

    public static void SaveValuesFromCache()
    {
        JsonLoader.SaveValuesToJson(Config.ConfigFile, CacheObjects.SettingsCache);
    }
}