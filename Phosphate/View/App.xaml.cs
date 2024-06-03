﻿using System.IO;
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
        JsonLoader.IsFinished = true;
        CacheLoader.SaveValuesFromCache();
        base.OnExit(e);
    }
}