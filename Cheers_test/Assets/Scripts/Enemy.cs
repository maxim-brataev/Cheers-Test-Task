using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    public GameObject[] deathEffect;
    public float deathEffectLifeTime = 3f;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        GameManager.killscounter ++;
        FindObjectOfType<AudioManager>().PlayRandomDeathSound();
        GameObject effect = (GameObject)Instantiate(deathEffect[Random.Range(0, deathEffect.Length)], transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        Destroy(effect, deathEffectLifeTime);
        Destroy(gameObject);
    }
}