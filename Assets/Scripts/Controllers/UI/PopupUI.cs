using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopupUI : MonoBehaviour
{
    protected void OnEnable()
    {
        Managers.ui._popup_count++;
    }

    protected void OnDisable()
    {
        Managers.ui._popup_count--;
    }
}
