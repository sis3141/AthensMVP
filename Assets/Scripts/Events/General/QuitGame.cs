using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : GeneralEvents
{
    public override void Start()
    {
        Utils.BindTouchEvent(gameObject,QuitGame);
    }
}
