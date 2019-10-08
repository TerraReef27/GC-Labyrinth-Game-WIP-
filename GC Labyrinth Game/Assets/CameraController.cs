using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject objectToFollow;

    [SerializeField] private float distanceFromScene = -10;
    
    void Update()
    {
        transform.position = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y, distanceFromScene);
    }
}
