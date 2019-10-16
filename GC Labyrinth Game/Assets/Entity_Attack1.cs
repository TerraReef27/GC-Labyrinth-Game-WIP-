using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Attack1 : MonoBehaviour
{
    [SerializeField] private Weapon1 weapon = null;
    [SerializeField] private Transform parentWeapon = null;

    private Vector3 attackPos;

    private void Update()
    {
        attackPos = new Vector3(transform.position.x, transform.position.y - weapon.HitboxY / 2);
    }

    public void Attack(float angleOfAttack)
    {
        //get data on weapon attack type
        //initaite the attack based off of previous data
        Weapon1.weaponType type = weapon.type;
        
        if(type == Weapon1.weaponType.gun)
        {
            //check ammo
            //shoot if there is ammo
            //reload ammo if 0 left
            //var attack = Instantiate(weapon.Attack);
            //attack.transform.parent = attackCollector.transform;
        }
        else if (type == Weapon1.weaponType.sword)
        {
            //sword swipe
            //lose durability
            Debug.Log("Swiping Sword");


            Collider2D[] damagedEnemies = Physics2D.OverlapBoxAll(transform.position, new Vector2(weapon.HitboxX, weapon.HitboxY), parentWeapon.eulerAngles.z);
            for (int i = 0; i < damagedEnemies.Length; i++)
            {
                if(damagedEnemies[i].tag == "Enemy")
                {
                    damagedEnemies[i].GetComponent<Enemy>().TakeDamage(weapon.Damage);
                }
            }
        }
    }

    private void GetWeaponData()
    {
        //get the weapon data and assign it to local variables
    }
}
