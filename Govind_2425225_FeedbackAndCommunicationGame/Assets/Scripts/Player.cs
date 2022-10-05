using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    public SecondSA secondSA;
    public ThirdSA thirdSA;
    public PlayerHitbox playerHitbox;
    public Notification notification; //new line

    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));

                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }

                animator.SetBool("isWalking", success);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            //set direction of sprite to movement direction 
            if (movementInput.x < 0) //Left?
            {
                spriteRenderer.flipX = true;
                
            }
            else if (movementInput.x > 0) //Right?
            {
                spriteRenderer.flipX = false;
                // swordAttack.leftHB();
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast
            (
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            );

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        if (playerHitbox.currentStamina >= 3)
        {
            animator.SetTrigger("swordAttack");
            LightAttack();
        }
    }

    public void LightAttack()
    {
        playerHitbox.currentStamina -= playerHitbox.lightAttackCost;
        StartCoroutine(playerHitbox.StaminaRegain());
    }

    void OnSlash()
    {
        if (playerHitbox.currentStamina >= 5)
            {
                animator.SetTrigger("swordAttack2");
                HeavyAttack();
            }
    }

    public void HeavyAttack()
    {
        playerHitbox.currentStamina -= playerHitbox.heavyAttackCost;
        StartCoroutine(playerHitbox.StaminaRegain());
    }

    void OnCut()
    {
        if (notification.npcInteract >= 1) //new if statement
        {
            if (playerHitbox.currentStamina >= 10)
            {
                animator.SetTrigger("swordAttack3");
                SpecialAttack();
            }
        }
    }

    public void SpecialAttack()
    {
        playerHitbox.currentStamina -= playerHitbox.specialAttackCost;
        StartCoroutine(playerHitbox.StaminaRegain());
    }

    public void SwordAttack()
    {
        LockMovement();

        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void SwordAttack2()
    {
        LockMovement();

        if (spriteRenderer.flipX == true)
        {
            secondSA.AttackLeft();
        }
        else
        {
            secondSA.AttackRight();
        }
    }

    public void EndSwordAttack2()
    {
        UnlockMovement();
        secondSA.StopAttack();
    }

    public void SwordAttack3()
    {
        LockMovement();

        if (spriteRenderer.flipX == true)
        {
            thirdSA.AttackLeft();
        }
        else
        {
            thirdSA.AttackRight();
        }
    }

    public void EndSwordAttack3()
    {
        UnlockMovement();
        thirdSA.StopAttack();
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}
