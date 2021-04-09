using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using DataStructure;
using Define;

public class Manufact : PopupUI
{
    Transform _background_ui;
    Transform _page_ui;
    Inventory _inven;
    int?[] _index_array;

    string[] _dict_key;

    Transform _result;
    bool _complete;

    int _result_type;
    int _result_index;

    int _key_count;

    // 0 = basic, 1 = blacksmith
    public int _level = 0;

    public int _item_type = 0;
    void Start()
    {
        ref Dictionary<int,Dictionary<string,int?[]>> dict = ref Managers.data._combine_dict;
        _inven = Managers.ui._common_UI_dict[Define.CommonUI.Inventory].GetComponent<Inventory>();
        
        int slot_num = Managers.data._combine_level_slot[_level];
        _index_array = new int?[slot_num];

        _key_count = dict[_level].Keys.Count;
        _dict_key = new string[_key_count];
        dict[_level].Keys.CopyTo(_dict_key,0);

        _background_ui = transform.GetChild(0);
        _page_ui = _background_ui.GetChild(0);
        _result = _background_ui.GetChild(1);
        Utils.BindTouchEvent(gameObject,OnDrop,Define.TouchEvent.Drop);
        Utils.BindTouchEvent(gameObject,OnClick);

        Debug.Log("letme see");
        string s = "";
        for(int i = 0; i<_key_count; i++)
        {
           for(int j = 0; j<9; j++)
           {
               s += dict[_level][_dict_key[i]][j];
               s += ',';
           }
           s+= '\n';
        }
        Debug.Log(s);
    }

    void Compare()
    {
        ref Dictionary<int,Dictionary<string,int?[]>> dict = ref Managers.data._combine_dict;

        string s = "";
        for(int i =0; i<9; i++)
        {
            s += _index_array[i];
            s += ',';
        }
        Debug.Log("Lets Compare : "+s);

        for(int i = 0; i<_key_count; i++)
        {
            if( _index_array.SequenceEqual( dict[_level][_dict_key[i]] ) )
            {
                Debug.Log("got it!!");
                _complete = true;
                _result_type = (int)_dict_key[i][1]-'0';
                _result_index = TempParse(_dict_key[i]);
                ref Dictionary<int,Sprite[]> icon_dict = ref Managers.data.IconDB((int)_result_type);
                _result.GetComponent<Image>().sprite = icon_dict[(int)_result_index/16][(int)_result_index%16];
                return;
            }
        }
        _complete = false;
        _result.GetComponent<Image>().sprite = null;
    }

    void OnDrop(PointerEventData evt)
    {
        Debug.Log("drop detect");
        ref DragInfo drag_info = ref Managers.ui._drag_info;
        if(!drag_info.exist)
            return;
        else
            drag_info.exist = false;
        int slot_index = evt.pointerEnter.transform.GetSiblingIndex();
        Debug.Log("target slot : "+slot_index);
        if(_index_array[slot_index] != null)
            return;
        else
            _index_array[slot_index] = drag_info.index;

        _page_ui.GetChild(slot_index).GetComponent<Image>().sprite = drag_info.obj.GetComponent<Image>().sprite;
        Managers.ui._common_UI_dict[Define.CommonUI.Inventory].GetComponent<Inventory>().UpdateInventory(0,(int)drag_info.index,-1);
        Compare();
    }

    int TempParse(string s)
    {
        int ret = 0;
        for(int i = 2; i< s.Length; i ++)
        {
            ret = ret * 10 + (s[i]-'0');
        }
        return ret;
    }

    void OnClick(PointerEventData evt)
    {
        Debug.Log("click detect!");
        int slot_index = evt.pointerEnter.transform.GetSiblingIndex();
        if(_index_array[slot_index]==null)
            return;
        int item_index = (int)_index_array[slot_index];
        _index_array[slot_index] = null;
        _page_ui.GetChild(slot_index).GetComponent<Image>().sprite = null;
        Managers.ui._common_UI_dict[Define.CommonUI.Inventory].GetComponent<Inventory>().UpdateInventory(_item_type,item_index,+1);
            
        Compare();

    }

    public void GetResult()
    {
        if(_complete)
        {
            _inven.UpdateInventory(_result_type,_result_index,1);
            for(int i = 0; i<_index_array.Length; i++)
            {
                int? item_nulla = _index_array[i];
                if(item_nulla != null)
                {
                    int item_index = (int)item_nulla;
                    _index_array[i] = null;
                    _page_ui.GetChild(i).GetComponentInParent<Image>().sprite = null;
                }
            }
            _result.GetComponent<Image>().sprite = null;
            _complete = false;
            //_index_array.
        }
        else
            return;
    }

    public void Reset()
    {
        for(int i = 0; i<_index_array.Length; i++)
        {
            int? item_nulla = _index_array[i];
            if(item_nulla != null)
            {
                int item_index = (int)item_nulla;
                _index_array[i] = null;
                _page_ui.GetChild(i).GetComponentInParent<Image>().sprite = null;
                _inven.UpdateInventory(0,item_index,1);
            }
        }
        _result.GetComponent<Image>().sprite = null;
        _complete = false;

    }

    // Update is called once per frame
    
}
