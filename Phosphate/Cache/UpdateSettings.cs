using System.IO;
using Phosphate.Converters;
using Phosphate.Files.FileScanner;
using Wpf.Ui.Appearance;
using static Phosphate.Cache.SettingKeys;

namespace Phosphate.Cache;

public static class UpdateSettings
{
    public static void Update()
    {
        ApplicationThemeManager.Apply(ThemeToIndexConverter.ConvertIndexToTheme(
            CacheObjects.SettingsCache.GetValue(ThemeIndex, ThemeToIndexConverter.ConvertThemeToIndex(ApplicationTheme.Dark), CacheObjects.IntegerConverter)));
    }

    public static void Start()
    {
        if (CacheObjects.SettingsCache.GetValue(SettingKeys.RescanOnReload, false, CacheObjects.BooleanConverter))
        {
            var fileSearchThread = new Thread(() =>
            {
                CacheObjects.ExecutableItemCache.Value = FileSearcher.SearchForExe(new FileInfo("C:/")).ToList();
            });
            fileSearchThread.Start();
        }

        Update();
    }
}