using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using TMPro;
public class Boss : MonoBehaviour
{
    public PlayerHitbox playerHitbox;

    //Coins to Drop
    public GameObject coinOne;
    public GameObject coinTwo;
    public Transform transform; //Location to drop coins

    private Vector2 _direction;

    Animator animator; //

    public int damage = 2; //Damage enemy deals
    public int maxHealth = 30;
    public int currentHealth;
    public HealthBar healthBar;
    public TextMeshProUGUI healthText;
    public SpriteRenderer sprite;
    public int enemyType;
    public int killedBoss;
    public GameObject bossArea;

    private void Start()
    {
        animator = GetComponent<Animator>();
        _direction = new Vector2(1, 0);

        if (enemyType == 1)
        {
            maxHealth = 5;
        }

        if (enemyType == 2)
        {
            maxHealth = 8;
        }

        //health bar
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        DamageTaken();
        healthText.text = "HP:" + currentHealth;

        if (_direction.x == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        if (currentHealth <= 0)
        {
            Defeated();
            currentHealth = 0;
        }
    }

    public void DamageTaken()
    {
        healthBar.SetHealth(currentHealth);
    }

    public void Defeated()
    {
        playerHitbox.killedBoss += 1;
        animator.SetTrigger("Defeated");
    }

    public void Delete()
    {
        Destroy(bossArea);
        Destroy(gameObject);
        DropCoin();
    }

    void DropCoin()
    {
        Vector2 position = transform.position;
        GameObject coin = Instantiate(coinOne, position, Quaternion.identity);
        GameObject secondCoin = Instantiate(coinTwo, position + new Vector2(0.3f, 0.3f), Quaternion.identity);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sword")
        {
            StartCoroutine(FlashRed());
        }
    }

    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
