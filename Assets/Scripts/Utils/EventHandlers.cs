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
    public int NumOfFinger = 1;
    public void OnDrag(PointerEventData eventData)
    {
        if(OnDragHandler != null)
            if(eventData.pointerId < NumOfFinger)
                OnDragHandler.Invoke(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(OnClickHandler != null)
            if(eventData.pointerId < NumOfFinger)
                OnClickHandler.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(OnDownHandler != null)
            if(eventData.pointerId < NumOfFinger)
                OnDownHandler.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
