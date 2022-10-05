using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public int damage;
    public SoundsScript soundsScript;

    Vector2 rightAttackOffset;
    Vector2 leftAttackOffset;

    public PlayerHitbox playerHitbox;

    // Start is called before the first frame update
    void Start()
    {
        rightAttackOffset = transform.localPosition;
        leftAttackOffset = new Vector2(rightAttackOffset.x -2, rightAttackOffset.y);
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false;
    }

    private void Update()
    {
        damage = playerHitbox.maxStrength - 7;
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
        soundsScript.Sword();

        // Debug.Log("collider true right");
        Debug.Log("light attack" + damage);
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(rightAttackOffset.x - 2, rightAttackOffset.y);
        soundsScript.Sword();

        // Debug.Log("collider true left");
        Debug.Log("light attack" + damage);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;

        // Debug.Log("collider false");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //deal damage to enemy
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.currentHealth -= damage;

                Debug.Log(enemy.maxHealth);
            }
        }
        
        if (other.tag == "Enemy2")
        {
            //deal damage to enemy
            Enemy2 enemy = other.GetComponent<Enemy2>();

            if (enemy != null)
            {
                enemy.currentHealth -= damage;

                // Debug.Log(enemy.maxHealth);
            }
        }
    }
}