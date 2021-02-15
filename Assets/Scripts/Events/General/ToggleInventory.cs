using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInventory : MonoBehaviour
{
    void Start()
    {
        GameObject target = Managers.ui._scene_UI_dict[Define.SceneUIType.Inventory];
        Utils.BindTouchEvent(gameObject,target.GetComponent<Island_Inventory>().TogglePage);
    }
}
