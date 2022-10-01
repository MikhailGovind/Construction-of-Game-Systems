using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public int damage = 3;
    public SoundsScript soundsScript;

    Vector2 rightAttackOffset;

    // Start is called before the first frame update
    void Start()
    {
        rightAttackOffset = transform.localPosition;
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false;
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
        soundsScript.Sword();

        Debug.Log("collider true right");
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
        soundsScript.Sword();

        Debug.Log("collider true left");
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;

        Debug.Log("collider false");
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
    }
}
