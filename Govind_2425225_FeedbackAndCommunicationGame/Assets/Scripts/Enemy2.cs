using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using TMPro;

public class Enemy2 : MonoBehaviour
{
    PlayerHitbox playerHitbox;
    public string demoVar;

    //Coins to Drop
    public GameObject coinOne;
    public GameObject coinTwo;
    public Transform transform; //Location to drop coins

    private Vector2 _direction;

    Animator animator;
    AIChase2 chaseScript;

    public int damage = 3; //Damage enemy deals

    public int maxHealth = 12;
    public int currentHealth;
    public HealthBar healthBar;
    public TextMeshProUGUI healthText;
    public SpriteRenderer sprite;
    Transform danger;
    public bool inRange;
    public bool isAttacking = false;

    [SerializeField] private float totalCycle = 5f;

    [SerializeField] private float timeBtwnAttacks = 3.5f;
    [SerializeField] private float jumpDuration = 1.5f;

    private float period = 0;

    private void Awake()
    {
        danger = this.gameObject.transform.GetChild(0);
        danger.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        jumpDuration = totalCycle - timeBtwnAttacks;
        animator = GetComponent<Animator>();
        chaseScript = GetComponent<AIChase2>();
        _direction = new Vector2(1, 0);

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

        if ((period > totalCycle) && (inRange))
        {
            //Run Attack Coroutine
            StartCoroutine(JumpingAttack());
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
    }

    public void DamageTaken()
    {
        healthBar.SetHealth(currentHealth);
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    public void Delete()
    {
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
            // Debug.Log("Trigger by sword activated");
            // animator.SetTrigger("Hit");

            // Debug.Log("HP should be " + currentHealth);
            StartCoroutine(FlashRed());
        }
    }

    public IEnumerator FlashRed()
    {
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.red;
    }

    public IEnumerator JumpingAttack()
    {
        //Deals damage thoughout duration of Jump --------------------------
        // chaseScript.isAttacking = true;
        // isAttacking = true;
        // danger.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        // yield return new WaitForSeconds(jumpDuration);
        // danger.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        // chaseScript.isAttacking = false;
        // isAttacking = false;

        //Deals damage only in last 0.1f -----------------------------------
        chaseScript.isAttacking = true;
        danger.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(jumpDuration - 0.1f);
        isAttacking = true;
        yield return new WaitForSeconds(0.1f);
        danger.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        chaseScript.isAttacking = false;
        isAttacking = false;
    }

    public void updateDanger(Vector2 target)
    {
        danger.position = target;
    }
}
