using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public float deathEffectLifeTime = 10;
    public GameObject deathEffect;
    public PlayerUI uI;

    void Start()
    {
        health = maxHealth;
        uI.SetMaxHealth(maxHealth);
    }
    public void AddHP(int amount)
    {
        health += amount;

        uI.SetHealth(health);

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        uI.SetHealth(health);
        
        if (health <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        FindObjectOfType<AudioManager>().Play("death1");
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        Destroy(effect, deathEffectLifeTime);
        Destroy(gameObject);
    }
}