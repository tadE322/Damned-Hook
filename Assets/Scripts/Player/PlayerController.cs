using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{


    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    public TrailRenderer tr;

    //����������� ���������� � ������ �� ������� ��� �����, ������ ���, ���� ����� ��� � ������, ���������� �� Unit � ������� ���������

    [Header("Player")]
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    public static float speed = 8.0f;
    public static Vector2 direction;
    private Rigidbody2D rb;
    public GameObject player;
    public GameObject shieldEffect;
    public Shield shieldTimer;
    public HealthBar healthBar;

    //� ������ ��� ������ ��������� �����, ���� ������� ��������� ����������, �����, ������� ���� �� ��� � ����� --- �������� ---

    [Header("Crosshair")]
    private Vector2 aim;
    private Vector2 mousePosition;
    public GameObject crossHair;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aim = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        if (aim.magnitude > 0.0f)
        {
            aim.Normalize();
            crossHair.transform.localPosition = aim;
        }

        
    }

    //��� ��� �������, ������ � FixedUpdate
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * direction);
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Potion"))
        {
            ChangeHealth(1);
            SpawnEnemy.nowTheEnemies--;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Shield"))
        {
            if(!shieldEffect.activeInHierarchy)
            {
                shieldEffect.SetActive(true);
                shieldTimer.gameObject.SetActive(true);
                shieldTimer.BGImage.gameObject.SetActive(true);
                shieldTimer.isCooldown = true;
                SpawnEnemy.nowTheEnemies--;
                Destroy(other.gameObject);
            }
            else
            {
                shieldTimer.ResetTimer();
                SpawnEnemy.nowTheEnemies--;
                Destroy(other.gameObject);
            }
        }
    }

    public void ChangeHealth(int healthValue)
    {
        if (!shieldEffect.activeInHierarchy || shieldEffect.activeInHierarchy && healthValue > 0)
        {
            currentHealth += healthValue;
        }
        else if (shieldEffect.activeInHierarchy && healthValue < 0)
        {
            shieldTimer.ReduceTime(healthValue);
        }
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            player.SetActive(false);
        }
    }



    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(direction.x * dashingPower, direction.y * dashingPower);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
