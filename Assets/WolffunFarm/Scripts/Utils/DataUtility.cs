using Newtonsoft.Json;
using UnityEngine;

public static class DataUtility
{
    public static void ExportData<T>(string path, T data)
    {
        string json = JsonConvert.SerializeObject(data);

        System.IO.File.WriteAllText(path, json);

        Logging.LogMessage(json);
    }

    public static T ImportData<T>(string path)
    {
        if (System.IO.File.Exists(path))
        {
            return JsonConvert.DeserializeObject<T>(System.IO.File.ReadAllText(path));
        }
        else return default;
    }

    public static T ImportData<T>(TextAsset text)
    {
        return JsonConvert.DeserializeObject<T>(text.ToString());
    }
}
