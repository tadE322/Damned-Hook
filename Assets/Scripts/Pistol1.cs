using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol1 : MonoBehaviour
{
    private GameObject player;
    private Vector2 mousePosition;
    private float timeBtwShoot;


    public GameObject bulletPrefab;
    public Transform shotPoint;
    public float startTimeBtwShoot;

    


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Shoot();
    }
    public void Shoot()
    {
        if (timeBtwShoot <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 shootingDirection = new(mousePosition.x - player.transform.position.x, mousePosition.y - player.transform.position.y);
                shootingDirection.Normalize();
                shootingDirection *= 15.0f;
                GameObject arrow = Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection;
                timeBtwShoot = startTimeBtwShoot;
            }
        }
        else
        { 
            timeBtwShoot -= Time.deltaTime;
        }
    }

}
