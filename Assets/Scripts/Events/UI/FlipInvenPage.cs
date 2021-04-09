using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipInvenPage : MonoBehaviour
{
    // Start is called before the first frame update
    public bool _next;

    public bool _inven_option;
    void Start()
    {
        if(_inven_option)
            Utils.BindTouchEvent(gameObject,InvenFlip);
        else
            Utils.BindTouchEvent(gameObject,DictFlip);
    }

    // Update is called once per frame
    void InvenFlip()
    {
        Managers.ui._common_UI_dict[Define.CommonUI.Inventory].GetComponent<Inventory>().FlipPage(_next);
    }

    void DictFlip()
    {
        Managers.ui._common_UI_dict[Define.CommonUI.Dictionary].GetComponent<Inventory>().FlipPage(_next);
    }
}
