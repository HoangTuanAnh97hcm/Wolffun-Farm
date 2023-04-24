using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public static GridSystem Instance { get; private set; }

    public event EventHandler OnLoaded;

    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private float gridSize;
    [SerializeField]private PlacedObject placedObject;
    [SerializeField]private AgriculturalSO agriculturalSO;

    private Grid<GridObject> grid;

    private const string DATA_NAME = "GridData.txt";
    private string SAVE_PATH { get { return Application.persistentDataPath + DATA_NAME; } }

    private void Awake()
    {
        Instance = this;

        grid = new Grid<GridObject>(gridWidth, gridHeight, gridSize, Vector3.zero, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));

        Load();
    }

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

        public bool CanLand()
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // x is x of Grid
            // y is y of Grid
            grid.GetXY(UtilsClass.GetMouseWorldPosition(), out int x, out int y);
            GridObject gridObject = grid.GetGridObject(x, y);

            if (gridObject == null) return;

            if (gridObject.CanLand() && placedObject != null)
            {
                PlacingObject(gridObject, x, y);
                this.placedObject = null;
                Save();
            }
            else if (!gridObject.CanLand() && agriculturalSO != null)
            {
                PlacingAgricultural(x, y);
            }
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
        
        this.agriculturalSO = null;

        Save();
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

    /*
        * Save - Load
        * */
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

        SaveSystem.SaveObject(saveObject);
    }

    public void Load()
    {
        SaveObject saveObject = SaveSystem.LoadMostRecentObject<SaveObject>();

        if (saveObject == null) return;

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

    private void OnApplicationQuit()
    {
        Save();
    }
}
