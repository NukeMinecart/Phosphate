using System.Collections.Immutable;
using System.IO;
using System.Text.Json;
using Microsoft.Win32.SafeHandles;
using Phosphate.Files.Json;

namespace Phosphate.Cache;

public static class CacheLoader
{
    public static void LoadValuesIntoCache()
    {
        Directory.CreateDirectory(Config.ConfigDirectory.FullName);
        if (!File.Exists(Config.ConfigFile.FullName))
        {
            File.Create(Config.ConfigFile.FullName);
        }
        else
        {
            try
            {
                CacheObjects.SettingsCache = JsonLoader.LoadValuesFromJson<CacheObjects.Cache>(Config.ConfigFile);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    public static void SaveValuesFromCache()
    {
        JsonLoader.SaveValuesToJson(Config.ConfigFile, CacheObjects.SettingsCache);
    }
}