using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStructure;
using Define;
using System;

public class MapperObject : MonoBehaviour
{
    public int _planet;
    public int _order;
    MapInfo _map_info;

    int x_length;
    int z_length;

    Dictionary<Vector3Int,GameObject> _obj_dict;

    Dictionary<DBHeader,Dictionary<int,string[]>> _b_dict;
    Vector3 _origin = new Vector3Int();

    Dictionary<int,string[]> _duration;
    Dictionary<int,string[]> _evolution;
    Dictionary<int,string[]> _destroy;
    Dictionary<int,string[]> _num;

    Inventory _inven;
    

    void Start()
    {
        _origin = transform.position;
        _map_info = Managers.data._user_DB.planet[_planet][_order];
        x_length = _map_info.x_length;
        z_length = _map_info.z_length;
        _b_dict = Managers.data._building_info_dict; 
        _duration = _b_dict[DBHeader.duration];
        _evolution = _b_dict[DBHeader.evolution];
        _destroy = _b_dict[DBHeader.destroy];
        _num = _b_dict[DBHeader.num];
        _inven = Managers.ui._common_UI_dict[CommonUI.Inventory].GetComponent<Inventory>();
    }
    public void LoadBuildings()
    {  
        //dictionary<DBHeader,Dictionary<int, string[]>>
        Quaternion rotation = Quaternion.Euler(0,0,0);
        MapData<BuildInfo> map = _map_info.data;
        BuildInfo temp_b;
        int obj_index;
        for(var z = 0; z<z_length ; z++)
        {
            for(var x = 0; x<x_length; x++)
            {
                temp_b = map.z[z].x[x];
                if(temp_b.index > 0)
                {
                    obj_index = int.Parse(_evolution[temp_b.index][temp_b.level]);
                    GameObject go = Resources.Load<GameObject>("Prefabs/item/item_b/"+obj_index);
                    Instantiate(go,_origin + new Vector3Int(x,0,z),rotation,transform);
                }
            }
        }
    }

    // Update is called once per frame
    public void AddBuilding(int x, int z, int item_index)
    {
        int[] size = Managers.data._building_DB.I(DBHeader.size);
        BuildInfo b_info = _map_info.data.z[z].x[x];
        BuildInfo b_temp;
        int obj_index = int.Parse(_evolution[item_index][0]);
        int obj_size = size[obj_index];
        int target_index = b_info.index;
        for(int i = z; i< obj_size; i++)
        {
            for(int j = x; j< obj_size; j++)
            {
                b_temp = _map_info.data.z[z+i].x[x+j];
                if(b_temp.index != 0)
                {
                    Debug.Log(name[Math.Abs(target_index)]+" allready built");
                    return;
                }
                b_temp.index = -obj_index;
                b_temp.level = x;
                b_temp.time = z;
            }
        }

        b_info.index = obj_index;
        b_info.level = 0;
        b_info.time = Managers.data._user_DB.time;
        GameObject go = Resources.Load<GameObject>("Prefabs/item/item_b/"+obj_index);
        Vector3Int offset = new Vector3Int(x,0,z);
        go = Instantiate(go,_origin+offset,Quaternion.Euler(0,0,0),transform);
        _obj_dict.Add(offset,go);


    }

    public void DestroyBuilding(int x, int z)
    {
        int[] size = Managers.data._building_DB.I(DBHeader.size);

        int type;
        int index;
        int count;
        int obj_size;
        int obj_index;

        int item_index;
        BuildInfo b_info = _map_info.data.z[z].x[x];
        item_index = b_info.index;
        if(item_index == 0)
        {
            Debug.Log("no building");
            return;
        }
        if(item_index < 0)
        {
            x = b_info.level;
            z = b_info.time;
            b_info = _map_info.data.z[z].x[x];
            item_index = b_info.index;
        }

        obj_index = int.Parse(_evolution[item_index][b_info.level]);
        obj_size = size[obj_index];

        for(int i = 0; i<obj_size; i++)
        {
            for(int j = 0; j< obj_size ; j++)
            {
                Vector3Int offset = new Vector3Int(x+j,0,z+i);
                GameObject go = _obj_dict[offset];
                Destroy(go);
                _obj_dict.Remove(offset);
                _map_info.data.z[z+i].x[x+j].index = 0;
            }
        }

        int level = b_info.level;
        string item_code = _destroy[item_index][level];
        string[] d_items = item_code.Split(',');
        string num_code = _num[item_index][level];
        string[] d_num = num_code.Split(',');

        for(int i = 0; i<d_items.Length; i++)
        {
            type = d_items[i][0]-'0';
            if(d_items.Length == 0)
                break;
            d_items[i].Remove(0,1);
            index = int.Parse(d_items[i]);
            count = int.Parse(d_num[i]);
            Managers.data.UpdateItem(_inven,type,index,count);

        }
       

    



    }
}
