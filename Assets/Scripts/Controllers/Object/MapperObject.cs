using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStructure;
using Define;
using System;
using UnityEngine.EventSystems;
using System.Linq;

public class MapperObject : MonoBehaviour
{
    public int _planet;
    public int _order;
    MapData_build _map_info;

    int x_length;
    int z_length;

    Dictionary<Vector3Int,GameObject> _obj_dict = new Dictionary<Vector3Int, GameObject>();
    Dictionary<Vector3Int,int> _time_dict = new Dictionary<Vector3Int, int>();
    Dictionary<DBHeader,Dictionary<int,Dictionary<int,int[]>>> _b_dict;
    Vector3 _origin = new Vector3Int();

    Dictionary<int,Dictionary<int,int[]>> _duration;
    Dictionary<int,Dictionary<int,int[]>> _evolution;
    Dictionary<int,Dictionary<int,int[]>> _destroy;
    Dictionary<int,Dictionary<int,int[]>> _num;

    Inventory _inven;

    RaycastHit _hit;
    

    void Start()
    {
        Debug.Log("start mapping");
        _origin = transform.position;
        if(Managers.data._user_DB.quest.dict == null)
            Debug.Log("설마 이거도?");
        if(Managers.data._user_DB.planet.dict == null)
            Debug.Log("시 발 아 좀");
        Debug.Log(Managers.data._user_DB.planet.Count());
        _map_info = Managers.data._user_DB.planet[_planet][_order];
        x_length = _map_info.x_length;
        z_length = _map_info.z_length;
        _b_dict = Managers.data._building_info_dict; 
        _duration = _b_dict[DBHeader.duration];
        _evolution = _b_dict[DBHeader.evolution];
        _destroy = _b_dict[DBHeader.destroy];
        _num = _b_dict[DBHeader.num];
        _inven = Managers.ui._common_UI_dict[CommonUI.Inventory].GetComponent<Inventory>();
        Utils.BindTouchEvent(gameObject,Ondrop,TouchEvent.Drop);
        Utils.BindTouchEvent(gameObject,OnClick);
        Managers._time_invoker += UpgradeBuilding;
        if(Managers._time_invoker == null)
        {
            Debug.Log("왜그러는데 십 련 아");
        }
    }
    public void LoadBuildings()
    {  
        //dictionary<DBHeader,Dictionary<int, string[]>>
        Quaternion rotation = Quaternion.Euler(0,0,0);
        MapData<BuildInfo> map = _map_info;
        BuildInfo temp_b;
        int obj_index;
        for(var z = 0; z<z_length ; z++)
        {
            for(var x = 0; x<x_length; x++)
            {
                temp_b = map.z[z].x[x];
                if(temp_b.I > 0)
                {
                    obj_index = _evolution[temp_b.I][temp_b.L][0];
                    GameObject go = Resources.Load<GameObject>("Prefabs/item/item_b/"+obj_index);
                    Instantiate(go,_origin + new Vector3Int(x,0,z),rotation,transform);
                }
            }
        }
    }

