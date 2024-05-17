using Wpf.Ui.Appearance;

namespace Phosphate.Cache;

public static class UpdateSettings
{
    public static void Update()
    {
        ApplicationThemeManager.Apply(CacheObjects.SettingsCache.GetValue(CacheKeys.HighContrast, false, CacheObjects.BooleanConverter)
            ? ApplicationTheme.HighContrast
            : CacheObjects.SettingsCache.GetValue(CacheKeys.DarkTheme, true, CacheObjects.BooleanConverter)
                ? ApplicationTheme.Dark
                : ApplicationTheme.Light);
    }
}