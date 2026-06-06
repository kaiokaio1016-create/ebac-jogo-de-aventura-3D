using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace Items
{
 public class ItemLayout : MonoBehaviour
 {
    private ItemManager.ItemSetup _currentSetup;

    public Image uiIcon;
    public TextMeshProUGUI uiValue;

    public void Load(ItemManager.ItemSetup setup)
    {
        _currentSetup = setup;
        UpdateUI();
    }

    private void UpdateUI()
    {
        uiIcon.sprite = _currentSetup.icon;
    }

    private void Update()
    {
        uiValue.text = _currentSetup.soInt.value.ToString();
    }
   
 }

}
