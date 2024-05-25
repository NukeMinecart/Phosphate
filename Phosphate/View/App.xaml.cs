using System.IO;
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
        //TODO Watch what happens when application is immediately shutdown -> maybe make backup config files and save them if initial ones are invalid (app crashed)
        
        base.OnExit(e);
    }
}