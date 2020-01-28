using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridTest : MonoBehaviour
{
    private Vector2 mousePos;

    [SerializeField] int x = 0;
    [SerializeField] int y = 0;
    [SerializeField] float cellSize = 0;
    [SerializeField] Vector3 origin = new Vector3(0, 0, 0);

    private PathGrid<PathfindingNode> grid;

    void Start()
    {
        grid = new PathGrid<PathfindingNode>(x, y, cellSize, origin);
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            //grid.SetGridObject(mousePos, 100);
            grid.GetGridObject(mousePos).SetXY(0, 0);
            Debug.Log(grid.GetGridObject(mousePos).GetX());
            Debug.Log(grid.GetGridObject(mousePos).GetY());
        }

        if(Input.GetMouseButtonDown(1))
        {
            grid.SetGridObject(mousePos, default);
        }
    }
}
