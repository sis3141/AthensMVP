using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : GeneralEvents
{
    public override void Start()
    {
        Utils.BindTouchEvent(gameObject,MoveScene);
    }
}
