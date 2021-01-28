using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddField : MonoBehaviour
{
    LoadMap _map;
    void Start()
    {
       _map = GameObject.Find("Map").GetComponent<LoadMap>();
       Utils.BindTouchEvent(gameObject,_map.Add_Field);
    }

    
}
