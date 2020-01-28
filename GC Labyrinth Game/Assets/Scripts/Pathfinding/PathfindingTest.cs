using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTest : MonoBehaviour
{
    private AIPathfiding pathfinding;

    private Vector2 mousePos;
    private Vector2 searchStart = new Vector2(5, 5);

    void Start()
    {
        pathfinding = new AIPathfiding(20, 20, Vector3.zero);
    }
    
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            //List<PathfindingNode> path = pathfinding.FindTarget(Vector2.zero, pathfinding.GetGrid().GetGridPos(mousePos));
            //Debug.Log(pathfinding.GetGrid().GetGridPos(mousePos));
            //pathfinding.GetGrid().GetGridObject(mousePos);
            //print(pathfinding.GetGrid().GetGridObject(mousePos));
            
            
            List<PathfindingNode> path = pathfinding.FindTarget(searchStart, mousePos);
            if (path != null)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].GetX(), path[i].GetY(), 0) * 10f + Vector3.one * 5f, new Vector3(path[i].GetX() + 1, path[i].GetY() + 1, 0) * 10f + Vector3.one * 5f, Color.cyan);
                }
            }
            
        }
    }
}
