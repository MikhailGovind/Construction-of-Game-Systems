using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using TMPro;

public class Enemy3 : MonoBehaviour
{
    private Vector2 _direction;

    Animator animator;
    AIChase3 chaseScript;

    public int damage = 5; //Damage enemy deals (PlayerHitbox needs to get this from the parent of slime (this))
    public Transform topLeft;
    public Transform topRight;
    public Transform bottomLeft;
    public Transform bottomRight;
    public Transform centre;
    public Transform pnt6, pnt7, pnt8, pnt9, pnt10, pnt11, pnt12, pnt13;
    Transform desti;
    public GameObject boss;

    int randomNumber, lastNumber;
    public bool inRange = false;
    public bool isAttacking = false;
    public bool isWaiting = true;
    public bool isJumping = false;
    public bool isTransitioning = false;
    public bool isDashing = false;
    public GameObject player;
    public float distanceBetween = 5;
    private float distance;

    // private float period = 0;
    Enemy3 enemyMain;
    bool locationUpdated = false;

    Vector2 playerPrevPlace;

    //Bezier curve variables
    Vector2 pointStart; //starting point
    Vector2 pointDesti; //ending point
    Vector2 pointBezHeight; //point tat determines height
    Vector2 baseVec;
    float count = 0f; //Counter for Bezier jump
    public float result;

    [SerializeField] private float totalCycle = 5f;

    [SerializeField] private float timeBtwnAttacks = 3.5f;
    [SerializeField] private float jumpDuration = 1.5f;

    private float period = 0;

    private void Awake()
    {
        topLeft = this.gameObject.transform.GetChild(0);
        topRight = this.gameObject.transform.GetChild(1);
        bottomLeft = this.gameObject.transform.GetChild(2);
        bottomRight = this.gameObject.transform.GetChild(3);
        centre = this.gameObject.transform.GetChild(4);
        pnt6 = this.gameObject.transform.GetChild(5);
        pnt7 = this.gameObject.transform.GetChild(6);
        pnt8 = this.gameObject.transform.GetChild(7);
        pnt9 = this.gameObject.transform.GetChild(8);
        pnt10 = this.gameObject.transform.GetChild(9);
        pnt11 = this.gameObject.transform.GetChild(10);
        pnt12 = this.gameObject.transform.GetChild(11);
        pnt13 = this.gameObject.transform.GetChild(12);
        boss = this.gameObject.transform.GetChild(13).gameObject;
        // danger = this.gameObject.transform.GetChild(0);
        // danger.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        jumpDuration = totalCycle - timeBtwnAttacks;
        animator = boss.GetComponent<Animator>();
        // animator = GetComponent<Animator>();
        // chaseScript = GetComponent<AIChase3>();
        // _direction = new Vector2(1, 0);

        baseVec = new Vector2(0.5f, 0.5f);
        Attack();
    }

