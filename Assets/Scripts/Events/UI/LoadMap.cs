using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadMap : PopupUI
{
    // Start is called before the first frame update
    public int index;

    void Start()
    {
        Utils.BindTouchEvent(gameObject,Load);

    }

    void Load()
    {
        Managers.scene.LoadMap(index);
    }

    // Update is called once per frame
    
}
