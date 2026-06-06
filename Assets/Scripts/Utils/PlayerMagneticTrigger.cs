using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Items;

public class PlayerMagneticTrigger : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      CollectableBase i = other.transform.GetComponent<CollectableBase>();
      if(i != null) 
      {
         i.gameObject.AddComponent<Magnetic>();
      }
   }
}
