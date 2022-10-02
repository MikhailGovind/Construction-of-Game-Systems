using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase2 : MonoBehaviour
{
    public bool isAttacking = false;
    public GameObject player;
    public float speed;
    public float distanceBetween;

    private float distance;

    // private float period = 0;
    Enemy2 enemyMain;
    bool locationUpdated = false;

    // public Sprite dangerCircle;

    Vector2 playerPrevPlace;
    // Update is called once per frame
    private void Awake()
    {
        enemyMain = GetComponent<Enemy2>();
    }
    void Update()
    {
        if (!isAttacking)
        {
            locationUpdated = false;
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            if (distance <= distanceBetween)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                enemyMain.inRange = true;
            } else{
                enemyMain.inRange = false;
            }
        }
        else if(!locationUpdated)
        {
            playerPrevPlace = player.transform.position;
            enemyMain.updateDanger(playerPrevPlace);
            locationUpdated = true;
        }

    }
}
