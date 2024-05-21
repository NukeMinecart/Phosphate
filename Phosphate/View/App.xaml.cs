using System.Windows;
using Phosphate.Cache;

namespace Phosphate.View;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnExit(ExitEventArgs e)
    {
        CacheLoader.SaveValuesFromCache();
        base.OnExit(e);
    }
}