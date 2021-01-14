using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    //스택
    Stack<Canvas> PopupStack = new Stack<Canvas>();
    int _sort_order = 0;

    public enum UIType
    {
        Popup,
        Scene,
    }

    //로드
    public GameObject LoadUI(string path)
    {
        GameObject _go = Managers.resource.Load<GameObject>(path);
        return _go;
    }
    //열기
    public Canvas OpenNewUI(string path, UIType type)
    {
        Canvas _canvas = Managers.resource.Instantiate<Canvas>(path);
        if(type == UIType.Popup)
        {
            _sort_order++;
            _canvas.sortingOrder = _sort_order;
        }
        return _canvas;
    }
    //닫기
    public bool CloseUI(Canvas _canvas,bool forced = false)
    {
        if(_canvas == null)
        {
            Debug.Log("No such UI!");
            return false;
        }
        if(_canvas.sortingOrder < _sort_order)
        {
            Debug.Log("Wrong order!");
            return false;
        }
        Managers.resource.Destroy(_canvas);
        _sort_order--;
        return true;
    }

    public void ToggleRayCast(Graphic _graphic, bool _active)
    {
        
        if(_graphic == null) 
        {   
            Debug.Log("No UI element");
            return;
        }
        string name = _graphic.ToString();
        _graphic.raycastTarget = _active;
        Debug.Log($"raycast of {name} set {_active}");
    }


}
