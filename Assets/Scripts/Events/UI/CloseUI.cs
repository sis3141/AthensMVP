using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUI : UIEvents
{
    public override void Start()
    {
        Utils.BindTouchEvent(gameObject,Close);
    }
}
