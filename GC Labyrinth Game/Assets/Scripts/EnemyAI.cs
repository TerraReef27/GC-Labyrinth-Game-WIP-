using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Entity entity = null;
    private Entity_Attack attack = null;
    [Tooltip("What the AI goes towords or away from. Leave empty to target player")]
    [SerializeField] private GameObject target = null;
    private Rigidbody2D rb = null;

    private Vector2 movementDir;
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
        if(target == null)
            ChagetTarget();
    }


    void Update()
    {
        if(target != null)
        {
            movementDir = target.transform.position - transform.position;
            movementDir.Normalize();
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
                rb.MovePosition(rb.position + movementDir * entity.MoveSpeed * Time.fixedDeltaTime);
            }
            else if (attack != null)
            {
                attack.Attack();
            }
        }
    }

    private void ChangeTarget(GameObject newTarget)
    {
        target = newTarget;
    }
    private void ChagetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
}
