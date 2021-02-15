using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class EzEventHandlers : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IDropHandler
{
    public Action OnClickHandler = null;
    public Action OnDragHandler = null;
    public Action OnDownHandler = null;
    public Action OnUpHandler = null;
    public int _max_finger_id = 1;

    void Start()
    {
        _max_finger_id = Managers.scene._current_scene._max_finger_id;
    }
    public void OnDrag(PointerEventData evt)
    {
        if(OnDragHandler != null)
            if(evt.pointerId < _max_finger_id)
                OnDragHandler.Invoke();
    }

    public void OnDrop(PointerEventData evt)
    {
    }

    public void OnPointerClick(PointerEventData evt)
    {
        if(OnClickHandler != null)
            if(evt.pointerId < _max_finger_id)
                OnClickHandler.Invoke();
    }

    public void OnPointerDown(PointerEventData evt)
    {
        if(OnDownHandler != null)
            if(evt.pointerId < _max_finger_id)
                OnDownHandler.Invoke();
    }

    public void OnPointerUp(PointerEventData evt)
    {
        if(OnUpHandler != null)
            if(evt.pointerId < _max_finger_id)
                OnUpHandler.Invoke();
    }
}
