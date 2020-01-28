using System.Collections.Generic;
using UnityEngine;

public class AIPathfiding
{
    private const int HorizontalMovePrice = 10;
    private const int DiagonalMovePrice = 14;

    private PathGrid<PathfindingNode> grid;

    private List<PathfindingNode> openNodes;
    private List<PathfindingNode> closedNodes;

    public AIPathfiding(int x, int y, Vector3 origin)
    {
        grid = new PathGrid<PathfindingNode>(x, y, 1f, origin);
        for (int i = 0; i < grid.GetGridWidth(); i++)
        {
            for (int j = 0; j < grid.GetGridHeight(); j++)
            {
                PathfindingNode newNode = new PathfindingNode(grid, i, j);
                grid.SetGridObject(i, j, newNode);
            }
        }
    }

    public List<PathfindingNode> FindTarget(Vector2 startPos, Vector2 endPos)
    {
        PathfindingNode startNode = grid.GetGridObject(startPos);
        PathfindingNode endNode = grid.GetGridObject(endPos);

        openNodes = new List<PathfindingNode> { startNode };
        closedNodes = new List<PathfindingNode>();

        for(int i = 0; i < grid.GetGridWidth(); i++)
        {
            for(int j = 0; j < grid.GetGridHeight(); j++)
            {
                PathfindingNode node = grid.GetGridObject(i, j);
                node.gCost = int.MaxValue;
                node.CalculateFCost();
                node.parentNode = null;
            }
        }
        
        startNode.gCost = 0;
        startNode.hCost = GetDistanceValue(startNode, endNode);
        startNode.CalculateFCost();

        while(openNodes.Count > 0)
        {
            PathfindingNode currentNode = GetLowestValueNode(openNodes);
            if (currentNode == endNode)
            {
                return CalculateFinalPath(endNode);
            }

            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            foreach(PathfindingNode neighbor in FindNeighbor(currentNode))
            {
                if (closedNodes.Contains(neighbor))
                    continue;
                
                int newGCost = currentNode.gCost + GetDistanceValue(currentNode, neighbor);
                
                if(newGCost < neighbor.gCost)
                {
                    neighbor.parentNode = currentNode;
                    neighbor.gCost = newGCost;
                    neighbor.hCost = GetDistanceValue(currentNode, endNode);
                    neighbor.CalculateFCost();
                }

                if (!openNodes.Contains(neighbor))
                    openNodes.Add(neighbor);
            }
        }
        return null;
    }
    //I have grown very tired of this project. It was supposed to be a fun way for the club to work and learn together, but I am the only one who does any work. I spend my little free time working on this project that I don't have any passion for and it is crushing to me. I feel that if I stop working I will let the club down. Even when I spend my free time on other things I just feel bad fro not having worked on this project when I had the time. I am sad.
    private List<PathfindingNode> FindNeighbor(PathfindingNode centerNode)
    {
        List<PathfindingNode> neighbors = new List<PathfindingNode>();
        
        if(centerNode.GetX() - 1 >= 0)
        {
            neighbors.Add(GetNode(centerNode.GetX() - 1, centerNode.GetY()));
            if(centerNode.GetY() - 1 >= 0)
                neighbors.Add(GetNode(centerNode.GetX() - 1, centerNode.GetY() - 1));
            if (centerNode.GetY() + 1 >= 0)
                neighbors.Add(GetNode(centerNode.GetX() - 1, centerNode.GetY() + 1));
        }
        if (centerNode.GetX() + 1 < grid.GetGridWidth())
        {
            neighbors.Add(GetNode(centerNode.GetX() + 1, centerNode.GetY()));
            if (centerNode.GetY() - 1 >= 0)
                neighbors.Add(GetNode(centerNode.GetX() + 1, centerNode.GetY() - 1));
            if (centerNode.GetY() + 1 >= 0)
                neighbors.Add(GetNode(centerNode.GetX() + 1, centerNode.GetY() + 1));
        }
        if(centerNode.GetY() - 1 >= 0)
            neighbors.Add(GetNode(centerNode.GetX(), centerNode.GetY() - 1));
        if (centerNode.GetY() + 1 < grid.GetGridHeight())
            neighbors.Add(GetNode(centerNode.GetX(), centerNode.GetY() + 1));

        return neighbors;
    }

    private PathfindingNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private List<PathfindingNode> CalculateFinalPath(PathfindingNode endNode)
    {
        List<PathfindingNode> path = new List<PathfindingNode>();
        path.Add(endNode);
        PathfindingNode currentNode = endNode;
        while (currentNode.parentNode != null)
        {
            currentNode = currentNode.parentNode;
            path.Add(currentNode);
        }
        path.Reverse();
        return path;
    }

    private PathfindingNode GetLowestValueNode(List<PathfindingNode> list)
    {
        PathfindingNode lowestFNode = list[0];
        for(int i = 0; i < list.Count; i++)
        {
            if (list[i].fCost < lowestFNode.fCost)
                lowestFNode = list[i];
        }
        return lowestFNode;
    }

    private int GetDistanceValue(PathfindingNode start, PathfindingNode goal)
    {
        int horizontalDisance = Mathf.Abs(start.GetX() - goal.GetX());
        int verticalDisance = Mathf.Abs(start.GetY() - goal.GetY());
        int remaining = Mathf.Abs(horizontalDisance - verticalDisance);
        return DiagonalMovePrice * Mathf.Min(horizontalDisance, verticalDisance) + HorizontalMovePrice * remaining;
    }

    public PathGrid<PathfindingNode> GetGrid()
    {
        return grid;
    }
}
