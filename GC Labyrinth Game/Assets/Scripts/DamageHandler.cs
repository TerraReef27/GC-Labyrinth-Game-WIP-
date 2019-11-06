using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] Entity entity = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Attack")
        {
            entity.TakeDamage(collision.gameObject.GetComponent<Weapon>().Damage);
        }
        else if(collision.gameObject.tag == "Projectile")
        {
            entity.TakeDamage(collision.gameObject.GetComponent<Projectile>().ProjectileDamage);
        }
    }
}
