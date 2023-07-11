using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool medkit = false;
    public int addedHP = 20;
    public bool ammobox = false;
    public int addedAmmo = 30;

   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }
    
    void Pickup(Collider2D player)
    {
        if (medkit)
        {
            Player pl = player.GetComponent<Player>();
            if (pl.health == pl.maxHealth)
            {
                return;
            }
            FindObjectOfType<AudioManager>().Play("medkit");
            pl.AddHP(addedHP);
        }
        if (ammobox)
        {
            PlayerShooting ps = player.GetComponent<PlayerShooting>();
            if (ps.currentAmmo == ps.maxAmmo)
            {
                return;
            }
            FindObjectOfType<AudioManager>().Play("ammobox");
            ps.AddAmmo(addedAmmo);
        }
        
        Destroy(gameObject);
    }
}