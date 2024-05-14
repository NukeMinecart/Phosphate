using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Media;
using Phosphate.Launcher;
using Image = System.Windows.Controls.Image;
using ImageConverter = Phosphate.Converters.ImageConverter;

namespace Phosphate.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        StateChanged += (_, _) => ChangeFullscreenButton();
        // ExecutableScanner.SearchForExe(new FileInfo("C:\\"));
        AddLaunch(new FileInfo(@"C:\Users\bradl\OneDrive\Desktop\Stuff\mmc-stable-win32\MultiMC\MultiMC.exe"), "Minecraft");
    }

    private void FadeWindowAnimation(float startValue, float endValue, Duration time, EventHandler endAction)
    {
        var animation = new DoubleAnimation(startValue, endValue, time);
        animation.Completed += endAction;
        BeginAnimation(OpacityProperty, animation);
    }

    private void FadeWindowAnimation(float startValue, float endValue, Duration time)
    {
        FadeWindowAnimation(startValue, endValue, time, (_, _) => { });
    }

    private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void OnMaximizeRestoreButtonClick(object sender, RoutedEventArgs e)
    {
        FadeWindowAnimation(0, 1, TimeSpan.FromSeconds(0.15));
        WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        ChangeFullscreenButton();
    }

    private void ChangeFullscreenButton()
    {
        if (WindowState == WindowState.Maximized)
        {
            FullscreenButton.Visibility = Visibility.Hidden;
            UnFullscreenButton.Visibility = Visibility.Visible;
        }
        else
        {
            FullscreenButton.Visibility = Visibility.Visible;
            UnFullscreenButton.Visibility = Visibility.Hidden;
        }
    }
    
    private void AddLaunch(FileInfo file, string name)
    {
        var image = new Image
        {
            Stretch = Stretch.Uniform,
            MaxWidth = 100,
            MaxHeight = 100
        };
        var icon = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);

        image.MouseDown += (_, _) => AppLauncher.LaunchExe(file);

        if (icon != null)
            image.Source = ImageConverter.ToImageSource(icon);
        
        Grid.SetRow(image, 0);
        Grid.SetColumn(image, 0);
        
        var button = new Button
        {
            Content = name,
        };
        
        button.Click += (_, _) => AppLauncher.LaunchExe(file);
        Grid.SetRow(button, 2);
        Grid.SetColumn(button, 0);
        
        var grid = new Grid
        {
            Children = { image, button },
        };

        var column1 = new ColumnDefinition();
        var column2 = new ColumnDefinition();

        var row1 = new RowDefinition();
        var row2 = new RowDefinition();
        var row3 = new RowDefinition();
        
        grid.ColumnDefinitions.Add(column1);
        grid.ColumnDefinitions.Add(column2);

        grid.RowDefinitions.Add(row1);
        grid.RowDefinitions.Add(row2);
        grid.RowDefinitions.Add(row3);

        LaunchPanel.Children.Add(grid);
    }
    

    private void OnCloseButtonClick(object sender, RoutedEventArgs e)
    {
        FadeWindowAnimation(1, 0, TimeSpan.FromSeconds(0.05), (_, _) => Close());
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        ((HwndSource)PresentationSource.FromVisual(this)!).AddHook(HookProc);
    }

    private static IntPtr HookProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg == WmGetMinMaxInfo)
        {
            // We need to tell the system what our size should be when maximized. Otherwise it will cover the whole screen,
            // including the task bar.
            var mmi = (MinMaxInfo)Marshal.PtrToStructure(lParam, typeof(MinMaxInfo))!;

            // Adjust the maximized size and position to fit the work area of the correct monitor
            var monitor = MonitorFromWindow(hwnd, MonitorDefaultToNearest);

            if (monitor != IntPtr.Zero)
            {
                var monitorInfo = new MonitorInfo
                {
                    cbSize = Marshal.SizeOf(typeof(MonitorInfo))
                };
                GetMonitorInfo(monitor, ref monitorInfo);
                var rcWorkArea = monitorInfo.rcWork;
                var rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.X = Math.Abs(rcWorkArea.Left - rcMonitorArea.Left);
                mmi.ptMaxPosition.Y = Math.Abs(rcWorkArea.Top - rcMonitorArea.Top);
                mmi.ptMaxSize.X = Math.Abs(rcWorkArea.Right - rcWorkArea.Left);
                mmi.ptMaxSize.Y = Math.Abs(rcWorkArea.Bottom - rcWorkArea.Top);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        return IntPtr.Zero;
    }

    private const int WmGetMinMaxInfo = 0x0024;

    private const uint MonitorDefaultToNearest = 0x00000002;

    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromWindow(IntPtr handle, uint flags);

    [DllImport("user32.dll")]
    private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfo lpmi);

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect(int left, int top, int right, int bottom)
    {
        public int Left = left;
        public int Top = top;
        public int Right = right;
        public int Bottom = bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MonitorInfo
    {
        public int cbSize;
        public Rect rcMonitor;
        public Rect rcWork;
        public uint dwFlags;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Point(int x, int y)
    {
        public int X = x;
        public int Y = y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MinMaxInfo
    {
        public Point ptReserved;
        public Point ptMaxSize;
        public Point ptMaxPosition;
        public Point ptMinTrackSize;
        public Point ptMaxTrackSize;
    }
}