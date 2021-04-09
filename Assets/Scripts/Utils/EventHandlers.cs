using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class EventHandlers : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IDropHandler, IBeginDragHandler, IEndDragHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnDownHandler = null;
    public Action<PointerEventData> OnUpHandler = null;
    public Action<PointerEventData> OnDropHandler = null;
    public Action<PointerEventData> OnBeginDragHandler = null;
    public Action<PointerEventData> OnEndDragHandler = null;
    public int _max_finger_id = 1;

    void Start()
    {
        _max_finger_id = Managers.scene._max_finger_id;
    }
    public void OnDrag(PointerEventData evt)
    {
        if(OnDragHandler != null)
            if(evt.pointerId < _max_finger_id)
                OnDragHandler.Invoke(evt);
    }

    public void OnDrop(PointerEventData evt)
    {
        if(OnDropHandler != null)
            if(evt.pointerId < _max_finger_id)
                OnDropHandler.Invoke(evt);
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
        if(OnUpHandler != null)
            if(evt.pointerId < _max_finger_id)
                OnUpHandler.Invoke(evt);
    }

    public void OnBeginDrag(PointerEventData evt)
    {
        if(OnBeginDragHandler != null)
            if(evt.pointerId < _max_finger_id)
                OnBeginDragHandler.Invoke(evt);
    }

    public void OnEndDrag(PointerEventData evt)
    {
        if(OnEndDragHandler != null)
            if(evt.pointerId < _max_finger_id)
                OnEndDragHandler.Invoke(evt);
    }
}
