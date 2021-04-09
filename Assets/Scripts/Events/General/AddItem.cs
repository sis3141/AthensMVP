using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    void Start()
    {
        Utils.BindTouchEvent(gameObject,Add);
    }

    void Add()
    {
        Managers.ui._common_UI_dict[Define.CommonUI.Inventory].GetComponent<Inventory>().AddItem();
    }


}
