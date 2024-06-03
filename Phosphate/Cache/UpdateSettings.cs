using System.IO;
using Phosphate.Converters;
using Phosphate.Files.FileScanner;
using Phosphate.Files.Json;
using Wpf.Ui.Appearance;
using static Phosphate.Cache.SettingKeys;

namespace Phosphate.Cache;

public static class UpdateSettings
{
    public static void Start()
    {
        JsonLoader.Initialize();
        if (CacheObjects.SettingsCache.GetValue(RescanOnReload, false, CacheObjects.BooleanConverter))
        {
            var fileSearchThread = new Thread(() =>
            {
                CacheObjects.ExecutableItemCache.Value = FileSearcher.SearchForExe(new FileInfo("C:/")).ToList();
            });
            fileSearchThread.Start();
        }

        ApplicationThemeManager.Apply(ThemeToIndexConverter.ConvertIndexToTheme(
            CacheObjects.SettingsCache.GetValue(ThemeIndex, ThemeToIndexConverter.ConvertThemeToIndex(ApplicationTheme.Dark), CacheObjects.IntegerConverter)));    
    }
}