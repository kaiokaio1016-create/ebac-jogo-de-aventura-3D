using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Color colorOn = Color.white;
    public Color colorOff = Color.black;
    public int key = 01;
    private bool checkpointActive = false;
    private string checkpointKey = "CheckpointKey";


   private void OnTriggerEnter(Collider other)
   {
      if(other.transform.tag == "Player" && !checkpointActive) 
      {
        CheckCheckpoint();
      }
   }

   private void CheckCheckpoint () 
   {
      TurnItOn();
      SaveCheckpoint();
   }
   
   [NaughtyAttributes.Button]
   private void TurnItOn()
   {
      meshRenderer.material.SetColor("_EmissionColor", colorOn);
   }

   [NaughtyAttributes.Button]
   private void TurnItOff()
   {
      meshRenderer.material.SetColor("_EmissionColor", colorOff);
   }

   private void SaveCheckpoint()
   {
      /*if(PlayerPrefs.GetInt(checkpointKey, 0) > key) 
      {
        PlayerPrefs.SetInt(checkpointKey, key);
      }*/

      CheckpointManager.Instance.SaveCheckpoint(key);

      checkpointActive = true;
    }
}
