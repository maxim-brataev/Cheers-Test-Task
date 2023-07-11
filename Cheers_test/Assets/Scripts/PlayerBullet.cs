using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 40;
    private float lifeTime = 3f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                collision.collider.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (collision.collider.CompareTag("Sandbag"))
            {
                Destroy(gameObject);
            }
        }
    }
}
