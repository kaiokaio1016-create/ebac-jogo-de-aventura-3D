using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    [Header("Setup")]
    public Color color = Color.red;
    public float duration = .1f;

    private Color defaultColor;
    private Tween _currTween;

    private void Start()
    {
        // Ensure you have a material with the _EmissionColor property
        defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        // Check if the tween is null or not active before starting a new one
        if (_currTween == null || !_currTween.IsActive())
        {
            _currTween = meshRenderer.material.DOColor(color, "_EmissionColor", duration)
                .SetLoops(2, LoopType.Yoyo);
        }
    }
}