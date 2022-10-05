using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomIn : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            Debug.Log("Trigger activated");
            vcam.m_Lens.OrthographicSize = 7.5f;
        }
    }
}
