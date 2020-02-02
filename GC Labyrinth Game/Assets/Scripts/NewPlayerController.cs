using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    private Vector3 movement;

    private Entity selfEntity;
    private Entity_Attack attack = null;

    [SerializeField] private GameObject weapon = null;

    private void Awake()
    {
        selfEntity = GetComponent<Entity>();
        attack = GetComponent<Entity_Attack>();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(selfEntity.currentState == Entity.EntityState.Neutral)
            transform.position += Vector3.ClampMagnitude(movement, selfEntity.MoveSpeed) * selfEntity.MoveSpeed * Time.deltaTime;

        if(Input.GetButtonDown("Fire2"))
        {
            selfEntity.Dodge(movement);
        }
    }

    private void GetWeaponSwitchInputs()
    {
        if (Input.GetButtonDown("Switch Weapons"))
        {
            attack.SwitchWeapons();
            weapon = attack.ActiveWeapon;
        }
        else if (Input.GetKeyDown("1"))
        {
            attack.SwitchWeapons(0);
            weapon = attack.ActiveWeapon;
        }
        else if (Input.GetKeyDown("2"))
        {
            attack.SwitchWeapons(1);
            weapon = attack.ActiveWeapon;
        }
        else if (Input.GetKeyDown("3"))
        {
            attack.SwitchWeapons(2);
            weapon = attack.ActiveWeapon;
        }
    }
}
