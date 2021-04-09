using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectInvenTab : MonoBehaviour
{
    // Start is called before the first frame update
    Transform target;
    public bool _inven_option;

    public int tab_num;
    void Start()
    {
        if(_inven_option)
            target = Managers.ui._common_UI_dict[Define.CommonUI.Inventory];
        else
            target = Managers.ui._common_UI_dict[Define.CommonUI.Dictionary];

        Utils.BindTouchEvent(gameObject,Select);
    }

    // Update is called once per frame
    void Select()
    {
        target.GetComponent<Inventory>().OpenTab(tab_num);
    }
}
