using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float moveSpeed;

    public float MoveSpeed { get { return moveSpeed; } private set { moveSpeed = value; } }
    public float Health { get { return hp; } set { hp = value; } }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log("took damage");

        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
