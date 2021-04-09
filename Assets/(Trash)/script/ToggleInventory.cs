using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// public class ToggleInventory : MonoBehaviour
// {
//     Transform _inven_ui;
//     public bool _inven_option;
//     public int type;
//     void Start()
//     {
//         if(_inven_option)
//             _inven_ui = Managers.ui._scene_UI_dict[Define.SceneUIType.Inventory].GetChild(0);
//         else
//             _inven_ui = Managers.ui._scene_UI_dict[Define.SceneUIType.Dictionary].GetChild(0);
//         Utils.BindTouchEvent(gameObject,Toggle);
//     }

//     void Toggle()
//     {
//         Debug.Log("toggleing");
//         if(_inven_ui.GetChild(type).gameObject.activeSelf)
//             return;
//         for(int i = 0; i < _inven_ui.childCount; i++)
//         {
//             _inven_ui.GetChild(i).gameObject.SetActive(false);
//         }
//         _inven_ui.GetChild(type).gameObject.SetActive(true);

//         //_target.GetComponent<Inventory>().TogglePage(page_option);
//     }
// }
