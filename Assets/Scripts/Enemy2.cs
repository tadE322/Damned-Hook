using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float AggreDistance;
    [SerializeField] private int damage = 5;
    private Animator anim;
    private SpriteRenderer flip;
    public float attackRange;
    private Rigidbody2D rb;
    public float timeBtwAttack = 2.0f;
    public float startTimeBtwAttack = 2.0f;
    public int health;



    private float distance;


    private void Start()
    {
        anim = GetComponent<Animator>();
        flip = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if (distance < AggreDistance)
        {
            direction = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            rb.MovePosition(direction);
            anim.SetBool("isClose", true);
            if (player.transform.position.x > transform.position.x)
            {
                flip.flipX = false;
            }
            else
            {
                flip.flipX = true;
            }
        }
        else
        {
            anim.SetBool("isClose", false);
        }
        if (distance <= 1.5 && timeBtwAttack <= 0)
        {
            speed = 0;
            anim.SetBool("Attack", true);
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            speed = normalSpeed;
            anim.SetBool("Attack", false);
            timeBtwAttack -= Time.deltaTime;
        }

        if(health <= 0 )
        {
            Destroy(gameObject);
        }
    }


    public void Attack()
    {
        PlayerController.health -= damage;
        timeBtwAttack = startTimeBtwAttack;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}



