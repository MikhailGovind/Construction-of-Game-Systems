using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinChest : MonoBehaviour
{
    //coin chest
    public GameObject closedChest;
    //public GameObject openChest;
    public GameObject coinOne;
    public GameObject coinTwo;
    public GameObject coinThree;
    public GameObject coinFour;
    public GameObject coinFive;


    public void DropCoin()
    {
        Vector2 position = transform.position;
        GameObject coin = Instantiate(coinOne, position, Quaternion.identity);
        //GameObject secondCoin = Instantiate(coinTwo, position + new Vector2(0.3f, 0.3f), Quaternion.identity);
        //GameObject thirdCoin = Instantiate(coinThree, position + new Vector2(0.6f, 0.6f), Quaternion.identity);
        //GameObject fourthCoin = Instantiate(coinFour, position + new Vector2(-0.3f, -0.3f), Quaternion.identity);
        //GameObject fifthCoin = Instantiate(coinFive, position + new Vector2(-0.6f, -0.6f), Quaternion.identity);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sword")
        {
            Destroy(gameObject);
            DropCoin();
            //openChest.SetActive(true);
        }
    }
}
