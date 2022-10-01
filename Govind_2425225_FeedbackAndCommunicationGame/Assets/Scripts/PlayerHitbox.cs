using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHitbox : MonoBehaviour
{
    Player player;

    //health
    public SpriteRenderer spriteRenderer;
    public int maxHealth = 10;
    public int currentHealth;
    public int hurt = 2;
    public int healthGain;
    public HealthBar healthBar;
    public TextMeshProUGUI healthText;

    //stamina
    public int maxStamina = 10;
    public int currentStamina;
    public int lightAttackCost;
    public int heavyAttackCost;
    public int specialAttackCost;
    public int staminaRegain;
    public TextMeshProUGUI staminaText;

    //coin
    public int coinCount;
    public int coinUp;
    public TextMeshProUGUI coinText;

    //door
    public GameObject closedDoor;
    public GameObject openDoor;
    public SoundsScript soundsScript;

    public void Awake()
    {
        maxHealth = 10;
        hurt = 2;
        healthGain = 2;
        coinUp = 1;

        maxStamina = 10;
        lightAttackCost = 3;
        heavyAttackCost = 5;
        specialAttackCost = 10;
        staminaRegain = 1;

}

public void Start()
    {
        //health bar
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //stamina bar
        currentStamina = maxStamina;
        healthBar.SetMaxStamina(maxStamina);
    }

    public void Update()
    {
        DamageTaken();
        StaminaLoss();
        coinText.text = " " + coinCount;
        healthText.text = "HP:" + currentHealth;
        staminaText.text = "ST:" + currentStamina;
    }

    public void DamageTaken()
    {
        healthBar.SetHealth(currentHealth);

        if (currentHealth == 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    public void StaminaLoss()
    {
        healthBar.SetStamina(currentStamina);

        if(currentStamina >= 10)
        {
            currentStamina = maxStamina;
        }

        if (currentStamina <= 0)
        {
            currentStamina = 0;
        }
    }

    public IEnumerator StaminaRegain()
    {
        while (currentStamina != maxStamina)
        {
            currentStamina += staminaRegain;
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            currentHealth -= hurt;
            StartCoroutine(FlashRed());

            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }

        if (other.tag == "HealthPot")
        {
            if (currentHealth < 10)
            {
                currentHealth += healthGain;
                StartCoroutine(FlashGreen());
            }

            if (currentHealth >= 10)
            {
                currentHealth = 10;
            }
        }

        if (other.tag == "Coin")
        {
            StartCoroutine(FlashGold());
        }

        if (other.tag == "Door")
        {
            if (coinCount >= 10)
            {
                StartCoroutine(FlashBlue());
                closedDoor.SetActive(false);
                openDoor.SetActive(true);
                soundsScript.Door();
                soundsScript.Opened();
            }
        }
    }

    public IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    public IEnumerator FlashGreen()
    {
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    public IEnumerator FlashGold()
    {
        coinCount += coinUp;
        spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    public IEnumerator FlashBlue()
    {
        spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = Color.white;
    }

}
