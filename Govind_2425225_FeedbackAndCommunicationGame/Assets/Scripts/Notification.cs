using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Notification : MonoBehaviour
{
    public GameObject firstText;
    public GameObject secondText;
    public GameObject thirdText;
    public GameObject fourthText;
    public GameObject panel;

    public PlayerHitbox playerHitbox;
    public SoundsScript soundsScript;

    public void FirstText()
    {
        panel.SetActive(true);
        firstText.SetActive(true);
    }

    public void SecondText()
    {
        panel.SetActive(true);
        secondText.SetActive(true);
    }

    public void ThirdText()
    {
        panel.SetActive(true);
        thirdText.SetActive(true);

        if (playerHitbox.coinCount >= 20)
        {
            StartCoroutine(WinScene());
        }
    }

    public IEnumerator WinScene()
    {
        thirdText.SetActive(false);
        fourthText.SetActive(true);
        soundsScript.Door();
        soundsScript.Opened();
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("WinScene");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "text1")
        {
            FirstText();
        }

        if (other.tag == "text2")
        {
            if (playerHitbox.coinCount <= 9)
            {
                SecondText();
            }
        }

        if (other.tag == "text3")
        {
            ThirdText();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        panel.SetActive(false);
        firstText.SetActive(false);
        secondText.SetActive(false);
        thirdText.SetActive(false);
        fourthText.SetActive(false);
    }
}
