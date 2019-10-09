using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Attack : MonoBehaviour
{
    [SerializeField] private Weapon weapon = null;
    [SerializeField] private GameObject attackCollector = null;

    public void Attack(Weapon.weaponType type)
    {
        //get data on weapon attack type
        //initaite the attack based off of previous data
        type = weapon.type;
        
        if(type == Weapon.weaponType.gun)
        {
            //check ammo
            //shoot if there is ammo
            //reload ammo if 0 left
            var attack = Instantiate(weapon.Attack, weapon.transform);
            attack.transform.parent = attackCollector.transform;
        }
        else if (type == Weapon.weaponType.sword)
        {
            //sword swipe
            //lose durability
        }
    }
}
