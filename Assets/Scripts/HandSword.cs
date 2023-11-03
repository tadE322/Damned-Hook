using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSword : MonoBehaviour
{
    private Animator anim;
    private float timeBtwAttack;

    public Transform attackPos;
    public GameObject player;
    public LayerMask whatIsSolid;
    public float startTimeBtwAttack;
    public float attackRange;
    public int damage;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                anim.SetBool("isAttacking", true);
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                anim.SetBool("isAttacking", false);
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        anim.SetFloat("Horizontal", Camera.main.ScreenToWorldPoint(Input.mousePosition).x - player.transform.position.x);
        anim.SetFloat("Vertical", Camera.main.ScreenToWorldPoint(Input.mousePosition).y - player.transform.position.y);

    }

    public void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsSolid);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy2>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
