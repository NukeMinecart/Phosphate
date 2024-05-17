namespace Phosphate.Cache;

public static class CacheObjects
{
    public static readonly Cache SettingsCache = new ();

    public static void InitializeTOREMOVE()
    {
        //TODO Remove this method and implement a file based solution
        SettingsCache.AddValue(CacheKeys.DarkTheme, true);
        SettingsCache.AddValue(CacheKeys.HighContrast, false);
    }
    
    public class Cache : Dictionary<string, object>
    {
        public void AddValue(string key, object value)
        {
            if (!TryAdd(key, value)) this[key] = value;
        }
        
        public TK GetValue<TK>(string key, TK defaultValue)
        {
            if (this[key].GetType() != typeof(TK) || !ContainsKey(key))
            {
                Console.WriteLine("Type is incorrect or key is not found");
                return defaultValue;
            }
            return (TK)this[key];
        }
    }

    
}