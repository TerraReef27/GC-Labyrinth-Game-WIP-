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
        if (entity.currentState != Entity.EntityState.Blocking || entity.currentState != Entity.EntityState.Dodging)
        {
            if (isPlayer)
            {
                if (collision.gameObject.tag == "Attack")
                {
                    PhysicalDamage(collision.gameObject);
                }
                else if (collision.gameObject.tag == "Projectile" && collision.gameObject.GetComponent<Projectile>().isPlayerProjectile == false)
                {
                    ProjectileDamage(collision.gameObject);
                }
            }
            else
            {
                if (collision.gameObject.tag == "Attack" && collision.gameObject.GetComponentInParent<Entity_Attack>().isPlayerWeapon == true)
                {
                    PhysicalDamage(collision.gameObject);
                }
                else if (collision.gameObject.tag == "Projectile" && collision.gameObject.GetComponent<Projectile>().isPlayerProjectile == true)
                {
                    ProjectileDamage(collision.gameObject);
                }
            }
        }
    }

    private void ProjectileDamage(GameObject collisionObject)
    {
        entity.TakeDamage(collisionObject.GetComponent<Projectile>().ProjectileDamage);
        StartCoroutine(entity.ApplyKnockback(transform.position - collisionObject.transform.position, collisionObject.GetComponent<Projectile>().ProjectileKnockback));
    }
    private void PhysicalDamage(GameObject collisionObject)
    {
        entity.TakeDamage(collisionObject.GetComponent<Weapon>().Damage);
        StartCoroutine(entity.ApplyKnockback(transform.position - collisionObject.transform.position, collisionObject.GetComponent<Weapon>().Knockback));
    }
}
