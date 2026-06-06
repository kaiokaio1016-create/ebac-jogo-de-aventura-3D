using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Items;

public class ActionLifePack : MonoBehaviour
{
   public KeyCode keyCode = KeyCode.L;
   public SOint soInt;

   private void Start()
   {
       soInt = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
   }

   private void RecoverLife()
   {
      if(soInt.value > 0) 
      {
          ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK);
          Player3D.Instance.healthBase.ResetLife();
      }
   }

   private void Update() 
   {
      if(Input.GetKeyDown(keyCode)) 
      {
        RecoverLife();
      }
   }
}
