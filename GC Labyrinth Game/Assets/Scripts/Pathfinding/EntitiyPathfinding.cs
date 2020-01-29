using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiyPathfinding : MonoBehaviour
{
    private AIPathfiding pathfinding;

    private Vector2 mousePos;

    void Start()
    {
        pathfinding = new AIPathfiding(20, 20, Vector3.zero);
    }


    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            List<PathfindingNode> path = pathfinding.FindTarget(gameObject.transform.position, mousePos);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    //transform.position = path[i];
                    //Debug.DrawLine(new Vector3(path[i].GetX(), path[i].GetY()) + Vector3.one, new Vector3(path[i + 1].GetX(), path[i + 1].GetY()) + Vector3.one, Color.cyan, 5f);
                }
            }

        }
    }
}
