﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEvents : MonoBehaviour
{
    public void Close(PointerEventData evt)
    {
        Canvas _canvas = evt.selectedObject.GetComponent<Canvas>();
        string name = _canvas.ToString();
        Managers.ui.CloseUI(_canvas);
        Debug.Log($"UI {name} closed!");
    }

    public void OnDrag(PointerEventData evt)
    {
        Image _Image = this.GetComponentInChildren<Image>();
        string name = _Image.ToString();
        _Image.transform.position = evt.position;
        Debug.Log($"UI {name} is moving! finger ID : {evt.pointerId}");
    }

 
}
