using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] Enemy enemy = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Attack")
        {
            gameObject.GetComponent<Enemy>().Health -= collision.gameObject.GetComponent<Bullet>().Damage;
            Debug.Log("Hit This");
            enemy.Health -= 10;
        }
    }
}
