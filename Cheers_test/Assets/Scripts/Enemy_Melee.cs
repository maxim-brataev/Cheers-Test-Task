using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee : MonoBehaviour
{
    public int damage = 10;
    public float attackRate = 1.5f;
    public float moveSpeed = 2f;
    public float huntingDistance = 100f;

    private float attackDistance = 1f;
    private float distanceToPlayer;
    private float fireCountdown = 0f;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = Mathf.Infinity;
        rb.drag = Mathf.Infinity;
        rb.angularDrag = Mathf.Infinity;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            fireCountdown -= Time.fixedDeltaTime;

            distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > huntingDistance)
                animator.SetTrigger("Idle");

            if (distanceToPlayer <= huntingDistance && distanceToPlayer > attackDistance)
            {
                animator.SetTrigger("Move");

                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.fixedDeltaTime);
                Vector2 lookDir = (Vector2)player.position - rb.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
            }
            if (distanceToPlayer <= attackDistance)
            {
                animator.SetTrigger("Attack");
                if (fireCountdown <= 0f)
                {
                    FindObjectOfType<AudioManager>().Play("zombie_attack");
                    player.GetComponent<Player>().TakeDamage(damage);
                    fireCountdown = 1f / attackRate;
                }
            }

        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }
}