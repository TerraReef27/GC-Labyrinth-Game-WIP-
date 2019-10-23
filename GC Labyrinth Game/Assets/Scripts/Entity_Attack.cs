using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Attack : MonoBehaviour
{
    [SerializeField] private Weapon weapon = null;

    private Vector3 attackPos;

    public void Attack()
    {
        //get data on weapon attack type
        //initaite the attack based off of previous data
        Weapon.weaponType type = weapon.type;
        
        if(type == Weapon.weaponType.ranged)
        {
            //check ammo
            //shoot if there is ammo
            //reload ammo if 0 left
            //var attack = Instantiate(weapon.Attack);
            //attack.transform.parent = attackCollector.transform;
            weapon.WeaponRangedAttack();
        }
        else if (type == Weapon.weaponType.melee)
        {
            //lose durability
            weapon.WeaponMeleeAttack();
        }
    }

    private void GetWeaponData()
    {
        //get the weapon data and assign it to local variables
    }
}
