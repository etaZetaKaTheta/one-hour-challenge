using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveForce;

    public static event Action EnemyHit;

    private void OnEnable()
    {
        Buffer();
    }

    private void Buffer()
    {
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle()
    {
        rb.AddForce(new Vector2(0.0f, moveForce), ForceMode2D.Impulse);
        yield return new WaitForSeconds(2.0f);
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0.0f, -moveForce), ForceMode2D.Impulse);
        yield return new WaitForSeconds(2.0f);
        rb.velocity = Vector2.zero;
        Buffer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            EnemyHit?.Invoke();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject, 1.5f);
        }
    }
}
