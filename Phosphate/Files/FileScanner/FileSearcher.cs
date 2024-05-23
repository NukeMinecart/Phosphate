using System.IO;
using Phosphate.Launcher;

namespace Phosphate.Files.FileScanner;

public static class FileSearcher
{
    public static void SearchForExe(FileInfo startingDirectory)
    {
        // Find all .exe files on the hard drive
        var filePaths = SearchForFiles(startingDirectory, "exe").Where(AppLauncher.IsExecutableFile);
        
        foreach (var exe in filePaths)
        {
            Console.WriteLine(exe);
        }
    }

    public static IEnumerable<FileInfo> SearchForFiles(FileInfo startingDirectory, string fileExtension)
    {
        if ((startingDirectory.Attributes & FileAttributes.Directory) != FileAttributes.Directory)
            throw new FileFormatException("Invalid starting directory");
        
        var filePaths = Directory.EnumerateFiles(startingDirectory.FullName, "*."+fileExtension, new EnumerationOptions
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = true,

        }).Select(file => new FileInfo(file));
        return filePaths;
    }

}