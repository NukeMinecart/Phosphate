using System.IO;
using System.Text.Json;

namespace Phosphate.Files.Json;

public static class JsonLoader
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        IncludeFields = true
    };

    private static readonly Thread WatcherThread = new(WatchThreadPool);
    private static readonly HashSet<Task> TaskSet = [];
    private static readonly EventWaitHandle WatcherThreadWait = new(false, EventResetMode.ManualReset);
    private static bool _isFinished;

    public static void Initialize()
    {
        WatcherThread.Start();
    }

    private static void WatchThreadPool()
    {
        while (true)
        {
            foreach (var task in TaskSet)
            {
                switch (task.Status)
                {
                    case TaskStatus.RanToCompletion:
                        TaskSet.Remove(task);
                        break;
                    case TaskStatus.Faulted:
                        throw new ThreadStateException("Task faulted");
                    case TaskStatus.Created:
                    case TaskStatus.WaitingForActivation:
                    case TaskStatus.WaitingToRun:
                    case TaskStatus.Running:
                    case TaskStatus.WaitingForChildrenToComplete:
                    case TaskStatus.Canceled:
                    default:
                        break;
                }
            }

            if (TaskSet.Count == 0)
            {
                if (_isFinished)
                    break;

                WatcherThreadWait.Reset();
                WatcherThreadWait.WaitOne();
            }
        }
    }

    public static void EndWatcherThread()
    {
        _isFinished = true;
        WatcherThreadWait.Set();
    }

    public static T LoadValuesFromJson<T>(FileInfo path)
    {
        var fileStream = new FileStream(path.FullName, FileMode.Open);
        return JsonSerializer.Deserialize<T>(fileStream, SerializerOptions) ??
               throw new NullReferenceException("Json deserialization is null");
    }

    public static void SaveValuesToJson<T>(FileInfo path, T value)
    {
        var jsonString = JsonSerializer.Serialize(value);
        WatcherThreadWait.Set();
        TaskSet.Add(File.WriteAllTextAsync(path.FullName, jsonString));
    }

    public static bool CreateFile(FileInfo path)
    {
        if (!path.Exists)
        {
            path.Create();
            return true;
        }

        return false;
    }
}