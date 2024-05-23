using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using Phosphate.Cache;
using Phosphate.Launcher;
using Phosphate.View.Pages;
using Wpf.Ui.Controls;
using Button = Wpf.Ui.Controls.Button;
using Image = Wpf.Ui.Controls.Image;
using ImageConverter = Phosphate.Converters.ImageConverter;

namespace Phosphate.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{
    public MainWindow()
    {
        InitializeComponent();
        CacheLoader.LoadValuesIntoCache();
        UpdateSettings.Update();
    }
    
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
            DragMove();
    }
    
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        ((HwndSource)PresentationSource.FromVisual(this)!).AddHook(HookProc);
        NavView.Navigate(typeof(MainPage));
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
                mmi.ptMaxSize.Y = Math.Abs(rcWorkArea.Bottom - rcWorkArea.Top - 1);
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