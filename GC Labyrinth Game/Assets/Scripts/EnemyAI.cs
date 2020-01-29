﻿using System.Collections;
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
    [Tooltip("How far away the AI stops from the target")]
    [SerializeField] private float stopDistance = 3f;
    [Tooltip("The weapon the AI uses")]
    [SerializeField] GameObject weapon = null;

    private List<Vector3> path;
    private int currentPathIndex;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        entity = GetComponent<Entity>();

        if(GetComponent<Entity_Attack>() != null)
            attack = GetComponent<Entity_Attack>();
    }

    void Start()
    {
        currentPathIndex = 0;

        if(target == null)
            ChangeTarget();
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
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (target != null)
        {
            Vector2 targetDifference = target.transform.position - transform.position;
            float sign = (target.transform.position.y < transform.position.y) ? -1f : 1f;
            angle = Vector2.Angle(Vector2.right, targetDifference) * sign - 90f;
            weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            /*
            if (Vector2.Distance(transform.position, target.transform.position) > stopDistance)     //change to state based programming
            {
                rb.MovePosition(rb.position + movementDir * entity.MoveSpeed * Time.fixedDeltaTime);
            }
            else if (attack != null)
            {
                attack.Attack();
            }
            */
        }
        FindTarget();
    }

    private void FindTarget()
    {
        path = AIPathfiding.instance.FindPath(gameObject.transform.position, target.transform.position);
        FollowPath(path);
    }

    private void FollowPath(List<Vector3> path)
    {
        if(path != null)
        {
            Vector3 moveTo = path[currentPathIndex];
            if(Vector3.Distance(transform.position, moveTo) > .1f && Vector2.Distance(transform.position, target.transform.position) > stopDistance)
            {
                Vector3 moveDir = (moveTo - transform.position).normalized;
                rb.MovePosition(rb.position + movementDir * entity.MoveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                currentPathIndex++;
                if(currentPathIndex >= path.Count)
                {
                    attack.Attack();
                }
            }
        }
        else
        {
            path = null;
            attack.Attack();
        }
    }
    
    private void ChangeTarget(GameObject newTarget)
    {
        target = newTarget;
    }
    private void ChangeTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
}