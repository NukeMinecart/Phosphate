using System.Windows;
using Phosphate.Cache;
using Phosphate.Files.Json;

namespace Phosphate.View;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnExit(ExitEventArgs e)
    {
        JsonLoader.EndWatcherThread();
        CacheLoader.SaveValuesFromCache();
        base.OnExit(e);
    }
}