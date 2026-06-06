using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Cloth
{
   public class ClothItemBase : MonoBehaviour
   {
      public ClothType clothType;
      public float duration = 2f;
      public string compareTag = "Player";

      private void OnTriggerEnter(Collider other)
      {
        if(other.transform.CompareTag(compareTag)) 
        {
            Collect();
        }
      }

      public virtual void Collect()
      {
        Debug.Log("Collect");
        var setup = ClothManager.Instance.GetSetupByType(clothType);
        Player3D.Instance.ChangeTexture(setup, duration);
        HideObject();
      }

      private void HideObject()
      {
        gameObject.SetActive(false);
      }
   } 

}
