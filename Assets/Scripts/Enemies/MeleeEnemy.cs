using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

//�������� �������� �����, �������� ������ ������ ���������� ����������, ������ ������� ��� �����������
public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration = 0.05f; 
    [SerializeField] private float normalSpeed;
    [SerializeField] private float AggreDistance;
    [SerializeField] private int damage = 5;

    [Header("Push")]
    [SerializeField] private float pushPower;
    [SerializeField] private float timeBtwPushesMin = 0.4f;
    [SerializeField] private float timeBtwPyshesMax = 1.2f;
    [SerializeField] private float pushSpread = 0.15f;
    private float pushTimer = 1f;

    [Header("Other")]
    private Animator anim;
    private SpriteRenderer flip;
    public float attackRange; 
    private Rigidbody2D rb;
    public float timeBtwAttack = 2.0f;
    public float startTimeBtwAttack = 2.0f;
    public GameObject coin;

    //������ ������� �������� 3 ���� ������, ��� ������, ���������� ����� �������� Unit � ��� ������ ������� ��������, ������ � ������ ���������� �� Unit,
    //���������� �������� � ������ � � ������ ������ ���� ����������

    //�� ������� �� ��������� ��� ��������������, ����� ������ ���� �������� ��, �� ������� ������ ������ � ��
    public int health;

    //public int Health { get; private set; }       ���� ��� ���, ������ ������, ��� ������ ������������� ����� ���� � UNITY � �����, � ����������, ��� � [serializedfield], ��� ��� ���������������

    private float distance;


    private void Start()
    {
        //���������� ����� �� ������, �� ���� �������� ����������, ������������ ��� � ������ � ���� ��������, ����� ����� ������� ��������
        anim = GetComponent<Animator>();
        flip = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        pushTimer = timeBtwPyshesMax;
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        
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

    }

    private void FixedUpdate()
    {
        //�� ����������� ������������� ���������� �������(RigidBody) � Update(), ������ ������ ����������� � FixedUpdate(), ������� � ��� ������ ��������� ��������
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if (distance < AggreDistance)
        {
            //��� �� �����-�� ������������ ����������� �����, � ��������
            rb.velocity = Vector2.Lerp(rb.velocity, speed * Time.fixedDeltaTime * direction, acceleration * Time.fixedDeltaTime);

            if (pushTimer < 0)
            {
                rb.velocity = (direction.normalized + new Vector2(Random.Range(-pushSpread, pushSpread), Random.Range(-pushSpread, pushSpread))) * pushPower;
                pushTimer = Random.Range(timeBtwPushesMin, timeBtwPyshesMax);
            }
            pushTimer -= Time.fixedDeltaTime;

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
            rb.velocity = Vector2.zero;
        }
    }

    public void Attack()
    {
        //������� �������� �� ��������� ��� ����� �� ������, � �� �����
        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            player.GetComponent<PlayerController>().ChangeHealth(-damage);
            timeBtwAttack = startTimeBtwAttack;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{gameObject.name} �������� {damage} �����", gameObject);
        health -= damage;

        //����� ��������� �� � �������, ���������� �����  ��������� �����, ��� ����� �� �� � ������ TakeDamage() --- �������� ---
        if (health <= 0)
        {
            Destroy(gameObject);
            coin.GetComponent<Coin>().CoinDrop(transform.position);
            SpawnEnemy.nowTheEnemies--;
        }
    }
}



