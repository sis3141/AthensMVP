using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Define;

public class OnOffUI : MonoBehaviour
{
    bool _is_active;
    Transform _target_object;

    public Define.CommonUI _common_ui_type;
    void Start()
    {
        _target_object = Managers.ui._common_UI_dict[_common_ui_type];
        Utils.BindTouchEvent(gameObject,OnOff);
    }

    public void OnOff()
    {
        Debug.Log("onoff detects click");
        _is_active = _target_object.gameObject.activeSelf;
        if(_is_active)
        {
            _target_object.gameObject.SetActive(false);
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
            _target_object.gameObject.SetActive(true);
            _is_active = true;
        }
        Managers.sound.Play_UI();
    }
}
