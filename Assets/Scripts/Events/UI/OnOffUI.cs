using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnOffUI : MonoBehaviour
{
    bool _is_active;
    GameObject _target_object;
    void Start()
    {
        _target_object = GameObject.Find("UI_world_Inventory");
        Utils.BindTouchEvent(gameObject,OnOff);
        _is_active = _target_object.activeSelf;
        Debug.Log("UI name :"+_target_object.name);
    }

    public void OnOff(PointerEventData evt)
    {
        if(_is_active)
        {
            _target_object.SetActive(false);
            _is_active = false;
        }
        else
        {
            _target_object.SetActive(true);
            _is_active = true;
        }
    }
}
