using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region variables
    [SerializeField] private float hp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float blockTime = .5f;
    [SerializeField] private float dodgeTime = 10f;

    private DamageHandler dh = null;

    public float MoveSpeed { get { return moveSpeed; } private set { moveSpeed = value; } }
    public float Health { get { return hp; } set { hp = value; } }

    public enum EntityState{ Neutral, Blocking, Dodging }
    public EntityState currentState = EntityState.Neutral;

    #endregion variables

    void Awake()
    {
        dh = GetComponent<DamageHandler>();
    }

    void Start()
    {
        currentState = EntityState.Neutral;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log("took damage");

        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Block(float blockPeriod)
    {
        currentState = EntityState.Blocking;
        yield return new WaitForSeconds(blockPeriod);
        currentState = EntityState.Neutral;
    }
    public IEnumerator Block()
    {
        currentState = EntityState.Blocking;
        yield return new WaitForSeconds(blockTime);
        currentState = EntityState.Neutral;
    }


    public void Dodge(Vector3 inputPoint)
    {
        //TODO play dodge animation
        //Maybe add raycast to check colliders
        StartCoroutine(MoveDodge(inputPoint.normalized));
        StartCoroutine(Block(dodgeTime));
    }
    private IEnumerator MoveDodge(Vector3 dodgeDirection)
    {
        currentState = EntityState.Dodging;
        float currentDodgeTime = dodgeTime;
        
        while(currentDodgeTime >= 1)
        {
            if(dodgeDirection != Vector3.zero)
                transform.position += dodgeDirection * currentDodgeTime * Time.deltaTime;
            else
                transform.position += Vector3.down * currentDodgeTime * Time.deltaTime;
            currentDodgeTime -= 1;
            yield return new WaitForEndOfFrame();
        }
        currentState = EntityState.Neutral;
    }

    public IEnumerator ApplyKnockback(Vector3 knockbackDirection, float knockbackPower)
    {
        Debug.Log("Knocking back");
        while (knockbackPower >= 1)
        {
            transform.position += knockbackDirection * knockbackPower * Time.deltaTime;
            knockbackPower -= 1;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Ending knockback");
    }
}
