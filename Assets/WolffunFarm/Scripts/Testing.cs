using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject prefab;
    private Grid grid;
    void Start()
    {
         grid = new Grid(10, 10, 1, Vector3.zero);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.GetXY(Camera.main.ScreenToWorldPoint(Input.mousePosition), out int x, out int y);

            Instantiate(prefab, grid.GetWorldPosition(x, y), Quaternion.identity);
        }
    }
}
