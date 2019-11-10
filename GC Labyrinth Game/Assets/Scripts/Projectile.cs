using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;

    private float damage = 1f;
    public bool isPlayerProjectile = false;
    public float ProjectileDamage { get { return damage; } set { damage = value; } }

    void Start()
    {
        StartCoroutine(Despawn());
    }

    public void Shoot(Vector2 rotation, float speed)
    {
        rb.AddForce(rotation * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isPlayerProjectile)
        {
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Terrain")
            {
                Destroy(gameObject);            }
        }
        else
        {
            if (collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetIsPlayer(bool isPlayer)
    {
        isPlayerProjectile = isPlayer;
    }
}
