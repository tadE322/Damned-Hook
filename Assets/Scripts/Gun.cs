using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;
    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;
    public Transform gun;
    [SerializeField] private float scaleValue;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetFloat("Horizontal", PlayerController.direction.x);
        anim.SetFloat("Vertical", PlayerController.direction.y);
        if (PlayerController.direction.x != 0 || PlayerController.direction.y != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if (PlayerController.direction.x == 1 && PlayerController.direction.y == 0) // вправо
        {
            offset = 0;
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
            {
                gun.localScale = new Vector3(scaleValue, scaleValue * -1, scaleValue);
            }
            else
            {
                gun.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
            }
        }
        else if (PlayerController.direction.x == -1 && PlayerController.direction.y == 0) // влево
        {
            offset = 180;
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
            {
                gun.localScale = new Vector3(scaleValue, scaleValue * -1, scaleValue);
            }
            else
            {
                gun.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
            }
        }
        else if (PlayerController.direction.x == 0 && PlayerController.direction.y == 1) // вверх
        {
            if (gun.localScale.y > 0)
            {
                offset = -90;
            } 
            else
            {
                offset = 90;
            }
            
        }
        else if ((PlayerController.direction.x == 0 && PlayerController.direction.y == -1) || (PlayerController.direction.x == 0 && PlayerController.direction.y == 0)) // вниз
        {
            if (gun.localScale.y > 0)
            {
                offset = 90;
            }
            else
            {
                offset = -90;
            }
        }

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
