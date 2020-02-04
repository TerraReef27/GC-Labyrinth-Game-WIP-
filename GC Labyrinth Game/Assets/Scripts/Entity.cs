using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region variables
    [SerializeField] private float hp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float blockTime = .5f;
    [SerializeField] private int dodgeTime = 10;

    [SerializeField] private float dodgeForce = 25000f;

    private Rigidbody2D rb = null;

    private DamageHandler dh = null;

    public float MoveSpeed { get { return moveSpeed; } private set { moveSpeed = value; } }
    public float Health { get { return hp; } set { hp = value; } }

    public enum EntityState{ Neutral, Blocking, Dodging, KnockedBack }
    public EntityState currentState = EntityState.Neutral;

    #endregion variables

    void Awake()
    {
        dh = GetComponent<DamageHandler>();
        rb = GetComponent<Rigidbody2D>();
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


    public void Dodge(Vector2 inputPoint)
    {
        //TODO play dodge animation
        //Maybe add raycast to check colliders
        //StartCoroutine(MoveDodge(inputPoint.normalized));
        if(currentState == EntityState.Neutral)
            StartCoroutine(NewDodge(inputPoint.normalized));
        //StartCoroutine(Block(dodgeTime));
    }
    private IEnumerator NewDodge(Vector2 direction)
    {
        currentState = EntityState.Dodging;

        float currentDodgeTime = dodgeTime;
        //float moveDistance = dodgeForce / dodgeFrames;

        rb.velocity = Vector2.zero;

        while(currentDodgeTime >= 1)
        {
            if (direction != Vector2.zero)
                rb.AddForce(direction * dodgeForce * Time.fixedDeltaTime);
                //rb.MovePosition(rb.position + direction * dodgeForce * Time.fixedDeltaTime);
            else
                rb.AddForce(Vector2.down * dodgeForce * Time.fixedDeltaTime);
                //rb.MovePosition(rb.position + Vctor2.down * dodgeForce * Time.fixedDeltaTime);
            currentDodgeTime--;
            yield return new WaitForEndOfFrame();
        }
        currentState = EntityState.Neutral;
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

    public IEnumerator ApplyKnockback(Vector2 knockbackDirection, float knockbackPower)
    {
        Debug.Log("Knocking back");
        currentState = EntityState.KnockedBack;

        float knockbackForce = knockbackPower * 10000;
        Vector2 dir = knockbackDirection.normalized;
        while (knockbackPower >= 1)
        {
            //transform.position += knockbackDirection * knockbackPower * Time.deltaTime;
            rb.AddForce(dir * knockbackForce * Time.fixedDeltaTime);
            knockbackPower -= 1;
            yield return new WaitForEndOfFrame();
        }
        currentState = EntityState.Neutral;
        Debug.Log("Ending knockback");
    }
}
