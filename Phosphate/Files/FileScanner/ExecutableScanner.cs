using System.IO;
using Phosphate.Launcher;

namespace Phosphate.Files.FileScanner;

public static class ExecutableScanner
{
    public static void SearchForExe(FileInfo startingDirectory)
    {
        // Find all .exe files on the hard drive
        if ((startingDirectory.Attributes & FileAttributes.Directory) != FileAttributes.Directory)
            throw new FileFormatException("Invalid starting directory");

        var filePaths = Directory.EnumerateFiles(startingDirectory.FullName, "*.exe", new EnumerationOptions
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = true,

        }).Select(file => new FileInfo(file)).Where(AppLauncher.IsExecutableFile);
         
        
        foreach (var exe in filePaths)
        {
            Console.WriteLine(exe);
        }
    }
}