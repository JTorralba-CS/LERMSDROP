using System.IO;

using static System.Console;
using static System.ConsoleColor;
using static System.Environment;

using System.Runtime.InteropServices;
using System;

public class Watcher
{
    public static void Main()
    {
        Run();
    }

    private static void Run()
    {
        string[] args = GetCommandLineArgs();
        string R = GetEnvironmentVariable("SystemDrive");
        string P = "X:\\";
        string L = "L:\\LERMSDROP";

        using (FileSystemWatcher watcher = new FileSystemWatcher())
        {
            watcher.Path = P;

            watcher.NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName

                                 | NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            watcher.IncludeSubdirectories = true;
            watcher.Filter = "*.xml";

            watcher.Created += OnCreated;

            watcher.EnableRaisingEvents = true;

            WriteLine("Press .<ENTER> to quit.");
            WriteLine();

            while (Read() != '.') ;

            ForegroundColor = White;
            WriteLine();
        }
    }

    private static void OnCreated(object Source, FileSystemEventArgs E)
    {
        string TimeStamp = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");

        ForegroundColor = Green;
        WriteLine($"{TimeStamp}_____{E.FullPath}");

        var A = E.FullPath;
        var B = $"L:\\LERMSDROP\\{TimeStamp}_____{E.Name}";

        try
        {
            File.Copy(A, B);
        }
        catch (Exception X)
        {
        }

    }
}
