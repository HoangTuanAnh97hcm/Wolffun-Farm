using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public static GridSystem Instance { get; private set; }

    public event EventHandler OnLoaded;

    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private float gridSize;
    [SerializeField] private PlacedObject placedObject;
    [SerializeField] private GlobalInforSO globalInforSO;
    [SerializeField] private NewGameSO newGameSO;

    private AgriculturalSO agriculturalSO;
    private Grid<GridObject> grid;
    private const string DATA_NAME = "GridData";

    private void OnValidate()
    {
        if (!gameObject.activeInHierarchy) return;

        if (placedObject == null) Logging.LogWarning(gameObject.name + " Missing Reference PlacedObject");
        if (globalInforSO == null) Logging.LogWarning(gameObject.name + " Missing Reference GlobalInforSO");
        if (newGameSO == null) Logging.LogWarning(gameObject.name + " Missing Reference NewGameSO");
    }

    private void Awake()
    {
        Instance = this;

        grid = new Grid<GridObject>(gridWidth, gridHeight, gridSize, Vector3.zero, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));

        Load();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // x is x of Grid
            // y is y of Grid
            grid.GetXY(UtilsClass.GetMouseWorldPosition(), out int x, out int y);
            GridObject gridObject = grid.GetGridObject(x, y);

            if (gridObject == null)
            {
                this.placedObject = null;
                this.agriculturalSO = null;
                return;
            }

            if (gridObject.CanPlaced() && placedObject != null)
            {
                PlacingObject(gridObject, x, y);
                this.placedObject = null;

                GameData.Instance.SetCoint(globalInforSO.priceLand);
                Save();
            }
            else if (!gridObject.CanPlaced() && agriculturalSO != null)
            {
                PlacingAgricultural(x, y);

                Inventory.Instance.SetAmountSeed(agriculturalSO.name, -1);

                this.agriculturalSO = null;
                Save();
            }

            gridObject.GetPlaceObject()?.Harvest();
        }
    }

    private void PlacingObject(GridObject gridObject, int x, int y)
    {
        PlacedObject placedObject = PlacedObject.Create(grid.GetWorldPosition(x, y), this.placedObject.transform);
        gridObject.SetPlaceObject(placedObject);
    }

    private void PlacingAgricultural(int x, int y)
    {
        PlacedObject placedObject = grid.GetGridObject(x, y).GetPlaceObject();
        placedObject.SetAgricultural(agriculturalSO);
        placedObject.StartStateProduction();
    }

    /// <summary>
    /// Set Prefab PlacedOBject to Grid System
    /// </summary>
    /// <param name="placedObject">Prefab has scritp Placed Object.</param>
    public void SetPlancedObject(PlacedObject placedObject)
    {
        this.placedObject = placedObject;
    }

    /// <summary>
    /// Set Agricultural ScriptableObject to Grid System
    /// </summary>
    /// <param name="agriculturalSO">Agricultura ScriptableOBject.</param>
    public void SetAgricultural(AgriculturalSO agriculturalSO)
    {
        this.agriculturalSO = agriculturalSO;
    }

    #region SaveLoad
    public class SaveObject
    {
        public GridObject.SaveObject[] gridObjectSaveObjectArray;
    }

    public void Save()
    {
        List<GridObject.SaveObject> gridObjectSaveObjectList = new List<GridObject.SaveObject>();
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                GridObject gridObject = grid.GetGridObject(x, y);
                gridObjectSaveObjectList.Add(gridObject.Save());
            }
        }

        SaveObject saveObject = new SaveObject { gridObjectSaveObjectArray = gridObjectSaveObjectList.ToArray() };

        SaveSystem.SaveObject(DATA_NAME, saveObject);
    }

    public void Load()
    {
        SaveObject saveObject = SaveSystem.LoadObject<SaveObject>(DATA_NAME);

        if (saveObject == null)
        {
            NewGame();

            placedObject = null;
            return;
        }

        foreach (GridObject.SaveObject gridObjectSaveObject in saveObject.gridObjectSaveObjectArray)
        {
            GridObject gridObject = grid.GetGridObject(gridObjectSaveObject.x, gridObjectSaveObject.y);

            if (gridObjectSaveObject.placedObject != "")
            {
                PlacingObject(gridObject, gridObjectSaveObject.x, gridObjectSaveObject.y);
                gridObject.Load(gridObjectSaveObject);
            }
        }

        placedObject = null;
        OnLoaded?.Invoke(this, EventArgs.Empty);
    }

    private void NewGame()
    {
        int firstXIndex = 0;

        for (int yIndex = 0; yIndex < newGameSO.plantedLand; yIndex++)
        {
            GridObject gridObject = grid.GetGridObject(firstXIndex, yIndex);
            PlacingObject(gridObject, firstXIndex, yIndex);
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    #endregion

    public class GridObject
    {
        private Grid<GridObject> grid;
        private int x, y;
        private PlacedObject placedObject;

        public GridObject(Grid<GridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void SetPlaceObject(PlacedObject placedObject)
        {
            this.placedObject = placedObject;
        }

        public PlacedObject GetPlaceObject()
        {
            return placedObject;
        }

        public void ClearPlaceObject()
        {
            placedObject = null;
        }

        public bool CanPlaced()
        {
            return placedObject == null;
        }

        [System.Serializable]
        public class SaveObject
        {
            public string placedObject;
            public int x;
            public int y;
        }

        /*
         * Save - Load
         * */
        public SaveObject Save()
        {
            return new SaveObject
            {
                placedObject = placedObject?.SaveData(),
                x = x,
                y = y,
            };
        }

        public void Load(SaveObject saveObject)
        {
            placedObject?.LoadData(saveObject.placedObject);
            x = saveObject.x;
            y = saveObject.y;
        }
    }
}
