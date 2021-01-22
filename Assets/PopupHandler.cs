using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupHandler : MonoBehaviour
{
    public GameObject BaseObj;
    
    public void SetMessage(string message)
    {
        var textComponenent = GetComponentInChildren<TextMeshProUGUI>();
        textComponenent.text = message;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Destroy(BaseObj);
    }
}
