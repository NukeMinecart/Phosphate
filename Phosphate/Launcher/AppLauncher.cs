using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Phosphate.Launcher;

public static class AppLauncher
{
    public static void LaunchExe(FileInfo exe)
    {
        Process.Start(exe.FullName);
    }
    
    public static bool IsExecutableFile(FileInfo fileInfo)
    {
        var twoBytes = new byte[2];
        int bytesRead;
        try
        {
            using var fileStream = File.Open(fileInfo.FullName, FileMode.Open);
            bytesRead = fileStream.Read(twoBytes, 0, 2);
        }
        catch (Exception)
        {
            return false;
        }

        return bytesRead == 2 && Encoding.UTF8.GetString(twoBytes) == "MZ" && fileInfo.Extension.Equals(".exe");
    }
}