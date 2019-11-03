using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;

    public float damage = 1f;

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
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
        }
    }
}
