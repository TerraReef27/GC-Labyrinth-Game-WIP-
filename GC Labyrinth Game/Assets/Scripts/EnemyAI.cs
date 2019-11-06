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

    [SerializeField] GameObject weapon = null;

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
        if(target != null)
        {
            movement = target.transform.position - transform.position;
            movement.Normalize();
        }
    }

    void FixedUpdate()
    {
        if(target != null)
        {
            Vector2 targetDifference = target.transform.position - transform.position;
            float sign = (target.transform.position.y < transform.position.y) ? -1f : 1f;
            angle = Vector2.Angle(Vector2.right, targetDifference) * sign - 90f;
            weapon.transform.rotation = Quaternion.Euler(0, 0, angle);

            if (Vector2.Distance(transform.position, target.transform.position) > stopDistance)     //change to state based programming
            {
                rb.MovePosition(rb.position + movement * entity.MoveSpeed * Time.fixedDeltaTime);
            }
            else if (attack != null)
            {
                attack.Attack();
            }
        }
    }
}
