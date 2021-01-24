using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIEvents : MonoBehaviour
{
    public abstract void Start();
    public void OnOffUI(PointerEventData evt)
    {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        string name = canvas.ToString();
        Managers.ui.CloseUI(canvas);
        Debug.Log($"UI {name} closed!");
    }

    public void OnDrag(PointerEventData evt)
    {
        Image image = this.GetComponentInChildren<Image>();
        string name = image.ToString();
        image.transform.position = evt.position;
        Debug.Log($"UI {name} is moving! finger ID : {evt.pointerId}");
    }

    

 
}
