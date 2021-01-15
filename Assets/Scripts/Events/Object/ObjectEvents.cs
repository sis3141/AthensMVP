using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public abstract class ObjectEvents : MonoBehaviour
{
    // public Action<PointerEventData> ObjectInteractionHandler = null;
    
    [SerializeField]
    public float _speed = 10.0f;
    public abstract void Start();
    // public void BindObjectEvent(Action<PointerEventData> action)
    // {
    //     ObjectInteractionHandler -= action;
    //     ObjectInteractionHandler += action;
    // }
    public void Destroy(PointerEventData evt)
    {
        Debug.Log("Destroy Object!");
        Managers.resource.Destroy(gameObject);
    }

    

    // public void SendEventData(PointerEventData evt)
    // {
    //     //set layer to field
    //     if(ObjectInteractionHandler!=null)
    //     {
    //         Debug.Log($"I send data{evt.position}");
    //         ObjectInteractionHandler.Invoke(evt);
    //     }
    // }

    // public void ReceiveEventData(PointerEventData evt)
    // {
    //     _event_data = evt;
    //     Debug.Log($"I got data {evt.position}");
    // }
}
