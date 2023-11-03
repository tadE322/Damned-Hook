using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float speed;
    [SerializeField] private float timeBtwAttack;
    [SerializeField] private float startTimeBtwAttack;
    [SerializeField] private int damage;
    private float stopTime;
    [SerializeField] private float startStopTime;
    [SerializeField] private float normalSpeed;
    private PlayerController player;
    private Animator anim;
    private SpriteRenderer flip;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        flip = GetComponent<SpriteRenderer>();
        normalSpeed = speed;

    }

    private void Update()
    {
        if (stopTime <= 0 )
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }



        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (player.transform.position.x > transform.position.x)
        {
            flip.flipX = false;
        }
        else
        {
            flip.flipX = true;
        }
    }

    public void TakeDamage(int damage)
    {
        stopTime = startStopTime;
        health -= damage;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0)
            {
                OnEnemyAttack();
                anim.SetTrigger("attack");
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    private void OnEnemyAttack()
    {
        PlayerController.health -= damage;
        timeBtwAttack = startTimeBtwAttack;
    }
}
