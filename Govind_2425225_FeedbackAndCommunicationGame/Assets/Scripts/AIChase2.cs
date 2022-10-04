using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase2 : MonoBehaviour
{
    public bool isAttacking = false;
    public bool isWaiting = false;
    public GameObject player;
    public float speed;
    public float distanceBetween;

    private float distance;

    // private float period = 0;
    Enemy2 enemyMain;
    bool locationUpdated = false;

    Vector2 playerPrevPlace;

    //Bezier curve variables
    Vector2 pointStart; //starting point
    Vector2 pointDesti; //ending point
    Vector2 pointBezHeight; //point tat determines height
    Vector2 baseVec;
    float count = 0f; //Counter for Bezier jump
    float result;


    //Test transforms
    // Transform pt1;
    // Transform pt2;
    // Transform pt3;
    private void Awake()
    {
        enemyMain = GetComponent<Enemy2>();
        baseVec = new Vector2(0.5f, 0.5f);
        // pt1 = enemyMain.transform.GetChild(2);
        // pt2 = enemyMain.transform.GetChild(3);
        // pt3 = enemyMain.transform.GetChild(4);
    }
    void Update()
    {
        if (!isWaiting && !isAttacking)
        {
            count = 0;
            locationUpdated = false;
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            if (distance <= distanceBetween)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                enemyMain.inRange = true;
            }
            else
            {
                enemyMain.inRange = false;
            }
        }
        else if (!locationUpdated)
        {
            playerPrevPlace = player.transform.position;
            enemyMain.updateDanger(playerPrevPlace);
            locationUpdated = true;

            pointStart = transform.position;
            pointDesti = playerPrevPlace;

            result = pointDesti.y - pointStart.y;
            Debug.Log(result);
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



            // pt1.position = pointStart;
            // pt2.position = pointBezHeight;
            // pt3.position = pointDesti;
        }

        if ((!isWaiting) && (isAttacking) && (locationUpdated) && (count < 1f))
        {
            enemyMain.updateDanger(playerPrevPlace);
            // pt1.position = pointStart;
            // pt2.position = pointBezHeight;
            // pt3.position = pointDesti;
            //The float here must match the float in Enemy2 script

            count += 1.5f * Time.deltaTime;

            Vector2 line1 = Vector2.Lerp(pointStart, pointBezHeight, count); //Line from Start to Height
            Vector2 line2 = Vector2.Lerp(pointBezHeight, pointDesti, count); //Line from Height to End
            transform.position = Vector2.Lerp(line1, line2, count); //Line from line1 to line2

        }
    }
}
