using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private float gridSize;
    [SerializeField] private GameObject placedObjectPrefab;

    private Grid<GridObject> grid;

    private void Awake()
    {
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

        public bool CanBuild()
        {
            return placedObject == null;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.GetXY(UtilsClass.GetMouseWorldPosition(), out int x, out int y);
            GridObject gridObject = grid.GetGridObject(x, y);

            if (gridObject != null && gridObject.CanBuild())
            {
                PlacedObject placedObject = PlacedObject.Create(grid.GetWorldPosition(x, y), placedObjectPrefab.transform);
                gridObject.SetPlaceObject(placedObject);
            }else
            {
                // Can not build
            }
        }
    }

}
