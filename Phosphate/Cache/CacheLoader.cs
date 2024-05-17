using System.Text.Json;

namespace Phosphate.Cache;

public static class CacheLoader
{
    public static void LoadValuesIntoCache()
    {
        
    }

    public static void SaveValuesToCache()
    {
        var jsonString = JsonSerializer.Serialize(CacheObjects.SettingsCache, new JsonSerializerOptions{WriteIndented = true, IncludeFields = true});
        Console.WriteLine(jsonString);
    }
}