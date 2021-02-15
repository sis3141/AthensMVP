using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    void Start()
    {
        Utils.BindTouchEvent(gameObject,MyAddItem);
    }

    void MyAddItem()
    {
        int index = Island_Inventory.current_item;
        Managers.ui._scene_UI_dict[Define.SceneUIType.Inventory].GetComponent<Island_Inventory>().TAddItem();
    }


}
