using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGrid<T>
{
    int width, height;
    float cellSize;
    Vector3 origin;
    T[,] gridArray; 

    public PathGrid(int _width, int _height, float _cellSize, Vector3 _origin)
    {
        width = _width;
        height = _height;
        cellSize = _cellSize;
        origin = _origin;

        gridArray = new T[width, height];

        
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Debug.DrawLine(GetWorldPos(i,j), GetWorldPos(i, j + 1), Color.green, 1000f);
                Debug.DrawLine(GetWorldPos(i, j), GetWorldPos(i + 1, j), Color.green, 1000f);
            }
        }
        Debug.DrawLine(GetWorldPos(0, height), GetWorldPos(width, height), Color.green, 1000f);
        Debug.DrawLine(GetWorldPos(width, 0), GetWorldPos(width, height), Color.green, 1000f);
        
    }

    private Vector3 GetWorldPos(int x, int y)
    {
        return new Vector3(x, y) * cellSize + origin;
    }

    public Vector2Int GetGridPos(Vector3 worldPos)
    {
        Vector2Int gridPos = new Vector2Int();
        gridPos.x = Mathf.FloorToInt((worldPos.x - origin.x) / cellSize);
        gridPos.y = Mathf.FloorToInt((worldPos.y - origin.y) / cellSize);
        
        return gridPos;
    }
    public void GetGridPos(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos.x - origin.x) / cellSize);
        y = Mathf.FloorToInt((worldPos.y - origin.y) / cellSize);

        return;
    }


    public void SetGridObject(int x, int y, T value)
    {
        if ((x >= 0 && y >= 0) && (x < width && y < height))
            gridArray[x, y] = value;
    }
    public void SetGridObject(Vector3 pos, T value)
    {
        //Vector2Int xy = new Vector2Int();
        //xy = GetGridPos(pos);
        //SetGridObject(xy.x, xy.y, value);
        int x, y;
        GetGridPos(pos, out x, out y);
        SetGridObject(x, y, value);
    }

    public T GetGridObject(int x, int y)
    {
        if ((x >= 0 && y >= 0) && (x < width && y < height))
        {
            return gridArray[x, y];
        }
        else
        {
            return default;
        }

    }
    public T GetGridObject(Vector3 pos)
    {
        Vector2Int xy = new Vector2Int();
        xy = GetGridPos(pos);
        return GetGridObject(xy.x, xy.y);
    }

    public int GetGridWidth()
    {
        return width;
    }
    public int GetGridHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }
}
