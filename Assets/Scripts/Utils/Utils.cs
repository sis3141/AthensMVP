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
            case Define.TouchEvent.StartDrag:
                evt.OnBeginDragHandler -= action;
                evt.OnBeginDragHandler += action;
                break;
            case Define.TouchEvent.Drop:
                evt.OnDropHandler -= action;
                evt.OnDropHandler += action;
                break;
            case Define.TouchEvent.EndDrag:
                evt.OnEndDragHandler -= action;
                evt.OnEndDragHandler += action;
                break;
            case Define.TouchEvent.OnDrag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            case Define.TouchEvent.Down:
                evt.OnDownHandler -=action;
                evt.OnDownHandler +=action;
                break;
            case Define.TouchEvent.Up:
                evt.OnUpHandler -=action;
                evt.OnUpHandler +=action;
                break;
        }
    }

    public static void BindTouchEvent(GameObject go, Action action, Define.TouchEvent type = Define.TouchEvent.Tap)
    {
        EzEventHandlers evt = GetOrAddComponent<EzEventHandlers>(go);

        switch(type)
        {
            case Define.TouchEvent.Tap:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.TouchEvent.StartDrag:
                evt.OnBeginDragHandler -= action;
                evt.OnBeginDragHandler += action;
                break;
            case Define.TouchEvent.Drop:
                evt.OnDropHandler -= action;
                evt.OnDropHandler += action;
                break;
            case Define.TouchEvent.EndDrag:
                evt.OnEndDragHandler -= action;
                evt.OnEndDragHandler += action;
                break;
            case Define.TouchEvent.OnDrag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            case Define.TouchEvent.Down:
                evt.OnDownHandler -=action;
                evt.OnDownHandler +=action;
                break;
            case Define.TouchEvent.Up:
                evt.OnUpHandler -=action;
                evt.OnUpHandler +=action;
                break;
        }

    }
    // public static void BindTouchEvent(GameObject go, Action<bool> action, Define.TouchEvent type = Define.TouchEvent.Tap)
    // {
    //     BoolEventHandlers evt = GetOrAddComponent<BoolEventHandlers>(go);

    //     switch(type)
    //     {
    //         case Define.TouchEvent.Tap:
    //             evt.OnClickHandler -= action;
    //             evt.OnClickHandler += action;
    //             break;
    //         case Define.TouchEvent.Drag:
    //             evt.OnDragHandler -= action;
    //             evt.OnDragHandler += action;
    //             break;
    //         case Define.TouchEvent.Down:
    //             evt.OnDownHandler -=action;
    //             evt.OnDownHandler +=action;
    //             break;
    //         case Define.TouchEvent.Up:
    //             evt.OnUpHandler -=action;
    //             evt.OnUpHandler +=action;
    //             break;
    //     }

    // }


    // public static Dictionary<int,GameObject> LoadAllToDict(string path)
    // {
    //     Dictionary<int,GameObject> ret_dict = new Dictionary<int, GameObject>();
    //     GameObject[] go_array = Resources.LoadAll<GameObject>(path);
    //     if(go_array == null)
    //     {
    //         Debug.Log("invalid path, maybe :"+path);
    //         return null;
    //     }

    //     for(int i = 0;i < go_array.Length; i++)
    //     {
    //         GameObject temp = UnityEngine.Object.Instantiate(go_array[i],Managers.scene._current_scene._pool_object.transform);
    //         temp.SetActive(false);
    //         ret_dict.Add(i,temp);
    //     }
        
    //     return ret_dict;
    // }

    static public T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }
}
