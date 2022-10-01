using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PotDrops : MonoBehaviour
{
    public int randomiser;
    public GameObject healthPotOne;
    public GameObject coinOne;

    public SoundsScript soundsScript;

    private void Update()
    {
        randomiser = UnityEngine.Random.Range(1, 4);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sword")
        {
            Destroy(gameObject);

            soundsScript.Glass();

            if (randomiser == 1)
            {
                DropCoin();            
            }

            if (randomiser == 2)
            {
                DropHealthPot();
            }

            if (randomiser == 3)
            {
                DropBoth();
            }
        }
    }

    void DropCoin()
    {
        Vector2 position = transform.position;
        GameObject coin = Instantiate(coinOne, position, Quaternion.identity);
    }

    void DropHealthPot()
    {
        Vector2 position = transform.position;
        GameObject healthPot = Instantiate(healthPotOne, position, Quaternion.identity);
    }

    void DropBoth()
    {
        Vector2 position = transform.position;
        GameObject coin = Instantiate(coinOne, position + new Vector2(0.3f, 0.3f), Quaternion.identity);
        GameObject healthPot = Instantiate(healthPotOne, position, Quaternion.identity);
    }
}
