using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public static GridSystem Instance { get; private set; }

    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private float gridSize;
    [SerializeField]private PlacedObject placedObject;
    [SerializeField]private AgriculturalSO agriculturalSO;

    private Grid<GridObject> grid;

    private void Awake()
    {
        Instance = this;

        grid = new Grid<GridObject>(gridWidth, gridHeight, gridSize, Vector3.zero, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
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

            }else if (!gridObject.CanLand() && agriculturalSO != null)
            {
                PlacingAgricultural(x, y);
            }
        }
    }

    private void PlacingObject(GridObject gridObject, int x, int y)
    {
        PlacedObject placedObject = PlacedObject.Create(grid.GetWorldPosition(x, y), this.placedObject.transform);
        gridObject.SetPlaceObject(placedObject);

        this.placedObject = null;
    }

    private void PlacingAgricultural(int x, int y)
    {
        PlacedObject placedObject = grid.GetGridObject(x, y).GetPlaceObject();
        placedObject.SetAgricultural(agriculturalSO);

        this.agriculturalSO = null;
    }

    /// <summary>
    /// Set Prefab PlacedOBject to Grid System
    /// </summary>
    /// <param name="placedObject">Prefab has scritp Placed Object.</param>
    public void SetPlaceObject(PlacedObject placedObject)
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
}
