using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AI
{
    void FollowTarget(GameObject target);
    void ChangeTarget(GameObject newTarget);
}
