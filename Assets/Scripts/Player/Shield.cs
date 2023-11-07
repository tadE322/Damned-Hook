using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public float cooldown;
    [HideInInspector] public bool isCooldown;
    private Image shieldImage;
    public Image BGImage;
    private PlayerController player;

    void Start()
    {
        shieldImage = GetComponent<Image>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        isCooldown = true;
    }

    void Update()
    {
        shieldImage.fillAmount -= 1 / cooldown * Time.deltaTime;
        if(shieldImage.fillAmount <= 0)
        {
            shieldImage.fillAmount = 1;
            isCooldown = false;
            player.shieldEffect.SetActive(false);
            BGImage.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }


    public void ResetTimer()
    {
        shieldImage.fillAmount = 1;
    }

    public void ReduceTime(int damage)
    {
        shieldImage.fillAmount += damage / 5f;
    }
}
