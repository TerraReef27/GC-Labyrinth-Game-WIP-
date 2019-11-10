using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Attack : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();

    [SerializeField] private Weapon activeWeapon = null;
    [SerializeField] private int activeWeaponNum;

    private Vector3 attackPos;
    //private bool isPlayerWeapon = false;
    public GameObject ActiveWeapon { get { return activeWeapon.gameObject; } private set { } }

    private void Start()
    {
        activeWeapon = weapons[0];
        activeWeaponNum = 0;
        activeWeapon.gameObject.SetActive(true);

        //if(transform.parent.gameObject.tag == "Player")
            //isPlayerWeapon = true;
    }

    public void Attack()
    {
        Weapon.weaponType type = activeWeapon.type;
        
        if(type == Weapon.weaponType.ranged)
        {
            //check ammo
            //shoot if there is ammo
            //reload ammo if 0 left
            //var attack = Instantiate(weapon.Attack);
            //attack.transform.parent = attackCollector.transform;
            activeWeapon.WeaponRangedAttack();
        }
        else if (type == Weapon.weaponType.melee)
        {
            activeWeapon.WeaponMeleeAttack();
        }
    }

    public void SwitchWeapons()
    {
        if ((activeWeaponNum + 1) < weapons.Capacity)
            activeWeaponNum++;
        else
            activeWeaponNum = 0;

        activeWeapon.gameObject.SetActive(false);
        activeWeapon = weapons[activeWeaponNum];
        activeWeapon.gameObject.SetActive(true);
    }

    public void SwitchWeapons(int weaponToSwitchTo)
    {
        if (weaponToSwitchTo < weapons.Capacity && weapons[weaponToSwitchTo] != null)
        {
            activeWeapon.gameObject.SetActive(false);
            activeWeapon = weapons[weaponToSwitchTo];
            activeWeaponNum = weaponToSwitchTo;
            activeWeapon.gameObject.SetActive(true);
        }
    }
}