    public void Attack()
    {
        isAttacking = true;
        // if (inRange)
        // {

        randomNumber = Random.Range(1, 14);
        if (randomNumber == lastNumber)
        {
            randomNumber = Random.Range(1, 14);
        }
        lastNumber = randomNumber;
        // int randomNumber = 6;
        upBezier(randomNumber);

        switch (randomNumber)
        {
            case 1:
                StartCoroutine(JumpingAttack(topLeft));
                break;
            case 2:
                StartCoroutine(JumpingAttack(topRight));
                break;
            case 3:
                StartCoroutine(JumpingAttack(bottomLeft));
                break;
            case 4:
                StartCoroutine(JumpingAttack(bottomRight));
                break;
            case 5:
                StartCoroutine(JumpingAttack(centre));
                break;
            case 6:
                desti = pnt7;
                StartCoroutine(DashingAttack(topLeft, topRight));
                break;
            case 7:
                desti = pnt6;
                StartCoroutine(DashingAttack(topLeft, topRight));
                break;
            case 8:
                desti = pnt9;
                StartCoroutine(DashingAttack(bottomLeft, bottomRight));
                break;
            case 9:
                desti = pnt8;
                StartCoroutine(DashingAttack(bottomLeft, bottomRight));
                break;
            case 10:
                desti = pnt12;
                StartCoroutine(DashingAttack(bottomRight, topRight));
                break;
            case 11:
                desti = pnt13;
                StartCoroutine(DashingAttack(bottomLeft, topLeft));
                break;
            case 12:
                desti = pnt10;
                StartCoroutine(DashingAttack(bottomRight, topRight));
                break;
            case 13:
                desti = pnt11;
                StartCoroutine(DashingAttack(bottomLeft, topLeft));
                break;
        }

        // Attack();
        // }

    }
    public void upBezier(int pnt)
    {
        switch (pnt)
        {
            case 1:
                pointStart = boss.transform.position;
                pointDesti = topLeft.position;

                result = pointDesti.y - pointStart.y;
                Debug.Log(result);
                break;
            case 2:
                pointStart = boss.transform.position;
                pointDesti = topRight.position;

                result = pointDesti.y - pointStart.y;
                Debug.Log(result);
                break;
            case 3:
                pointStart = boss.transform.position;
                pointDesti = bottomLeft.position;

                result = pointDesti.y - pointStart.y;
                Debug.Log(result);
                break;
            case 4:
                pointStart = boss.transform.position;
                pointDesti = bottomRight.position;

                result = pointDesti.y - pointStart.y;
                Debug.Log(result);
                break;
            case 5:
                pointStart = boss.transform.position;
                pointDesti = centre.position;

                result = pointDesti.y - pointStart.y;
                Debug.Log(result);
                break;
            case 6:
                pointStart = boss.transform.position;
                pointDesti = pnt6.position;

                result = pointDesti.y - pointStart.y;
                Debug.Log(result);
                break;
            case 7:
                pointStart = boss.transform.position;
                pointDesti = pnt7.position;

                result = pointDesti.y - pointStart.y;
                Debug.Log(result);
                break;
            case 8:
                pointStart = boss.transform.position;
                pointDesti = pnt8.position;

                result = pointDesti.y - pointStart.y;
                Debug.Log(result);
                break;
            case 9:
                pointStart = boss.transform.position;
                pointDesti = pnt9.position;

                result = pointDesti.y - pointStart.y;
                Debug.Log(result);
                break;
            case 10:
                pointStart = boss.transform.position;
                pointDesti = pnt10.position;

                result = pointDesti.y - pointStart.y;
                Debug.Log(result);
                break;
            case 11:
                pointStart = boss.transform.position;
                pointDesti = pnt11.position;

                result = pointDesti.y - pointStart.y;
                Debug.Log(result);
                break;
            case 12:
                pointStart = boss.transform.position;
                pointDesti = pnt12.position;

                result = pointDesti.y - pointStart.y;
                Debug.Log(result);
                break;
            case 13:
                pointStart = boss.transform.position;
                pointDesti = pnt13.position;

                result = pointDesti.y - pointStart.y;
                Debug.Log(result);
                break;
        }

        if (result > 0)
        {
            if (result < 0.5f)
            {
                pointBezHeight = pointStart + (pointDesti - pointStart) / 2 +
                            Vector2.up * (baseVec) * 10f;
            }
            else if (result < 1.8f)
            {
                pointBezHeight = pointStart + (pointDesti - pointStart) / 2 +
                            Vector2.up * (pointDesti - pointStart) * 8f;
            }
            else
            {
                pointBezHeight = pointStart + (pointDesti - pointStart) / 2 +
                            Vector2.up * (pointDesti - pointStart) * 4f;
            }

        }
        else if (result < 0)
        {
            if (result > -0.5f)
            {
                pointBezHeight = pointStart + (pointDesti - pointStart) / 2 +
                            Vector2.up * (baseVec) * 10f;
            }
            else if (result > -1.8f)
            {
                pointBezHeight = pointStart + (pointDesti - pointStart) / 2 +
                                        Vector2.up * (pointStart - pointDesti) * 8f;
            }
            else
            {
                pointBezHeight = pointStart + (pointDesti - pointStart) / 2 +
                                            Vector2.up * (pointStart - pointDesti) * 4f;
            }

        }
        else
        {
            pointBezHeight = pointStart + (pointDesti - pointStart) / 2 +
                                        Vector2.up * 9f;
        }

    }



    public IEnumerator JumpingAttack(Transform danger)
    {
        //Deals damage only in last 0.1f -----------------------------------

        //Warning section 
        danger.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(2.5f);

        //Moving and Attacking
        isJumping = true;
        danger.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 18, 0, 205); //Red
        danger.gameObject.GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(1.5f);

        //Resetting to base state
        isJumping = false;
        count = 0;
        danger.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 142, 0, 140);//Orange
        danger.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        danger.gameObject.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }
    public IEnumerator DashingAttack(Transform danger1, Transform danger2)
    {
        //Deals damage only in last 0.1f -----------------------------------

        //Transitions from where it is to side pnt
        isTransitioning = true;
        yield return new WaitForSeconds(1.5f);
        isTransitioning = false;
        count = 0;

        //Waits and highlights bad
        danger1.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        danger2.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(2.5f);

        //Dashes
        isDashing = true;
        danger1.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 18, 0, 205);
        danger1.gameObject.GetComponent<Collider2D>().enabled = true;
        danger2.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 18, 0, 205);
        danger2.gameObject.GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(1f);
        isDashing = false;

        // danger1.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 142, 0, 140);
        // danger2.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 142, 0, 140);
        danger1.gameObject.GetComponent<Collider2D>().enabled = false;
        danger1.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 142, 0, 140);
        danger1.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        danger2.gameObject.GetComponent<Collider2D>().enabled = false;
        danger2.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 142, 0, 140);
        danger2.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }


    private void Update()
    {
        // distance = Vector2.Distance(transform.position, player.transform.position);
        // if (distance <= distanceBetween && !inRange)
        // {
        //     inRange = true;
        //     Attack();
        // }
        // else
        // {
        //     inRange = false;
        // }

        if (isDashing)
        {
            boss.transform.position = Vector2.MoveTowards(boss.transform.position, desti.position,
            40 * Time.deltaTime);
        }

        if ((isTransitioning) && (count < 1.5f))
        {
            count += 2f * Time.deltaTime;

            Vector2 line1 = Vector2.Lerp(pointStart, pointBezHeight, count); //Line from Start to Height
            Vector2 line2 = Vector2.Lerp(pointBezHeight, pointDesti, count); //Line from Height to End
            boss.transform.position = Vector2.Lerp(line1, line2, count); //Line from line1 to line2
        }

        if ((isJumping) && (count < 1.5f))
        {
            count += 2f * Time.deltaTime;

            Vector2 line1 = Vector2.Lerp(pointStart, pointBezHeight, count); //Line from Start to Height
            Vector2 line2 = Vector2.Lerp(pointBezHeight, pointDesti, count); //Line from Height to End
            boss.transform.position = Vector2.Lerp(line1, line2, count); //Line from line1 to line2
        }


        if (!isAttacking)
        {
            Attack();
        }
    }



}
