using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Cloth 
{
   public class ClothItemSpeed : ClothItemBase
   {
      public float targetSpeed = 2f;
       public override void Collect()
       {
          base.Collect();
          Player3D.Instance.ChangeSpeed(targetSpeed, duration);
       } 
   }

}
