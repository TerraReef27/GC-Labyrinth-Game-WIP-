using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    private Vector2 movement;

    private Entity selfEntity;
    private Entity_Attack attack = null;
    private Rigidbody2D rb = null;

    [SerializeField] private GameObject weapon = null;

    private void Awake()
    {
        selfEntity = GetComponent<Entity>();
        attack = GetComponent<Entity_Attack>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        GetWeaponSwitchInputs();
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Fire2"))
        {
            selfEntity.Dodge(movement);
        }
    }

    private void FixedUpdate()
    {
        if (selfEntity.currentState == Entity.EntityState.Neutral)
            rb.MovePosition(rb.position + Vector2.ClampMagnitude(movement, selfEntity.MoveSpeed) * selfEntity.MoveSpeed * Time.fixedDeltaTime);
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
