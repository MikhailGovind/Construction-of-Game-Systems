using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Notification : MonoBehaviour
{
    //king's notice
    public GameObject kingNotice;
    public GameObject kingPanel;
    public GameObject firstText;
    public GameObject secondText;
    public GameObject thirdText;
    public SpriteRenderer spriteRenderer;

    //skeegelmore text
    public GameObject skeegPanel;
    public GameObject fourthText;
    public GameObject fifthText;
    public GameObject sixthText;


    public PlayerHitbox playerHitbox;
    public SoundsScript soundsScript;


    private void Awake()
    {
        StartCoroutine(Flash());
    }

    #region King's Notice
    public IEnumerator Flash()
    {
        spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = Color.white;
    }

    public IEnumerator kingPanelOpen()
    {
        kingPanel.SetActive(true);
        firstText.SetActive(true);
        yield return new WaitForSeconds(3f);
        firstText.SetActive(false);
        secondText.SetActive(true);
        yield return new WaitForSeconds(5f);
        secondText.SetActive(false);
        thirdText.SetActive(true);
        yield return new WaitForSeconds(3f);
        kingNotice.SetActive(false);
    }

    public void kingPanelClosed()
    {
        kingPanel.SetActive(false);
    }

    #endregion

    #region Skeeg's Notice
    public IEnumerator skeegPanelOpen()
    {
        skeegPanel.SetActive(true);
        fourthText.SetActive(true);
        yield return new WaitForSeconds(3f);
        fourthText.SetActive(false);
        fifthText.SetActive(true);
        yield return new WaitForSeconds(3f);
        fifthText.SetActive(false);
        sixthText.SetActive(true);
        yield return new WaitForSeconds(3f);
        skeegPanel.SetActive(false);
    }

    public void skeegPanelClosed()
    {
        kingPanel.SetActive(false);
    }

    #endregion

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "KingNotice")
        {
            StartCoroutine(kingPanelOpen());
        }

        if (other.tag == "SkeegNotice")
        {
            StartCoroutine(skeegPanelOpen());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "KingNotice")
        {
            kingPanelClosed();
            kingNotice.SetActive(false);
        }

        if (other.tag == "SkeegNotice")
        {
            skeegPanelClosed();
        }
    }
}
