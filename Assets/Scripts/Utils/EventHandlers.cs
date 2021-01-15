using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class EventHandlers : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IDropHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnDownHandler = null;
    public int _max_finger_id = 1;
    public void OnDrag(PointerEventData evt)
    {
        if(OnDragHandler != null)
            if(evt.pointerId < _max_finger_id)
                OnDragHandler.Invoke(evt);
    }

    public void OnDrop(PointerEventData evt)
    {
    }

    public void OnPointerClick(PointerEventData evt)
    {
        if(OnClickHandler != null)
            if(evt.pointerId < _max_finger_id)
                OnClickHandler.Invoke(evt);
    }

    public void OnPointerDown(PointerEventData evt)
    {
        if(OnDownHandler != null)
            if(evt.pointerId < _max_finger_id)
                OnDownHandler.Invoke(evt);
    }

    public void OnPointerUp(PointerEventData evt)
    {
    }
}
