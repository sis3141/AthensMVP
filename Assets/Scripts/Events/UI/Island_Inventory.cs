using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Island_Inventory : MonoBehaviour
{
    GameObject _item_set;
    void Start()
    {
        _item_set = gameObject;
        for(int i = 0; i < Managers.data._user.item_count; i++)
        {
            _item_set.transform.GetChild(i).GetComponent<Image>().enabled = true;
        }
    }
}
