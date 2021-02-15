using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Buildings : MonoBehaviour
{
    bool _moving;
    GameObject _move_component;
    void Start()
    {
        _move_component = Managers.scene._current_scene._pool[0];
        Utils.BindTouchEvent(gameObject,ToggleActivity);
    }

    void ToggleActivity()
    {
        if(_moving)
        {
            _move_component.transform.SetParent(null);   //pool[0] :the gameobject which controlls moving
            _move_component.SetActive(false);
            _moving = false;
            Debug.Log("stop moving");
        }
        else
        {
            _move_component.transform.SetParent(transform);
            _move_component.SetActive(true);
            _move_component.transform.position = gameObject.transform.position + Vector3.forward;
            _moving = true;
            Camera.main.GetComponent<ZoomControll>()._lock = true;
            Debug.Log("start moving"+_move_component.name);
        }
    }
}
