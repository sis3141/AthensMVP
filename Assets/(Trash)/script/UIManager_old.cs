// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class UIManager_old
// {
//     //스택
//     Stack<Canvas> _popup_stack = new Stack<Canvas>();
//     int _sort_order = 0;

//     public enum UIType
//     {
//         Popup,
//         Scene,
//     }
//     //로드
//     public GameObject LoadUI(string path)
//     {
//         GameObject go = Resources.Load<GameObject>(path);
//         return go;
//     }
//     //열기
//     public Canvas OpenNewUI(string path, UIType type)
//     {
//         Canvas canvas = Managers.resource.Instantiate<Canvas>(path);
//         if(type == UIType.Popup)
//         {
//             _sort_order++;
//             canvas.sortingOrder = _sort_order;
//         }
//         return canvas;
//     }
//     //닫기
//     public bool CloseUI(Canvas canvas,bool forced = false)
//     {
//         if(canvas == null)
//         {
//             Debug.Log("No such UI!");
//             return false;
//         }
//         if(canvas.sortingOrder < _sort_order)
//         {
//             Debug.Log("Wrong order!");
//             return false;
//         }
//         Managers.resource.Destroy(canvas);
//         _sort_order--;
//         return true;
//     }

//     public void ToggleRayCast(Graphic graphic, bool active)
//     {
//         if(graphic == null) 
//         {   
//             Debug.Log("No UI element");
//             return;
//         }
//         string name = graphic.ToString();
//         graphic.raycastTarget = active;
//         Debug.Log($"raycast of {name} set {active}");
//     }


// }
