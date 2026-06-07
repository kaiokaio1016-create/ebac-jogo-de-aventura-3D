using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOUIint_Update : MonoBehaviour
{
    public SOint sOint;
    public TextMeshProUGUI uitextValue;
    
    void Start()
    {
        uitextValue.text = sOint.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        uitextValue.text = sOint.value.ToString();
    }
}
