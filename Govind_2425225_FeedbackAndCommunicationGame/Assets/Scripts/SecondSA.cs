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

    private void Start()
    {
        rightAttackOffset = transform.localPosition;
        leftAttackOffset = transform.localPosition;
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false;
        damage = 5;
    }

public void AttackRight()
    {
        swordCollider.enabled = true;
        Debug.Log("SASecond happened");

        transform.localPosition = rightAttackOffset;
        soundsScript.Sword();
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        Debug.Log("SASecond happened");
        transform.localPosition = new Vector2(rightAttackOffset.x - 2, rightAttackOffset.y);
        soundsScript.Sword();
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

                Debug.Log("trigger activated");
            }
        }
    }
}
