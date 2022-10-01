using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsScript : MonoBehaviour
{
    public AudioSource glassBreak;
    public AudioSource opened;
    public AudioSource door;
    public AudioSource sword;

    public void Glass()
    {
        glassBreak.Play();
    }

    public void Opened()
    {
        opened.Play();
    }

    public void Sword()
    {
        sword.Play();
    }

    public void Door()
    {
        door.Play();
    }
}
