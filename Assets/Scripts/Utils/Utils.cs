using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Utils
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }
    public static T GetOrAddComponent<T>(Component component) where T : UnityEngine.Component
    {
        GameObject go = component.gameObject;
        return GetOrAddComponent<T>(go);
    }

    public static void BindTouchEvent(GameObject go, Action<PointerEventData> action, Define.TouchEvent type = Define.TouchEvent.Tap)
    {
        EventHandlers evt = GetOrAddComponent<EventHandlers>(go);

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
    
    // public static void BindObjectEvent(GameObject go, Action<)
}
