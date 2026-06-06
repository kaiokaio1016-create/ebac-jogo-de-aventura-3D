using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

public enum UIFillUpdateType
{
    Health,
    Ammo
}
public class UIFillUpdater : MonoBehaviour
{
    public UIFillUpdateType updateType = UIFillUpdateType.Health;
    public Image uiImage;

    [Header("Animation")]
    public float duration = .1f;
    public Ease ease = Ease.OutBack;
    private Tween _currentTween;

    private void OnValidate()
    {
        if(uiImage == null) uiImage = GetComponent<Image>();
    }

    public void UpdateValue(float f)
    {
        uiImage.fillAmount = f;
    }

    public void UpdateValue(float max, float current)
    {
        if(_currentTween != null) _currentTween.Kill();
        uiImage.DOFillAmount(1-(current/max), duration).SetEase(ease);
    }
}
