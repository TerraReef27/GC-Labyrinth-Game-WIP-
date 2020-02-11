using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Entity selfEntitiy = null;
    private Rigidbody2D rb = null;
    private Entity_Attack attack = null;
    [Tooltip("What the AI goes towords or away from. Leave empty to target player")]
    [SerializeField] private GameObject target = null;

    private Vector2 movementDir;
    private float angle;
    [Tooltip("How far away the AI stops from the target")]
    [SerializeField] private float stopDistance = 3f;
    [Tooltip("The weapon the AI uses")]
    [SerializeField] GameObject weapon = null;

    private List<Vector3> path;
    private int currentPathIndex;

    private List<PathfindingNode> underPath;

    void Awake()
    {
        selfEntitiy = GetComponent<Entity>();
        rb = GetComponent<Rigidbody2D>();
        if(GetComponent<Entity_Attack>() != null)
            attack = GetComponent<Entity_Attack>();
    }

    void Start()
    {
        //currentPathIndex = 0;

        if(target == null)
            ChangeTarget();
    }


    void Update()
    {
        if(target != null)
        {
            TrackTarget();
            //movementDir = target.transform.position - transform.position;
            //movementDir.Normalize();
            SetTargetPosition(target.transform.position);
        }
    }

    void FixedUpdate()
    {
         FollowTarget();
    }

    private void TrackTarget()
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
    }

    private void FollowTarget()
    {
        if(target != null)
        {
            //Moved to Update for preformance   path = AIPathfiding.instance.FindPath(gameObject.transform.position, target.transform.position);
            FollowPath(path);

            //FOR DEBUG
            underPath = AIPathfiding.instance.FindTarget(gameObject.transform.position, target.transform.position);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(underPath[i].GetX(), underPath[i].GetY()) + Vector3.one, new Vector3(underPath[i + 1].GetX(), underPath[i + 1].GetY()) + Vector3.one, Color.cyan);
                }
            }
        }
    }

    public Vector3 moveTo;
    private void FollowPath(List<Vector3> path)
    {
        if (path != null)
        {
            //Vector3 moveTo = path[currentPathIndex];
            moveTo = path[currentPathIndex];
            if (Vector3.Distance(transform.position, moveTo) > .1f && Vector2.Distance(transform.position, target.transform.position) > stopDistance)
            {
                Vector2 moveDir = (moveTo - transform.position).normalized;
                //Debug.Log(moveDir);
                //Debug.Log("Path is: " + path[currentPathIndex]);
                //Debug.Log("Mocement Direction: " + moveDir);
                //Debug.Break();
                //transform.position += Vector3.ClampMagnitude(moveDir, selfEntitiy.MoveSpeed) * selfEntitiy.MoveSpeed * Time.deltaTime;
                rb.AddForce(moveDir * selfEntitiy.ForceSpeed);
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
            //path = null;
            //attack.Attack();
        }
    }
    
    public void SetTargetPosition(Vector3 target)
    {
        currentPathIndex = 0;
        path = AIPathfiding.instance.FindPath(transform.position, target);

        if(path != null && path.Count > 1)
        {
            path.RemoveAt(0);
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