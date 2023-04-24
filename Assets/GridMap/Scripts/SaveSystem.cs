/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveSystem {

    private const string SAVE_EXTENSION = "txt";

    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    private static bool isInit = false;

    public static void Init() {
        if (!isInit) {
            isInit = true;
            // Test if Save Folder exists
            if (!Directory.Exists(SAVE_FOLDER)) {
                // Create Save Folder
                Directory.CreateDirectory(SAVE_FOLDER);
            }
        }
    }

    private static string getPath(string fileName)
    {
        return SAVE_FOLDER + fileName + "." + SAVE_EXTENSION;
    }

    private static void Save(string fileName, string saveString) {
        Init();
        File.WriteAllText(getPath(fileName), saveString);
    }

    public static void SaveObject(string fileName, object saveObject) {
        Init();
        string json = JsonUtility.ToJson(saveObject, true);
        Save(fileName, json);
        Logging.LogMessage(json);
    }

    private static string Load(string fileName) {
        Init();
        if (File.Exists(SAVE_FOLDER + fileName + "." + SAVE_EXTENSION)) {
            string saveString = File.ReadAllText(getPath(fileName));
            return saveString;
        } else {
            return null;
        }
    }

    public static TSaveObject LoadObject<TSaveObject>(string fileName) {
        Init();
        string saveString = Load(fileName);
        if (saveString != null) {
            TSaveObject saveObject = JsonUtility.FromJson<TSaveObject>(saveString);
            return saveObject;
        } else {
            return default(TSaveObject);
        }
    }

    public static string NewtonsoftSerializeObject(object saveObject)
    {
        return JsonConvert.SerializeObject(saveObject);
    }

    public static TSaveObject NewtonsoftDeserializeObject<TSaveObject>(string saveObject)
    {
        return JsonConvert.DeserializeObject<TSaveObject>(saveObject);
    }
}
