using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingGrid : MonoBehaviour
{
    private AIPathfiding pathfinding;

    [SerializeField] int x, y;
    [SerializeField] Vector3 origin;
    
    private Vector2 mousePos;
    [SerializeField] private GameObject source;

    [SerializeField] Tilemap collisions;

    void Start()
    {
        pathfinding = new AIPathfiding(x, y, origin);

        foreach(var pos in collisions.cellBounds.allPositionsWithin)
        {
            Vector3Int localArea = new Vector3Int(pos.x, pos.y, pos.z);
            if(collisions.HasTile(localArea))
            {
                pathfinding.GetGrid().GetGridObject(pos).isWalkable = false;
            }
        }
    }
    
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {          
            List<PathfindingNode> path = pathfinding.FindTarget(source.transform.position, mousePos);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].GetX(), path[i].GetY()) + Vector3.one, new Vector3(path[i+1].GetX(), path[i+1].GetY()) + Vector3.one, Color.cyan, 5f);
                }
            }
            
        }
    }
    
}