using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 30;
    private float lifeTime = 3f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.collider.GetComponent<Player>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (collision.collider.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
            if (collision.collider.CompareTag("Sandbag"))
            {
                Destroy(gameObject);
            }
        }
    }
}
