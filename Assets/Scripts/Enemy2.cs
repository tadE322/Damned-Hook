using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

//Название дичайший кринж, название класса должно отображать функционал, номера классов это мегазашквар
public class Enemy2 : MonoBehaviour
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


    private Animator anim;
    private SpriteRenderer flip;
    [Header("Other")]
    public float attackRange; //почему не используется?
    private Rigidbody2D rb;
    public float timeBtwAttack = 2.0f;
    public float startTimeBtwAttack = 2.0f;

    //Пишете систему здоровья 3 раза подряд, так нельзя, создавайте класс например Unit и там пишите систему здоровья, врагов и игрока наследуйте от Unit,
    //функционал здоровья у игрока и у врагов должен быть одинаковый

    //Не делайте ХП публичным для редактиновения, чтобы нельзя было поменять ХП, не проведя логику смерти и пр
    public int health;

    //public int Health { get; private set; }       типа вот так, только учтите, что нельзя редактировать такие поля в UNITY в самой, в инспекторе, как с [serializedfield], так что инициализируйте

    private float distance;


    private void Start()
    {
        //Старайтесь такое не делать, не надо получать компоненты, сериализуйте как в делали в гейм обжектом, сцена будет быстрее грузится
        anim = GetComponent<Animator>();
        flip = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

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

        //зачем проверять ХП в апдейте, проверяйте после  получения урона, для этого же вы и писали TakeDamage()
        if(health <= 0 )
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        //Вы занимаетесь перемещениями ФИЗИЧЕСКИХ обьеков(RigidBody) в Update(), физика должна обновляться в FixedUpdate(), поэтому у вас камера дергается ПОПРАВИЛ
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if (distance < AggreDistance)
        {
            //Тут ты зачем-то пересчитывал направление врага, я поправил
            rb.velocity = Vector2.Lerp(rb.velocity, direction * speed * Time.fixedDeltaTime, acceleration * Time.fixedDeltaTime);

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
        }
    }

    public void Attack()
    {
        //Добавил проверку на дистанцию для атаки по игроку, а то кринж
        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            PlayerController.health -= damage;
            timeBtwAttack = startTimeBtwAttack;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{gameObject.name} получает {damage} урона", gameObject);
        health -= damage;
    }
}



