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
    public int maxHealth;
    public int currentHealth;
    public int hurtSlime = 2;
    public int healthGain;
    public HealthBar healthBar;
    public TextMeshProUGUI healthText;
    public Collider2D playColl;

    //stamina
    public int maxStamina = 10;
    public int currentStamina;
    public int lightAttackCost;
    public int heavyAttackCost;
    public int specialAttackCost;
    public int staminaRegain;
    public TextMeshProUGUI staminaText;

    //strength
    public int maxStrength;

    //coin
    public int coinCount;
    public int coinUp;
    public TextMeshProUGUI coinText;

    //door
    public GameObject closedDoor;
    public GameObject openDoor;
    public GameObject doorCollider;
    public SoundsScript soundsScript;
    public int killedBoss;

    public static int coins;
    public string actorName;

    //shop
    public GameObject backToMapPanel;
    public GameObject mainPanels;
    public GameObject buttonsButton;

    //npc
    public GameObject npc;
    public Notification notification;

    //boss
    public Boss boss;

    public void Awake()
    {
        maxHealth = 20;
        hurtSlime = 2;
        healthGain = 2;

        coinCount = 90;
        coinUp = 10;

        maxStamina = 10;
        lightAttackCost = 3;
        heavyAttackCost = 5;
        specialAttackCost = 10;
        staminaRegain = 1;

        maxStrength = 10;
    }

    public void Start()
    {
        //health bar
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //stamina bar
        currentStamina = maxStamina;
        healthBar.SetMaxStamina(maxStamina);
        playColl = this.gameObject.GetComponent<Collider2D>();
    }

    public void Update()
    {
        DamageTaken();
        StaminaLoss();
        coinText.text = " " + coinCount;
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
        staminaText.text = "ST:" + currentStamina + "/" + maxStamina;

        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        healthBar.SetMaxStamina(maxStamina);
        healthBar.SetStamina(currentStamina);
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

        if (currentStamina >= maxStamina)
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy2")
        {
            Enemy2 enemy = other.GetComponent<Enemy2>();
            if (enemy.isAttacking)
            {
                currentHealth -= enemy.damage;
                StartCoroutine(FlashRed());

                if (currentHealth < 0)
                {
                    currentHealth = 0;
                }
            }

        }

        // if (other.tag == "Enemy3")
        // {
        //     Enemy3 enemy = other.GetComponent<Enemy3>();
        //     if (enemy.isAttacking)
        //     {
        //         currentHealth -= enemy.damage;
        //         StartCoroutine(FlashRed());

        //         if (currentHealth < 0)
        //         {
        //             currentHealth = 0;
        //         }
        //     }

        // }

        if (other.tag == "BossPlate")
        {
            Enemy3 enemy = other.GetComponentInParent<Enemy3>();

            currentHealth -= enemy.damage;
            StartCoroutine(FlashRed());

            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            currentHealth -= enemy.damage;
            StartCoroutine(FlashRed());

            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }

        if (other.tag == "HealthPot")
        {
            StartCoroutine(FlashGreen());
        }

        if (other.tag == "Coin")
        {
            StartCoroutine(FlashGold());
        }

        if (other.tag == "Door")
        {
            if (killedBoss >= 1)
            {
                StartCoroutine(FlashBlue());
                closedDoor.SetActive(false);
                openDoor.SetActive(true);
                doorCollider.SetActive(false);
                soundsScript.Door();
                soundsScript.Opened();
            }
        }

        if (other.tag == "Shop")
        {
            mainPanels.SetActive(true);
            backToMapPanel.SetActive(true);
            buttonsButton.SetActive(true);
        }

        if (other.tag == "Win")
        {
            StartCoroutine(FlashBlue());
            StartCoroutine(Win());
        }

        if (other.tag == "NPC")
        {
            StartCoroutine(NPC());
        }
    }

    public IEnumerator NPC()
    {
        npc.SetActive(true);
        yield return new WaitForSeconds(1f);
        notification.NPCInteraction();
        yield return new WaitForSeconds(12f);
        npc.SetActive(false);
    }

    public IEnumerator Win()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("WinScene");
    }

    public IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        playColl.enabled = false;

        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(0.7f);
        playColl.enabled = true;
    }

    public IEnumerator FlashGreen()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healthGain;
            spriteRenderer.color = Color.green;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.white;
        }

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
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