using System.IO;
using Phosphate.Files.FileScanner;
using Wpf.Ui.Appearance;

namespace Phosphate.Cache;

public static class UpdateSettings
{
    public static void Update()
    {
        ApplicationThemeManager.Apply(CacheObjects.SettingsCache.GetValue(SettingKeys.HighContrast, false, CacheObjects.BooleanConverter)
            ? ApplicationTheme.HighContrast
            : CacheObjects.SettingsCache.GetValue(SettingKeys.DarkTheme, true, CacheObjects.BooleanConverter)
                ? ApplicationTheme.Dark
                : ApplicationTheme.Light);
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