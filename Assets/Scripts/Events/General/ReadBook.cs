using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReadBook : MonoBehaviour
{
    //GameObject _inven_item;

    void Start()
    {
      // _inven_item = Managers.ui._scene_UI_dict[Define.SceneUIType.Inventory].transform.Find("Scroll View/Viewport/Content").gameObject;
       Utils.BindTouchEvent(gameObject,CompleteReadBook);
    }

    public void CompleteReadBook(PointerEventData evt)
    {
        Managers.data._user.book_count++;
        Managers.data._user.book_money++;
        int count = Managers.data._user.book_count;
        if(count < 3)
            Debug.Log($"Tutorial event {count} !");
        // else if(count%10 == 0)
        //     Debug.Log($"Special event {count/10} !");
        else if(count%5 == 0)
        {
            Debug.Log($"Normal event {count/5} !");
            ObtainNewItem();
        }
    }

     public void ObtainNewItem()
    {
        Managers.data._user.item_count++;
        //GameObject slot = _inven_item.transform.GetChild(Managers.data._user.item_count).gameObject;
        //slot.GetComponent<Image>().enabled = true;
        //change inventory's nth item to active
    }
}
