using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossStartCheck : MonoBehaviour
{
    public string tagToCheck = "Player";
    public GameObject bossCamera;
    public Color gizmosColor = Color.yellow;

    private void Awake()
    {
        bossCamera.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCheck) 
        {
            TurnCameraOn();
        }
    }

    private void TurnCameraOn()
    {
        bossCamera.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
    }
}
