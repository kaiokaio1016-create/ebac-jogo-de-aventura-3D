using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.E;
    public Animator animator;
    public string triggerOpen = "Open"; 
    
    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;

    [Space]
    public ChestItemBase chestItem;

    private float startScale;
    private bool _chestOpened = false;

    void Start()
    {
        startScale = notification.transform.localScale.x;
        HideNotification();
    }

    private void Update()
    {
        if(Input.GetKeyDown(keyCode) && notification.activeSelf) 
        {
            OpenChest();
        }
    }

    [NaughtyAttributes.Button] 
    private void OpenChest()
    {
        if(_chestOpened) return;

       animator.SetTrigger(triggerOpen);
       _chestOpened = true;
       HideNotification();
       Invoke(nameof(ShowItem), 1f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), 1f);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    }

    public void OnTriggerEnter(Collider other)
    {
        Player3D p = other.transform.GetComponent<Player3D>();
        if(p != null)
        {
           ShowNotification();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Player3D p = other.transform.GetComponent<Player3D>();
        if(p != null)
        {
           HideNotification();
        }
    }

    private void ShowNotification()
    {
        if(_chestOpened) return;
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(startScale, tweenDuration);
    }

    private void HideNotification()
    {
        notification.SetActive(false);
    }
}
