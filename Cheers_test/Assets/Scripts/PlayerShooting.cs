using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject muzzleFlashEffect;
    public GameObject bulletPrefab;
    public float bulletForce = 14f;
    public float fireRate = 0.1f;
    public int currentClip, maxClip = 30, currentAmmo, maxAmmo = 150;
    public PlayerUI uI;

    private bool isReloading = false;
    private float fireCountdown = 0f;
    private Animator animator;
    private Transform cam;

    void Start()
    {
        currentClip = maxClip;
        currentAmmo = maxAmmo;
        isReloading = false;
        animator = GetComponent<Animator>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    void Update()
    {
        uI.SetAmmo(currentClip, currentAmmo);
        if (currentClip == 0 && currentAmmo != 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
        if(Input.GetKeyDown(KeyCode.R) && !isReloading && currentClip != maxClip && currentAmmo != 0)
        {
            StartCoroutine(Reload());
        }
        if (Input.GetMouseButton(0) && fireCountdown <= 0 && currentClip > 0 && !isReloading)
        {
                Shoot();
                fireCountdown = fireRate;
                currentClip--;
        }
        if (Input.GetButtonDown("Fire1") && currentClip == 0 && currentAmmo == 0)
        {
            FindObjectOfType<AudioManager>().Play("ar_empty");
        }
        fireCountdown -= Time.deltaTime;
    }
    void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("ar_shoot");
        animator.SetTrigger("Shoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        GameObject fire_fx = Instantiate(muzzleFlashEffect, firePoint.position, firePoint.rotation);
        Destroy(fire_fx, 0.075f);
    }
    IEnumerator Reload ()
    {
        isReloading = true;
        FindObjectOfType<AudioManager>().Play("ar_reload");
        animator.SetTrigger("Reload");
        WaitForSeconds wait = new WaitForSeconds(1.3f);
        yield return wait;
        int reloadAmount = maxClip - currentClip;
        reloadAmount = (currentAmmo - reloadAmount) >= 0 ? reloadAmount : currentAmmo;
        currentClip += reloadAmount;
        currentAmmo -= reloadAmount;
        isReloading = false;
    }
    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        if(currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }
}