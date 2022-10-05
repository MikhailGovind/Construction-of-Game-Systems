using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class StatHUD : MonoBehaviour
{
    public TextMeshProUGUI constitutionStatText;
    public TextMeshProUGUI statStatText;
    public TextMeshProUGUI healthStatText;
    public TextMeshProUGUI strenghtStatText;

    public PlayerHitbox playerHitbox;

    private void Update()
    {
        healthStatText.text = " " + playerHitbox.maxHealth;
        strenghtStatText.text = " " + playerHitbox.maxStrength;
        constitutionStatText.text = " " + playerHitbox.maxStamina;
        statStatText.text = playerHitbox.actorName + "'s Stats:";
    }
}
