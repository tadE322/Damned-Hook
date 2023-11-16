using JetBrains.Annotations;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float speed;
    public int damage;
    public int health;
    public GameObject player;


    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }

        
    }

    public virtual void Die()
    {

    }

}
