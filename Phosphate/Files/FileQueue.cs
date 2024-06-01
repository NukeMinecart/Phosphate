using System.IO;
using Phosphate.Files.Json;

namespace Phosphate.Files;

public static class FileQueue
{
    private struct SaveData(FileInfo path, object toSave, Type type)
    {
        public readonly FileInfo Path = path;
        public readonly object ToSave = toSave;
        public readonly Type Type = type;
    }

    public static void Add<T>(FileInfo fileLocation, T toSave)
    {
        //TODO IMPLEMENT A LOADING QUEUE AS WELL -> ALSO MAKE SURE TO SYNCHRONIZE BOTH QUEUES TOGETHER
        ThreadPool.QueueUserWorkItem(Save, new SaveData(fileLocation, toSave!, toSave!.GetType()));
    }
    private static void Save(object? threadSaveData)
    {
        var saveData = (SaveData)threadSaveData!;
        JsonLoader.SaveValuesToJson(saveData.Path, Convert.ChangeType(saveData.ToSave, saveData.Type));
    }
}