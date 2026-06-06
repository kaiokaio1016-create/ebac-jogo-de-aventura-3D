using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Cloth 
{
   public class ClothItemStrong : ClothItemBase
   {
      public float damageMultiply = .5f;
       public override void Collect()
       {
          base.Collect();
          Player3D.Instance.healthBase.ChangeDamageMultiply(damageMultiply, duration);
       } 
   }

}
