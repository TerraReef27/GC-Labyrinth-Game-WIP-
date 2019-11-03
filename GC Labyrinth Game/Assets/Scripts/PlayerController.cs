using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Camera cam = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private SpriteRenderer rend = null;

    [SerializeField] private GameObject weapon = null;
    [SerializeField] private GameObject cursor = null;


    [SerializeField] private Entity_Attack attack = null;

    private Vector2 movement;
    private Vector2 mousePos;

    private float angle;

    void Update()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        animator.SetFloat("X_WalkValue", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("Y_WalkValue", Input.GetAxisRaw("Vertical"));

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            rend.flipX = true;
        }
        else
        {
            rend.flipX = false;
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        cursor.transform.position = mousePos;

        if (Input.GetButtonDown("Fire1"))
        {
            attack.Attack();
        }

        GetWeaponSwitchInputs();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        Vector3 facing = mousePos - rb.position;
        angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg - 90f;
        weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
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
