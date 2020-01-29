using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingNode
{
    private PathGrid<PathfindingNode> grid;
    private int x;
    private int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathfindingNode parentNode;

    public bool isWalkable;

    public PathfindingNode(PathGrid<PathfindingNode> _grid, int _x, int _y)
    {
        grid = _grid;
        x = _x;
        y = _y;
        isWalkable = true;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public override string ToString()
    {
        return "Node is X: " + x + " " + "Y: " + y + "\nNode walkability is:" + isWalkable;
    }

    public int GetX()
    {
        return x;
    }
    public void SetX(int newX)
    {
        x = newX;
    }
    public int GetY()
    {
        return y;
    }
    public void SetY(int newY)
    {
        x = newY;
    }

    public void SetXY(int newX, int newY)
    {
        x = newX;
        y = newY;
    }
}
