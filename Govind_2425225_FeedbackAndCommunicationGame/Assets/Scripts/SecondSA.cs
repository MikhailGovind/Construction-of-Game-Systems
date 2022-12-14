using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SecondSA : MonoBehaviour
{
    public Collider2D swordCollider;
    public int damage;
    public SoundsScript soundsScript;

    Vector2 rightAttackOffset;
    Vector2 leftAttackOffset;

    public PlayerHitbox playerHitbox;

    private void Start()
    {
        rightAttackOffset = transform.localPosition;
        leftAttackOffset = transform.localPosition;
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false;
        damage = 5;
    }

    private void Update()
    {
        damage = playerHitbox.maxStrength - 5;
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        // Debug.Log("SASecond happened");

        transform.localPosition = rightAttackOffset;
        soundsScript.Sword();

        Debug.Log("heavy attack" + damage);
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        // Debug.Log("SASecond happened");
        transform.localPosition = new Vector2(rightAttackOffset.x - 2, rightAttackOffset.y);
        soundsScript.Sword();

        Debug.Log("heavy attack" + damage);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
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

                // Debug.Log("trigger activated");
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

        if (other.tag == "Boss")
        {
            //deal damage to enemy
            Boss boss = other.GetComponent<Boss>();

            if (boss != null)
            {
                boss.currentHealth -= damage;

            }
        }
    }
}