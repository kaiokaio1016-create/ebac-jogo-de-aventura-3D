using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class FlashColor : MonoBehaviour
{
   public MeshRenderer meshRenderer;
   public SkinnedMeshRenderer skinnedMeshRenderer;

   [Header("Setup")]
   public Color color = Color.red;
   public float duration = .1f;

   private Tween _currentTween;

    public string colorParameter = "_EmissionColor";

   private void OnValidate()
   {
      if(meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
      if(skinnedMeshRenderer == null) skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
   }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        // Se estiver no Edit Mode, usamos sharedMaterial para evitar o aviso/vazamento
        // Se estiver no Play Mode, usamos material para não afetar o asset original
        Renderer target = (Renderer)meshRenderer ?? (Renderer)skinnedMeshRenderer;

        if (target != null)
        {
            // Cancelamos qualquer tween anterior se estiver ativo
            if (_currentTween != null && _currentTween.IsActive())
            {
                _currentTween.Kill();
            }

            if (Application.isPlaying)
            {
                _currentTween = target.material.DOColor(color, colorParameter, duration).SetLoops(2, LoopType.Yoyo);
            }
            else
            {
                // Debug.Log para avisar que no modo de edição alteramos o sharedMaterial
                _currentTween = target.sharedMaterial.DOColor(color, colorParameter, duration).SetLoops(2, LoopType.Yoyo);
            }
        }
}   }
