using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTargetUI : MonoBehaviour
{
    Transform _target_object;
    public Define.CommonUI _common_UI_type;
    void Start()
    {
        _target_object = Managers.ui._common_UI_dict[_common_UI_type];
        Utils.BindTouchEvent(gameObject,Close);
        Debug.Log("UI name :"+_target_object.name);
    }

    public void Close()
    {
        _target_object.gameObject.SetActive(false);
    }
}
