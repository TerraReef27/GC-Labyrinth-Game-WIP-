using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 50f;

    [SerializeField] Rigidbody2D rb = null;
    [SerializeField] Transform weapon = null;

    void Start()
    {
        rb.AddForce(weapon.up * bulletSpeed, ForceMode2D.Impulse);

        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
