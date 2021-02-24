using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Define;

public class OnOffUI : MonoBehaviour
{
    bool _is_active;
    GameObject _target_object;

    public Define.SceneUIType _scene_UI_type;
    void Start()
    {
        _target_object = Managers.ui._scene_UI_dict[_scene_UI_type];
        Utils.BindTouchEvent(gameObject,OnOff);
        _is_active = _target_object.activeSelf;
        Debug.Log("UI name :"+_target_object.name);
    }

    public void OnOff()
    {
        if(_is_active)
        {
            _target_object.SetActive(false);
            _is_active = false;
        }
        else
        {
            // int count = Managers.ui._scene_UI_dict.Keys.Count;
            // SceneUIType[] key_index = new SceneUIType[count];
            // Managers.ui._scene_UI_dict.Keys.CopyTo(key_index,0);
            // for(int i = 0;i<count; i++)
            // {
            //     SceneUIType key =  key_index[i];
            //     Managers.ui._scene_UI_dict[key].SetActive(false);
            // }
            _target_object.SetActive(true);
            _is_active = true;
        }
    }
}
