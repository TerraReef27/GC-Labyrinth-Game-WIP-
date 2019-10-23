using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float hp;

    [SerializeField] public float MoveSpeed { get; set; }
    public float Health { get { return hp; } set { hp = value; } }


    //public float health = 100f;
    [SerializeField] private float moveSpeed;

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
