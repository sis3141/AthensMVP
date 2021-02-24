using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUI : MonoBehaviour
{
    GameObject _target_object;

    public Define.SceneUIType _scene_UI_type;
    void Start()
    {
        _target_object = Managers.ui._scene_UI_dict[_scene_UI_type];
        Utils.BindTouchEvent(gameObject,Close);
        Debug.Log("UI name :"+_target_object.name);
    }

    public void Close()
    {
        _target_object.SetActive(false);
    }
}
