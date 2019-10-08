using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera cam;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer rend;

    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject cursor;

    Vector2 movement;
    Vector2 mousePos;
    
    void Update()
    {
        /* VECTOR MOVEMENT
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        transform.position += movement * speed * Time.deltaTime;
        */

        movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        animator.SetFloat("X_WalkValue", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("Y_WalkValue", Input.GetAxisRaw("Vertical"));
        
        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            rend.flipX = true;
        }
        else
        {
            rend.flipX = false;
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        cursor.transform.position = mousePos;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        Vector2 facing = mousePos - rb.position;
        float angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg - 90f;
        weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
