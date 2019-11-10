using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] Entity entity = null;
    private bool isPlayer = false;

    private void Start()
    {
        if (transform.gameObject.tag == "Player")
            isPlayer = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isPlayer)
        {
            if (collision.gameObject.tag == "Attack")
            {
                entity.TakeDamage(collision.gameObject.GetComponent<Weapon>().Damage);
            }
            else if (collision.gameObject.tag == "Projectile" && collision.gameObject.GetComponent<Projectile>().isPlayerProjectile == false)
            {
                entity.TakeDamage(collision.gameObject.GetComponent<Projectile>().ProjectileDamage);
            }
        }
        else
        {
            if (collision.gameObject.tag == "Attack")
            {
                entity.TakeDamage(collision.gameObject.GetComponent<Weapon>().Damage);
            }
            else if (collision.gameObject.tag == "Projectile" && collision.gameObject.GetComponent<Projectile>().isPlayerProjectile == true)
            {
                entity.TakeDamage(collision.gameObject.GetComponent<Projectile>().ProjectileDamage);
            }
        }
    }
}
