using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Entity entity = null;
    private Entity_Attack attack = null;
    private GameObject target = null;
    private Rigidbody2D rb = null;

    private Vector2 movement;
    private float angle;
    [SerializeField] private float stopDistance = 3f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        entity = GetComponent<Entity>();

        if(GetComponent<Entity_Attack>() != null)
            attack = GetComponent<Entity_Attack>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        movement = target.transform.position - transform.position;
        movement.Normalize();
    }

    void FixedUpdate()
    {
        //Vector3 facing = mousePos - rb.position;
        angle = Mathf.Atan2(target.transform.position.y, target.transform.position.x) * Mathf.Rad2Deg;
        //weapon.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Vector2.Distance(transform.position, target.transform.position) > stopDistance)
        {
            rb.MovePosition(rb.position + movement * entity.MoveSpeed * Time.fixedDeltaTime);
        }
        else if(attack != null)
        {
            //attack player
            //attack.Attack();
        }
    }
}
