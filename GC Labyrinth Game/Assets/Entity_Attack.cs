using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Attack : MonoBehaviour
{
    [SerializeField] private Weapon weapon = null;
    [SerializeField] private GameObject attackCollector = null;

    private Vector3 attackPos;

    private void Update()
    {
        attackPos = new Vector3(transform.position.x, transform.position.y - weapon.HitboxY / 2);
    }

    public void Attack(Vector3 attackDirection)
    {
        //get data on weapon attack type
        //initaite the attack based off of previous data
        Weapon.weaponType type = weapon.type;
        
        if(type == Weapon.weaponType.gun)
        {
            //check ammo
            //shoot if there is ammo
            //reload ammo if 0 left
            //var attack = Instantiate(weapon.Attack);
            //attack.transform.parent = attackCollector.transform;
        }
        else if (type == Weapon.weaponType.sword)
        {
            //sword swipe
            //lose durability
            Debug.Log("Swiping Sword");


            Collider2D[] damagedEnemies = Physics2D.OverlapBoxAll(transform.position, new Vector2(weapon.HitboxX, weapon.HitboxY), 0);
            for(int i = 0; i < damagedEnemies.Length; i++)
            {
                damagedEnemies[i].GetComponent<Enemy>().TakeDamage(weapon.Damage);
            }
        }
    }

    private void GetWeaponData()
    {
        //get the weapon data and assign it to local variables
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos, new Vector3(weapon.HitboxX, weapon.HitboxY));
    }
}
