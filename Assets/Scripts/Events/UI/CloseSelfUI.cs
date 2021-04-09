using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CloseSelfUI : MonoBehaviour
{
    GameObject _self;
    void Start()
    {
        _self = gameObject.GetComponentInParent<Canvas>().gameObject;
        Utils.BindTouchEvent(gameObject,CloseUI);
    }

    void CloseUI()
    {
        _self.SetActive(false);
        Managers.sound.Play_UI();
    }
}