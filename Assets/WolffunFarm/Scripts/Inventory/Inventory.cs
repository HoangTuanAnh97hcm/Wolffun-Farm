using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] NewGameSO newGameSO;

    private Seeds[] seeds;
    private Products[] products;

    private const string DATA_NAME = "Invertory";

    private void Awake()
    {
        Instance = this;

        AgriculturalSO[] agriculturalSO = Resources.LoadAll<AgriculturalSO>("");

        seeds = agriculturalSO.Select(s => new Seeds() { name = s.name }).ToArray();
        products = agriculturalSO.Select(s => new Products() { name = s.name }).ToArray();

        Load();
    }

    #region Seed
    /// <summary>
    /// Set Amount of Seed
    /// </summary>
    /// <param name="name">Name of Seed</param>
    /// <param name="value">Value Amount</param>
    public void SetAmountSeed(string name, int value)
    {
        seeds.Where(s => s.name == name).FirstOrDefault().amounts += value;

        Save();
    }

    /// <summary>
    /// Get Amount of Seed
    /// </summary>
    /// <param name="name">Name of Seed</param>
    /// <returns></returns>
    public int GetAmountSeed(string name)
    {
        return seeds.Where(s => s.name == name).FirstOrDefault().amounts;
    }

    public Seeds[] GetAllSeeds()
    {
        return seeds;
    }
    #endregion

    #region Product
    /// <summary>
    /// Set Amount of Product
    /// </summary>
    /// <param name="name">Name of Product</param>
    /// <param name="value">Value Amount</param>
    public void SetAmountProduct(string name, int value)
    {
        products.Where(s => s.name == name).FirstOrDefault().amounts += value;

        Save();
    }

    /// <summary>
    /// Get Amount of Product
    /// </summary>
    /// <param name="name">Name of Product</param>
    /// <returns></returns>
    public int GetAmountProduct(string name)
    {
        return products.Where(s => s.name == name).FirstOrDefault().amounts;
    }

    public Products[] GetAllProducts()
    {
        return products;
    }
    #endregion

    #region SaveLoad
    public class SaveObject
    {
        public Seeds[] seeds;
        public Products[] products;
    }

    private void Save()
    {
        SaveObject saveObject = new SaveObject()
        {
            seeds = seeds,
            products = products
        };

        SaveSystem.SaveObject(DATA_NAME, saveObject);
    }

    private void Load()
    {
        SaveObject saveObject = SaveSystem.LoadObject<SaveObject>(DATA_NAME);

        if (saveObject == null)
        {
            LoadNewGame();
            return;
        }

        seeds = saveObject.seeds;
        products = saveObject.products;
    }

    private void LoadNewGame()
    {
        Logging.LogMessage("New Game");
        foreach (var item in newGameSO.seeds)
        {
            SetAmountSeed(item.name, item.amounts);
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    #endregion

    private void OnValidate()
    {
        if (!gameObject.activeInHierarchy) return;

        if (newGameSO == null) Logging.LogWarning(gameObject.name + " Missing Reference NewGameSO");
    }
}

[Serializable]
public class Seeds
{
    public string name;
    public int amounts;
}

[Serializable]
public class Products
{
    public string name;
    public int amounts;
}
