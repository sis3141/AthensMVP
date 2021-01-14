using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUI : UIEvents
{
    CloseUI temp = new CloseUI();
    void Start()
    {
        Utils.BindTouchEvent(this.gameObject,Close);
    }
}
