using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : ObjectEvents
{
    public override void Start()
    {
        Utils.BindTouchEvent(gameObject,Destroy);
    }

}
