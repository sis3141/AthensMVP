using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStructure;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class LoadMap : MonoBehaviour
{
    public int _last_x;
    public int _last_z;

    public int _next_x;
    public int _next_z;
    MapData _map;
    UserData _user;
    GameObject FakePool;
    GameObject _inven_item;
    void Start()
    {
        LoadUserData();
        LoadMapData();
        InitiateMap();
        GetNextPosition();
        Utils.BindTouchEvent(gameObject,CompleteReadBook);
        _inven_item = GameObject.Find("Content");
        
    }

    public void GetNextPosition()
    {
        if(_last_x == 0)
        {
            _next_x = _last_z + 1;
            _next_z = 0;
            return;
        }

        if(_last_z < _last_x)
            _next_z = _last_z + 1;
        else
            _next_x = _last_x - 1;

        return;
    }

    public void LoadMapData()
    {
        TextAsset text = Managers.resource.Load<TextAsset>("Data/MapData");
        _map = JsonUtility.FromJson<MapData>(text.text);


        List<Dictionary<string,object>> _item_info = CSVReader.Read("Data/ItemInfo");

        FakePool = Managers.resource.Load<GameObject>("Prefabs/Base_field_block");
    }

    public void LoadUserData()
    {
        TextAsset text = Managers.resource.Load<TextAsset>("Data/UserData");
        _user = JsonUtility.FromJson<UserData>(text.text);
        _last_x = _user.map_info.x;
        _last_z = _user.map_info.z;
    }

    public void InitiateMap()
    {
        int map_length = (_last_x >= _last_z) ? _last_x : _last_z;
        map_length++;
        int itemcode = 0;
        Vector3Int position = new Vector3Int();
        Quaternion rotation = new Quaternion();
        for(int z = 0; z < map_length; z++)
        {
            for(int x = 0; x< map_length; x++)
            {
                position = new Vector3Int(x,0,z);
                itemcode = _map.mapinfo[z].x[x];
                if(itemcode > 0)
                    UnityEngine.Object.Instantiate(FakePool,position,rotation);
            }
        }
        Debug.Log("x :"+_last_x);
        Debug.Log("z :"+_last_z);
        Debug.Log("map length :"+map_length);
    }
    public void CompleteReadBook(PointerEventData evt)
    {
        Add_Field();
        _user.book_count++;
        int count = _user.book_count;
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

    public void Add_Field()
    {
        _map.mapinfo[_next_z].x[_next_x] = 1;
        Quaternion rotation = new Quaternion();
        Vector3Int position = new Vector3Int(_next_x,0,_next_z);
        UnityEngine.Object.Instantiate(FakePool,position,rotation);
        _last_x = _next_x;
        _last_z = _next_z;
        GetNextPosition();
    }

    public void ObtainNewItem()
    {
        _user.item_count++;
        GameObject slot = _inven_item.transform.GetChild(_user.item_count).gameObject;
        slot.GetComponent<Image>().enabled = true;
        //change inventory's nth item to active
    }

    


}