    // Update is called once per frame
    public void AddBuilding(int x, int z, int item_index)
    {
        int time = Managers.data._user_DB.time;
        int[] size = Managers.data._building_DB.I(DBHeader.size);
        x -= (int)_origin.x;
        z -= (int)_origin.z;
        BuildInfo b_info = _map_info.z[z].x[x];
        BuildInfo b_temp;
        Debug.Log("origin index : "+_evolution[item_index][0][0]);
        int obj_index = _evolution[item_index][0][0];
        int obj_size = size[obj_index];
        Debug.Log("building size : "+(obj_size));
        int target_index = b_info.I;
        string outp = "";
        foreach(int s in size)
        {
            outp += s.ToString();
            outp += ' ';
        }
        Debug.Log("size arr : "+outp);
        for(int i = 0; i< obj_size; i++)
        {
            for(int j = 0; j< obj_size; j++)
            {
                b_temp = _map_info.z[z+i].x[x+j];
                Debug.Log("already info: "+b_temp.I);
                if(b_temp.I != 0)
                {
                    Debug.Log(name[Math.Abs(target_index)]+" allready built");
                    return;
                }
            }
        }
        for(int i = 0; i< obj_size; i++)
        {
            for(int j = 0; j< obj_size; j++)
            {
                b_temp = _map_info.z[z+i].x[x+j];
                b_temp.I = -item_index;
                b_temp.L = x;
                b_temp.T = z;
            }
        }

        b_info.I = item_index;
        Debug.Log("after"+b_info.I);
        b_info.L = 0;
        b_info.T = Managers.data._user_DB.time;
        GameObject go = Resources.Load<GameObject>("Prefabs/item/item_b/"+obj_index);
        Vector3Int offset = new Vector3Int(x,0,z);
        int duration = _duration[item_index][0][0];
        if(duration != 0)
        {
            _time_dict.Add(offset, time + duration);
        }
        go = Instantiate(go,_origin+offset,Quaternion.Euler(0,0,0));
        go.transform.SetParent(transform);
        _obj_dict.Add(offset,go);
        _inven.UpdateInventory(1,item_index,-1);


    }
    public void UpgradeBuilding(int time)
    {
        int length = _time_dict.Count;
        Vector3Int[] keys = _time_dict.Keys.ToArray();
        Debug.Log("upgrade check count:"+length);
        for(int i = 0 ; i< length; i++)
        {
            Vector3Int key = keys[i];
            if(_time_dict[key] == time)
            {
                BuildInfo b_info = _map_info.z[key.z].x[key.x];
                b_info.L++;
                int i_index = b_info.I;
                int next_level = b_info.L;
                int next_build = _evolution[i_index][next_level][0];
                Debug.Log("next build : "+next_build);
                int next_time = _duration[i_index][next_level][0];
                GameObject go = _obj_dict[key];
                Destroy(go);
                _obj_dict.Remove(key);
                GameObject new_go = Resources.Load<GameObject>("Prefabs/item/item_b/"+next_build);
                go = Instantiate(new_go, _origin+key,Quaternion.Euler(0,0,0));
                _obj_dict.Add(key,go);
                if(next_time == 0)
                {
                    _time_dict.Remove(key);
                }
                else
                {
                    _time_dict[key] += next_time;
                }
            }
        }
    }

    public void DestroyBuilding(int x, int z)
    {
        int[] size = Managers.data._building_DB.I(DBHeader.size);

        int type;
        int count;
        int obj_size;
        int obj_index;

        int item_index;
        z -= (int)_origin.z;
        x -= (int)_origin.x;
        BuildInfo b_info = _map_info.z[z].x[x];
        item_index = b_info.I;
        if(item_index == 0)
        {
            Debug.Log("no building");
            return;
        }
        if(item_index < 0)
        {
            x = b_info.L;
            z = b_info.T;
            b_info = _map_info.z[z].x[x];
            item_index = b_info.I;
        }

        obj_index = _evolution[item_index][b_info.L][0];
        obj_size = size[obj_index];
        Vector3Int offset = new Vector3Int(x,0,z);
        GameObject go = _obj_dict[offset];
        Destroy(go);

        for(int i = 0; i<obj_size; i++)
        {
            for(int j = 0; j< obj_size ; j++)
            {
                _obj_dict.Remove(offset);
                _time_dict.Remove(offset);
                _map_info.z[z+i].x[x+j].I = 0;
            }
        }

        int level = b_info.L;
        int[] d_items = _destroy[item_index][level];
        int[] d_num = _num[item_index][level];

        for(int i = 0; i<d_items.Length; i++)
        {
            int index = d_items[i];
            if(index != 0)
            {
                type = index%10;
                index /= 10;
                int dec = 1;
                for(; index / dec != 0 ; dec*= 10 );
                index %= (dec/10);

                count = d_num[i];
                Managers.data.UpdateItem(_inven,type,index,count);
            }

        }

    }
        public void Ondrop(PointerEventData evt)
        {
            Debug.Log("drop detect");
            ref DragInfo drag_info = ref Managers.ui._drag_info;
            if(!drag_info.exist || drag_info.type!= 1)
                return;
            else
                drag_info.exist = false;

            Vector3 position = evt.pointerCurrentRaycast.worldPosition;
                Debug.Log("x : "+(int)position.x+", z:"+(int)position.z);
            int x = (int)position.x;
            int z = (int)position.z;

            AddBuilding(x,z,drag_info.index);
            

        }

        public void OnClick(PointerEventData evt)
        {
            Debug.Log("CLicked..");
            Vector3 position = evt.pointerCurrentRaycast.worldPosition;
            int x = (int)position.x;
            int z = (int)position.z;
                Debug.Log("x : "+(int)position.x+", z:"+(int)position.z+"code: "+_map_info.z[z-(int)_origin.z].x[x-(int)_origin.x].I);

            DestroyBuilding(x,z);
        }
}
