using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ClosableUI : MonoBehaviour
{
    void Start()
    {
        Utils.BindTouchEvent(gameObject,CloseUI);
    }

    void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
