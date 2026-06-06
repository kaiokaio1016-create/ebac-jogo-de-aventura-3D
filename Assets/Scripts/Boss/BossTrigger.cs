using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Boss
{
  public class BossTrigger : MonoBehaviour
  {
    public GameObject objToActivate;
    public BossBase boss;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !objToActivate.activeSelf )
        {
          objToActivate.SetActive(true);
          boss.SwitchWalk();
        }

    }
  }

}
