using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStructure;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using Define;

public class LoadMap : MonoBehaviour
{
    // public int _last_x;
    // public int _last_z;

    // public int _next_x;
    // public int _next_z;
    // GameObject FakePool;
    // GameObject _map_parent;
    // MapData _map;
    // void Start()
    // {
    //     FakePool = Managers.resource.Load<GameObject>("Prefabs/Base_field_block");
    //     //Managers.ui.LoadSceneUI();

    //    InitiateMap();
        
    // }

    // public void GetNextPosition()
    // {
    //     if(_last_x == 0)
    //     {
    //         _next_x = _last_z + 1;
    //         _next_z = 0;
    //         Managers.data._user.map_size++;
    //         return;
    //     }

    //     if(_last_z < _last_x)
    //     {
    //         _next_x = _last_x;
    //         _next_z = _last_z + 1;
    //     }
    //     else
    //     {
    //         _next_x = _last_x - 1;
    //         _next_z = _last_z;
    //     }
        
    //     Debug.Log($"next position x = {_next_x}, z= {_next_z}");

    //     return;
    // }

    // public void GetLastPosition()
    // {
    //     int size = Managers.data._user.map_size;
    //     int side_blocks = Managers.data._user.book_count - Managers.data._user.book_money + 16 - (size-1)*(size-1);

    //     if(side_blocks > size)
    //     {
    //         _last_z= size-1;
    //         _last_x = size*2 - side_blocks- 1;
    //     }
    //     else
    //     {
    //         _last_x = size-1;
    //         _last_z= side_blocks - 1;
    //     }
    // }

    // public void InitiateMap()
    // {
    //     int map_size = Managers.data._user.map_size;
    //     int itemcode = 0;
    //     //GetNextPosition();
    //     //for(int i = 0; i < 15; i++)
    //     // {
    //     //     Debug.Log(i+" : x ="+_next_x+", z="+_next_z);
    //     //     GetNextPosition();
    //     //     _last_x = _next_x;
    //     //     _last_z = _next_z;
    //     // }
    //     Vector3Int position = new Vector3Int();
    //     Quaternion rotation = new Quaternion();
    //     _map_parent = gameObject;
    //     for(int z = 0; z < map_size; z++)
    //     {
    //         for(int x = 0; x< map_size; x++)
    //         {
    //             position = new Vector3Int(x,0,z);
    //             itemcode = Managers.data._map.z[z].x[x];
    //             if(itemcode > 0)
    //                 UnityEngine.Object.Instantiate(FakePool,position,rotation,_map_parent.transform);
    //         }
    //     }
    //     GetLastPosition();
    //     Debug.Log("x :"+_last_x);
    //     Debug.Log("z :"+_last_z);
    //     Debug.Log("map length :"+map_size);
    //     Debug.Log("left field : "+Managers.data._user.book_money);
    // }
    

    // public void Add_Field(PointerEventData evt)
    // {
    //     if(Managers.data._user.book_money == 0)
    //         return;
    //     Managers.data._user.book_money--;
        
    //     GetNextPosition();
    //     Managers.data._map.z[_next_z].x[_next_x] = 1;

    //     Quaternion rotation = new Quaternion();
    //     Vector3Int position = new Vector3Int(_next_x,0,_next_z);
    //     UnityEngine.Object.Instantiate(FakePool,position,rotation,_map_parent.transform);
    //     _last_x = _next_x;
    //     _last_z = _next_z;
    //     Debug.Log("field added!");
    //     ///////

    // }

   

    


}
