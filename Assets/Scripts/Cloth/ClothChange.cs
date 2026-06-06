using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Cloth
{
public class ClothChange : MonoBehaviour
{
    public SkinnedMeshRenderer mesh;
    public Texture2D texture;
    public string shaderName = "_EmissionMap";

    private Texture2D _defaultTexture;

    private void Awake() 
    {
      _defaultTexture = (Texture2D) mesh.sharedMaterials[0].GetTexture(shaderName);
    }

   [NaughtyAttributes.Button]
   private void ChangeTexture()
   {
     mesh.sharedMaterials[0].SetTexture(shaderName, texture);
   }

   public void ChangeTexture(ClothSetup setup)
   {
     mesh.sharedMaterials[0].SetTexture(shaderName, setup.texture);
   }

   public void ResetTexture()
   {
      mesh.sharedMaterials[0].SetTexture(shaderName, _defaultTexture);
   }

}
}
