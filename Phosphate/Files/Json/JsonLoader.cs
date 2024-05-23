using System.IO;
using System.Text.Json;
using Phosphate.Cache;

namespace Phosphate.Files.Json;

public static class JsonLoader
{
    private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
    {
        IncludeFields = true
    };

    public static T LoadValuesFromJson<T>(FileInfo path)
    {
        var fileStream = new FileStream(path.FullName, FileMode.Open);
        return JsonSerializer.Deserialize<T>(fileStream, _serializerOptions) ??
                                     throw new NullReferenceException("Json deserialization is null");
    }

    public static void SaveValuesToJson<T>(FileInfo path, T value)
    {
        var jsonString = JsonSerializer.Serialize(value);
        File.WriteAllText(path.FullName, jsonString);
    }

    public static bool CreateFile(FileInfo path)
    {
        if (!path.Exists)
        {
            path.Create();
            return true;
        }

        return false;
    }
}