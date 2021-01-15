using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Utils
{
    public static T GetOrAddComponent<T>(GameObject _go) where T : UnityEngine.Component
    {
        T component = _go.GetComponent<T>();
        if (component == null)
            component = _go.AddComponent<T>();
        return component;
    }
    public static T GetOrAddComponent<T>(Component _component) where T : UnityEngine.Component
    {
        GameObject _go = _component.gameObject;
        return GetOrAddComponent<T>(_go);
    }

    public static void BindTouchEvent(GameObject _go, Action<PointerEventData> action, Define.TouchEvent type = Define.TouchEvent.Tap)
    {
        EventHandlers evt = GetOrAddComponent<EventHandlers>(_go);

        switch(type)
        {
            case Define.TouchEvent.Tap:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.TouchEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            case Define.TouchEvent.Down:
                evt.OnDownHandler -=action;
                evt.OnDownHandler +=action;
                break;
        }
    }

    // public static void BindObjectEvent(GameObject _go, Action<)
    

    
 
}
