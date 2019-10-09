using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Attack : MonoBehaviour
{
    [SerializeField] private Weapon weapon = null;
    [SerializeField] private GameObject attackCollector = null;

    public void Attack()
    {
        //get data on weapon attack type
        //initaite the attack based off of previous data

        var attack = Instantiate(weapon.Attack, weapon.transform);
        attack.transform.parent = attackCollector.transform;
    }
}
