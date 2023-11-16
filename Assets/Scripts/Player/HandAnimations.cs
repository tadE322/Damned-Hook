using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimations : MonoBehaviour
{
    private Animator anim;
    public GameObject player;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (PlayerController.direction.x != 0 || PlayerController.direction.y != 0)
        {
            anim.SetBool("isRunning", true);
        }

        anim.SetFloat("Horizontal", player.transform.position.x - transform.position.x);
        anim.SetFloat("Vertical", Camera.main.ScreenToWorldPoint(Input.mousePosition).y - player.transform.position.y);
    }

}
