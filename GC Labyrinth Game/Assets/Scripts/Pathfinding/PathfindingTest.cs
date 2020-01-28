using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTest : MonoBehaviour
{
    private AIPathfiding pathfinding;

    private Vector2 mousePos;
    private Vector2 searchStart = new Vector2(0, 0);

    [SerializeField] private GameObject source;

    void Start()
    {
        pathfinding = new AIPathfiding(20, 20, Vector3.zero);
    }
    
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {          
            List<PathfindingNode> path = pathfinding.FindTarget(source.transform.position, mousePos);
            if (path != null)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    Debug.Log(path[i]);
                    Debug.DrawLine(new Vector3(path[i].GetX(), path[i].GetY()) + Vector3.one, new Vector3(path[i+1].GetX(), path[i+1].GetY()) + Vector3.one, Color.cyan, 5f);
                }
            }
            
        }
    }
}