using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    public static EventHandler<GameDataEventArgs> OnDataChange;
    public class GameDataEventArgs : EventArgs
    {
        public int coint;
        public int levelDevices;
    }
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

        OnDataChange?.Invoke(this, new GameDataEventArgs() { coint = coints, levelDevices = levelDevices});
    }

    public int GetCoint()
    {
        return coints;
    }

    public void SetLevelDevice()
    {
        levelDevices++;
        Save();

        OnDataChange?.Invoke(this, new GameDataEventArgs() { coint = coints, levelDevices = levelDevices});
    }

    public int GetLevelDevice()
    {
        return levelDevices;
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

        if (saveObject == null)
        {
            NewGame();
            return;
        }

        coints = saveObject.coints;
        levelDevices = saveObject.levelDevices;
    }

    private void NewGame()
    {
        coints = 0;
        levelDevices = 1;
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    #endregion
}
