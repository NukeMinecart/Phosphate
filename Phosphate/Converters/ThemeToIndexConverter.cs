// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Wpf.Ui.Appearance;

namespace Phosphate.Converters;

internal static class ThemeToIndexConverter
{
    public static int ConvertThemeToIndex(object? value)
    {
        return value switch
        {
            ApplicationTheme.Dark => 1,
            ApplicationTheme.HighContrast => 2,
            _ => 0
        };
    }

    public static ApplicationTheme ConvertIndexToTheme(object? value)
    {
        return value switch
        {
            1 => ApplicationTheme.Dark,
            2 => ApplicationTheme.HighContrast,
            _ => ApplicationTheme.Light
        };
    }
}