using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 aim;
    private Vector2 mousePosition;
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    public static float speed = 8.0f;
    public static Vector2 direction;
    public static int health = 3;

    public GameObject crossHair;
    public GameObject player;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public TrailRenderer tr;
    public GameObject dashEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        if(isDashing)
        {
            return;
        }

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aim = new Vector2(mousePosition.x  - transform.position.x, mousePosition.y - transform.position.y);
        if (aim.magnitude > 0.0f)
        {
            aim.Normalize();
            crossHair.transform.localPosition = aim;
        }

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if (health <= 0)
        {
            player.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Mathf.RoundToInt(health))
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if ( i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * direction);
    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        Instantiate(dashEffect, transform.position, Quaternion.identity);
        rb.velocity = new Vector2(direction.x * dashingPower, direction.y * dashingPower);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
