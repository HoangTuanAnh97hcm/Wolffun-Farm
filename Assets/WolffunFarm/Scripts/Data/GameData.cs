using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    private const string DATA_NAME = "GameData";

    private int coints;
    private int levelDevices;

    private void Awake()
    {
        Instance = this;

        Load();
    }

    public void SetCoint(int value)
    {
        coints += value;
        Save();
    }

    public int GetCoint()
    {
        return coints;
    }

    #region SaveLoad
    public class SaveObject
    {
        public int coints;
        public int levelDevices;
    }

    private void Save()
    {
        SaveObject saveObject = new SaveObject()
        {
            coints = coints,
            levelDevices = levelDevices
        };

        SaveSystem.SaveObject(DATA_NAME, saveObject);
    }

    private void Load()
    {
        SaveObject saveObject = SaveSystem.LoadObject<SaveObject>(DATA_NAME);

        if (saveObject == null) return;

        coints = saveObject.coints;
        levelDevices = saveObject.levelDevices;
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    #endregion
}
