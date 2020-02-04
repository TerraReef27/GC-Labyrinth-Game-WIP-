using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private Rigidbody2D rb = null;
    private Animator animator = null;
    private SpriteRenderer rend = null;

    [SerializeField] private Camera cam = null;
    [SerializeField] private GameObject weapon = null;
    [SerializeField] private GameObject cursor = null;


    private Entity_Attack attack = null;
    private Entity selfEntity = null;
    
    private Vector2 movement;
    private Vector2 mousePos;

    private float angle;


    private void Awake()
    {
        selfEntity = GetComponent<Entity>();
        attack = GetComponent<Entity_Attack>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //POSSIBLY CHANGE LATER ONCE DODGE ANIMATION IS ADDED IN
        if (selfEntity.currentState == Entity.EntityState.Neutral)
        {
            animator.SetFloat("X_WalkValue", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("Y_WalkValue", Input.GetAxisRaw("Vertical"));

            if (Input.GetButtonDown("Fire1"))
            {
                attack.Attack();
            }

            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                rend.flipX = true;
            }
            else
            {
                rend.flipX = false;
            }
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        cursor.transform.position = mousePos;
        
        if(Input.GetButtonDown("Fire2"))
        {
            /*
             * For dodgeing in the mouse direction
             * selfEntity.Dodge((mousePos - new Vector2(transform.position.x, transform.position.y)).normalized);
             */
            selfEntity.Dodge(movement);
        }
        if (Input.GetButtonDown("Block"))
        {
            StartCoroutine(selfEntity.Block());
        }
        GetWeaponSwitchInputs();
    }

    void FixedUpdate()
    {
        if (selfEntity.currentState == Entity.EntityState.Neutral)
            rb.MovePosition(rb.position + Vector2.ClampMagnitude(movement, selfEntity.MoveSpeed) * selfEntity.MoveSpeed * Time.fixedDeltaTime);

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
