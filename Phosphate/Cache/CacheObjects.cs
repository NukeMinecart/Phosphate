using System.IO;
using System.Text.Json;
using Phosphate.Event;

namespace Phosphate.Cache;

public static class CacheObjects
{
    public static Cache SettingsCache { get; set; } = new();
    public static Cache LaunchItemCache { get; } = new();

    public static Property<List<FileInfo>> ExecutableItemCache { get; } = new();

    public static readonly Func<object, bool> BooleanConverter = value => bool.Parse(value.ToString()!);
    public static readonly Func<object, string> StringConverter = value => value.ToString()!;
    
    public class Cache : Dictionary<string, object>
    {
        public void AddValue(string key, object value)
        {
            if (!TryAdd(key, value)) this[key] = value;
        }
        
        public TK GetValue<TK>(string key, TK defaultValue)
        {
            if (!ContainsKey(key))
            {
                Console.WriteLine("Key missing from cache");
                return defaultValue;
            }

            if (this[key].GetType() != typeof(TK))
            {
                Console.WriteLine("Type doesn't match cache type");
                return defaultValue;
            }

            return (TK)this[key];
        }
        
        public TK GetValue<TK>(string key, TK defaultValue, Func<object, TK> converter)
        {
            if (!ContainsKey(key))
            {
                Console.WriteLine("Key missing from cache");
                return defaultValue;
            }

            return converter.Invoke(this[key]);
        }
    }

    
}