using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private Animator anim;
    private PlayerController player;
   // [SerializeField] private GameObject gun;

    private void Start()
    { 
        player = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (PlayerController.direction.x != 0 || PlayerController.direction.y != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
       // if (Input.GetKey(KeyCode.Q))
       // {
           // gun.SetActive(true);
       //     anim.SetBool("isEquipped", true);
      //  }
        anim.SetFloat("Horizontal", Camera.main.ScreenToWorldPoint(Input.mousePosition).x - player.transform.position.x);
        anim.SetFloat("Vertical", Camera.main.ScreenToWorldPoint(Input.mousePosition).y - player.transform.position.y);
    }

}
