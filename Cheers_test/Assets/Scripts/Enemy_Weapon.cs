using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weapon : MonoBehaviour
{
    public float moveSpeed = 2f;

    public float fireRate = 2f;
    public float fireDistance = 7f;
    
    public Transform firePoint;
    public GameObject muzzleFlashEffect;
    public GameObject bulletPrefab;
    public float bulletForce = 12;
    public int maxClip = 7;

    private int currentClip;
    private bool isReloading = false;
    private float distanceToPlayer;
    private float fireCountdown = 0f;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;

    void Start()
    {
        currentClip = maxClip;
        isReloading = false;
        rb = GetComponent<Rigidbody2D>();
        rb.mass = Mathf.Infinity;
        rb.drag = Mathf.Infinity;
        rb.angularDrag = Mathf.Infinity;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator.SetTrigger("Idle");
    }
    void FixedUpdate()
    {
        if (player != null)
        {
            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            Vector2 lookDir = (Vector2)player.position - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;

            if (distanceToPlayer > fireDistance)
            {
                animator.SetTrigger("Move");
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.fixedDeltaTime);
            }

            fireCountdown -= Time.fixedDeltaTime;

            if (distanceToPlayer <= fireDistance)
            {
                if (fireCountdown <= 0f)
                {
                    if (currentClip > 0 && !isReloading)
                    {
                        Shoot();
                        fireCountdown = 1f / fireRate;
                        currentClip--;
                    }
                }
                if (currentClip == 0 && !isReloading)
                {
                    StartCoroutine(Reload());
                }
            }
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }
    void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("gun_shoot");
        animator.SetTrigger("Shoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();
        bullet_rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        GameObject fire_fx = Instantiate(muzzleFlashEffect, firePoint.position, firePoint.rotation);
        Destroy(fire_fx, 0.075f);
    }
    IEnumerator Reload()
    {
        FindObjectOfType<AudioManager>().Play("gun_reload");
        isReloading = true;
        animator.SetTrigger("Reload");
        WaitForSeconds wait = new WaitForSeconds(2f);
        yield return wait;
        currentClip = maxClip;
        isReloading = false;
    }
}