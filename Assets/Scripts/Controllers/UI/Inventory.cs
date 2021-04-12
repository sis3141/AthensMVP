using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Define;
using DataStructure;
using System;
using UnityEngine.EventSystems;
using System.Linq;
//인벤토리 밑에 페이지, 페이지밑에 슬롯이있는구조, 인벤토리 위치에 스크립트 붙일것.
public class Inventory : PopupUI
{
    public int _dict_slot = 16;
    Transform _inventory;
    Transform _background;
    Transform _tabs;
    GameObject _preset;
    GameObject _slot;
    Transform _page_num_ui;

    Transform _pages;
    // Sprite[] _icon_image;
    //어떻게 전달할거?
    int type_line;

    public bool _inven_option;

    public int _curr_type = 0;
    public int _curr_tab = 0;
    public int _curr_page = 0;
    float _time;

    public int _max_tab = 4;
    public int[] _max_page = new int[4]{2,2,2,2};
    public int[] _max_slot = new int[4]{16,16,16,16};
    public int[][] _code = new int[][]
    {
        new int[]{0,0,0,1,1,1},
        new int[]{2,3,3}
    };

    void Awake()
    {
        _inventory = transform;
        _background = _inventory.GetChild(0);
        _page_num_ui = _background.GetChild(1);
        _tabs = _background.GetChild(0);
        _preset = _inventory.GetChild(1).gameObject;
        _slot = _inventory.GetChild(2).gameObject;
        //Utils.BindTouchEvent(gameObject,ShowItemInfo);
        Utils.BindTouchEvent(gameObject,StartDrag,Define.TouchEvent.Down);
        Utils.BindTouchEvent(gameObject,OnDrag,TouchEvent.OnDrag);
        Utils.BindTouchEvent(gameObject,EndDrag,TouchEvent.Up);
        if(_inven_option)
            LoadInventory(_max_tab,_max_page,_max_slot,_code);
        else
            LoadDictionary();
    }

    public void LoadDictionary(int types = 2)
    {
        if(_tabs.childCount != 0)
            return;
        //csv에서 모든 아이템로드, 터치하면 아이템 추가등의 동작으로 테스트용도
        this._max_page = new int[2];
        GameObject ui = Instantiate(_tabs.gameObject);
        for(int t = 0; t<types; t++)
        {
            ref Dictionary<int,Sprite[]> icon_dict = ref Managers.data.IconDB(t);
            GameObject new_tab = Instantiate(ui,_tabs.transform);
            new_tab.name = "tab"+t;
            int count = 0;
            foreach(int key in icon_dict.Keys)
            {
                count += icon_dict[key].Length;
            }
            int max_page = count/_dict_slot;
            _max_page[t] = max_page;
            for(var p = 0; p < max_page; p++)
            {
                //GameObject new_page = new GameObject("page: "+p);
                GameObject new_page = Instantiate(_preset,new_tab.transform);
                new_page.GetComponent<RectTransform>().position = _background.GetComponent<RectTransform>().position;
                 
                for(int i = 0; i<_dict_slot; i++)
                {
                    GameObject new_slot = Instantiate(_slot,new_page.transform);
                    Image image = Utils.GetOrAddComponent<Image>(new_slot.GetComponentInChildren<Image>());
                    int index = p*16 + i;
                    if(index<count)
                    {
                        image.sprite = icon_dict[p][i];
                    }
                    else
                        image.sprite = null;
                }
                new_page.gameObject.SetActive(false);
            }
            new_tab.transform.GetChild(0).gameObject.SetActive(true);
            new_tab.gameObject.SetActive(false);
        }
            _tabs.GetChild(0).gameObject.SetActive(true);
            Destroy(ui);
            Destroy(_preset);
            Destroy(_slot);
            Debug.Log("Dictionary loaded");
    }
    public void MakeInven(int tap_num, int[] max_page, int[] _max_slot)
    {
        ref Dictionary<int,O_Dictionary<int,ItemInfo>> _dict = ref Managers.data._category_inven;
        bool first = !_dict.ContainsKey(0);
        //GameObject ui = Instantiate(_tabs.gameObject);
        for(var i = 0; i< tap_num ; i++)
        {
            GameObject new_tab = _tabs.transform.GetChild(i).gameObject;
            new_tab.name = "tab "+i;
            Debug.Log("tab create "+i);
            if(first)
                _dict.Add(i,new O_Dictionary<int, ItemInfo>());
            for(var j = 0; j<max_page[i]; j++)
            {
               // GameObject new_page = Instantiate(_preset.transform.GetChild(j).gameObject,new_tab.transform);
                GameObject new_page = Instantiate(_preset.transform.GetChild(j).gameObject,new_tab.transform);
                new_page.name = "page "+j;
                Debug.Log("page create"+j);
                for(var k = 0; k<_max_slot[i]; k++)
                {
                    GameObject new_slot = Instantiate(_slot, new_page.transform);
                    new_slot.name = j+"_"+k;
                    new_slot.transform.GetChild(0).name = new_slot.name;
                }
                new_page.gameObject.SetActive(false);
            }
            new_tab.transform.GetChild(0).gameObject.SetActive(true);
            new_tab.gameObject.SetActive(false);
        }
        _tabs.GetChild(0).gameObject.SetActive(true);
       // Destroy(ui);
        Destroy(_preset);
        Destroy(_slot);
        string s = "";
        foreach(int key in _dict.Keys)
        {
            s += key.ToString();
            s += " ";
        }
    }
    public void LoadInventory(int tap_num, int[] max_page, int[] max_slot, int[][] code)
    {
       MakeInven(tap_num,max_page,max_slot);
       ref Dictionary<int,O_Dictionary<int,ItemInfo>> _dict = ref Managers.data._category_inven;
        //bool first = !_dict.ContainsKey(0);
        int type_count = code.Length;
        for(int t = 0; t< type_count; t++)
        {
            ref ItemInfoDict user_item = ref Managers.data.UserItem(t);
            ref CSVDict item_DB = ref Managers.data.ItemDB(t);
            ref Dictionary<int,Sprite[]> icon_dict = ref Managers.data.IconDB(t);

            foreach(KeyValuePair<int,ItemInfo> set in user_item.dict)
            {
                int tap = code[t][set.Value.type];
               // if(first)
                _dict[tap].Add(set.Key,set.Value);
                int length = _dict[tap].Count-1;
                int page_n = length/max_slot[tap];
                int slot_n = length%max_slot[tap];
                Transform slot = _tabs.GetChild(tap).GetChild(page_n).GetChild(slot_n);
                Image image = slot.GetComponentInChildren<Image>();
                image.sprite = icon_dict[set.Key/16][set.Key%16];
                slot.GetComponentInChildren<Text>().text = set.Value.count.ToString();
            }
        }

        type_line = code[0].Max();
    }


    public void OpenPage(int page, bool forced = false)
    {
        
        if(page > _max_page[_curr_tab] || page < 0)
        {
            return;
        }   
        if(forced)
        {
            foreach(Transform ch in _tabs.GetChild(_curr_tab))
            {
                ch.gameObject.SetActive(false);
            }
        }
        else
        {
            if(page == _curr_page)
            return;
            _tabs.GetChild(_curr_tab).GetChild(_curr_page).gameObject.SetActive(false);
        }
        
        _tabs.GetChild(_curr_tab).GetChild(page).gameObject.SetActive(true);
        _curr_page = page;
        _page_num_ui.GetComponentInChildren<Text>().text = (page+1).ToString();

    }
    public void OpenTab(int tab, bool forced = false)
    {
        Debug.Log("try open "+tab);
        Debug.Log("시발좀"+_tabs.childCount);
        
        if(tab > _max_tab || tab < 0)
        {
            Debug.Log("no that tab");
            return;
        }
        if(forced)
        {
            foreach(Transform ch in _tabs)
            {
                ch.gameObject.SetActive(false);
            }
        }
        else
        {
            if(tab == _curr_tab)
            return;
            _tabs.GetChild(_curr_tab).gameObject.SetActive(false);
        }
        _tabs.GetChild(tab).gameObject.SetActive(true);
        _curr_tab = tab;
        if(tab > type_line)
            _curr_type = 1;
        else
            _curr_type = 0;
        OpenPage(0,true);
    }

    public void UpdateInventory(int type, int index, int count)
    {
        //ref Dictionary<int,ItemInfo> user_item = ref Managers.data.Inventory(type);
        ref ItemInfoDict user_item = ref Managers.data.UserItem(type);
        ref CSVDict DB = ref Managers.data.ItemDB(type);
        ref Dictionary<int,O_Dictionary<int,ItemInfo>> _dict = ref Managers.data._category_inven;
        // Debug.Log("타입은 허허: "+type);
        // if(user_item == null)
        //     Debug.Log("왜 시발?");
        int left = 0;
        int key_type = DB.I(DBHeader.type, index);
        int c_tab = _code[type][key_type];
        Debug.Log("target tab");
        int key_index = 0;
        bool exist = _dict[c_tab].ContainsKey(index);

        if(exist)
        {
            key_index = _dict[c_tab].IndexOf(index);
            left = _dict[c_tab][index].count + count;
        }
          //현재슬롯                    탭               페이지              슬롯
        int max_page = _max_page[c_tab];
        int max_slot = _max_slot[c_tab]; 
        int c_page = key_index/16;
        int c_slot = key_index%16;
        Transform slot = _tabs.GetChild(c_tab).GetChild(c_page).GetChild(c_slot);
        if(count > 0)
        {
            if(!exist)
            {//신규
                ref Dictionary<int,Sprite[]> icons = ref Managers.data.IconDB(type);
                slot = _tabs.GetChild(c_tab).GetChild(_dict[c_tab].Count/16).GetChild(_dict[c_tab].Count%16);
                slot.GetComponentInChildren<Image>().sprite = icons[index/16][index%16];
                slot.GetComponentInChildren<Text>().text = count.ToString();
                left = count;
                _dict[c_tab].Add(index,new ItemInfo(count,key_type));
                user_item.Add(index,new ItemInfo(count,key_type));

                string s = "";
                foreach(int key in _dict[c_tab].Keys)
                {
                    s += key;
                    s += ",";
                }
                Debug.Log("added "+s);
            }
            else
            {//증가
                user_item[index].Add(count);
                _dict[c_tab][index].Add(count);
                slot.GetComponentInChildren<Text>().text = left.ToString();
            }
        }
        else
        {
            if(!exist)
            {//오류: 없는거 삭제시도
                Debug.Log("nothing to delete");
                return;
            }

            if(left < 0)
            {//오류: 있는거보다 많이 삭제시도
                Debug.Log("can't remove more than you have!");
                return;
            }
            else if(left == 0)
            {//제거 : 항목빼줘야 하는 상황
                user_item.dict.Remove(index);
                _dict[c_tab].Remove(index);
                Transform cur_tab = _tabs.GetChild(c_tab);
                Transform cur_page = cur_tab.GetChild(c_page);

                slot.GetComponentInChildren<Text>().text = null;
                Image image = slot.GetComponentInChildren<Image>();
                image.sprite = null;
                image.rectTransform.SetParent(cur_tab.GetChild(max_page-1).GetChild(max_slot-1));
                image.rectTransform.anchoredPosition = new Vector2(0,0);
                for(int s = (c_slot)+1; s<max_slot; s++)
                {
    
                    Image cur_page_img = cur_page.GetChild(s).GetComponentInChildren<Image>();
                    cur_page_img.rectTransform.SetParent(cur_page.GetChild(s-1));
                    cur_page_img.rectTransform.anchoredPosition = new Vector2(0,0);

                }
                for(int p = (c_page)+1; p<max_page; p++)
                {
                    Transform other_page = cur_tab.GetChild(p);
                    Image cross_img = other_page.GetChild(0).GetComponentInChildren<Image>();
                    cross_img.rectTransform.SetParent(cur_tab.GetChild(p-1).GetChild(max_slot-1));
                    cross_img.rectTransform.anchoredPosition = new Vector2(0,0);
                    for(int s = 1; s< max_slot; s++)
                    {
                        Image other_p_img = other_page.GetChild(s).GetComponentInChildren<Image>();
                        other_p_img.rectTransform.SetParent(other_page.GetChild(s-1));
                        other_p_img.rectTransform.anchoredPosition = new Vector2(0,0);
                    }
                }

                string st = "";
                foreach(int key in _dict[c_tab].Keys)
                {
                    st += key;
                    st += ",";
                }
                Debug.Log("removed "+st);
            }
            else
            {//감소
                user_item[index].Add(count);
                _dict[c_tab][index].Add(count);
                slot.GetComponentInChildren<Text>().text = left.ToString();

            }
        }
        if(Managers.ItemInvoker != null)
            {
                Managers.ItemInvoker(new ItemTypeInfo(type, index, left));
            }



    }

    public void FlipPage(bool next)
    {
        if(next)
            if(_curr_page == _max_page[_curr_tab]-1)
            {
                Debug.Log("max page!");
                return;
            }
            else
                OpenPage(_curr_page+1);
        else
            if(_curr_page == 0)
            {
                Debug.Log("first page!");
                return;
            }
            else
                OpenPage(_curr_page-1);
    }

    public void FLipTab(bool next)
    {
        {
        if(next)
            if(_curr_tab == _max_tab-1)
            {
                Debug.Log("max page!");
                return;
            }
            else
                OpenTab(_curr_tab+1);
        else
            if(_curr_tab == 0)
            {
                Debug.Log("first page!");
                return;
            }
            else
                OpenTab(_curr_tab-1);
    }
    }
    public void ShowItemInfo(int index)
    {
        Transform item_info_ui = Managers.ui._common_UI_dict[Define.CommonUI.ItemInfo];
        ref Dictionary<int,O_Dictionary<int,ItemInfo>> _dict = ref Managers.data._category_inven;
        //Debug.Log("selected"+evt.pointerEnter.name);
        //int index = evt.pointerEnter.transform.GetSiblingIndex();
        int item_type;
        int slot_index;
        if(_inven_option)
        {
            if(_curr_tab <= type_line )
                item_type = 0;
            else
                item_type = 1;
            slot_index = index%_max_slot[_curr_tab];
        }
        else
        {
            item_type = _curr_tab;
            slot_index = index%_dict_slot;
        }
        
        //몇번째 차일드인지 파악
        //해당인덱스로 유저데이터 아이템리스트에 접근
        //아이템 인덱스 겟
        //아이템 db에서 인덱스로 정보 겟
        //인포 유아이는 내부에 터치하면 비활되는 스크립트만 넣자
        //사전모드일때는 그냥 인덱스로 접근
        //인벤토리일때는 좌표로 접근 ㅇㅋ?
        Sprite icon;
        string count;
        string name;
        string price;
        string explain;
        //ref Dictionary<DBHeader,Array> item_DB = ref Managers.data._item_DB_n;
        ref CSVDict item_DB = ref Managers.data.ItemDB(item_type);
        ref ItemInfoDict user_item = ref Managers.data.UserItem(item_type);
        ref Dictionary<int,Sprite[]> icon_dict = ref Managers.data.IconDB(item_type);
        ref DragInfo drag_info = ref Managers.ui._drag_info;

        if(index > item_DB[DBHeader.index].Length)
        {
            Debug.Log("Dictionary,["+index+"] is not found");
            return;
        }
        Debug.Log("show index : "+index);
        //Debug.Log("arrya lenth : "+icon_image.Length);
        icon = icon_dict[_curr_page][slot_index];
        name = item_DB.S(DBHeader.name,index);
        if(item_type == 1)
            price = "Not for sale";
        else
            price = item_DB.I(DBHeader.price,index).ToString();

        explain = item_DB.S(DBHeader.explain,index);

        Transform panel = item_info_ui.GetChild(0);

        if(_inven_option)
        {
            count = user_item[index].count.ToString();
            panel.GetChild(5).gameObject.SetActive(false);
        }
        else
        {
            count = "N/A";
            panel.GetChild(2).GetComponent<Text>().text = "";
            panel.GetChild(5).gameObject.SetActive(true);
        }
        panel.GetChild(0).GetComponent<Image>().sprite = icon;
        panel.GetChild(1).GetComponent<Text>().text = "이름 : "+name;
        panel.GetChild(2).GetComponent<Text>().text = "개수 : "+count;
        panel.GetChild(3).GetComponent<Text>().text = "판매가격 : "+price;
        panel.GetChild(4).GetComponent<Text>().text = "설명 : "+explain;

        Debug.Log("open "+item_info_ui.name);
        item_info_ui.gameObject.SetActive(true);

        drag_info.index = index;
        drag_info.type = _curr_type;
        
    }

    public void StartDrag(PointerEventData evt)
    {
        Debug.Log("clicded"+evt.pointerPressRaycast.gameObject.name);
        if(evt.pointerPressRaycast.gameObject.transform.parent == _background)
            return;
        Transform item_info_ui = Managers.ui._common_UI_dict[Define.CommonUI.ItemInfo];
       // Debug.Log("drag deteced"+evt.pointerEnter.name);
        _time = Time.time;
        //Debug.Log(evt.pointerEnter.name);
        int index = evt.pointerPressRaycast.gameObject.transform.parent.GetSiblingIndex();
        ref DragInfo drag_info = ref Managers.ui._drag_info;
        ref Dictionary<int,O_Dictionary<int,ItemInfo>> inven_dict = ref Managers.data._category_inven;
        ref Dictionary<int,Sprite[]> icon_dict = ref Managers.data.IconDB(_curr_type);
        
        int item_index;

        if(_inven_option)
        {
            if(index >= inven_dict[_curr_tab].Count)
            {
                Debug.Log("Inventory,["+index+"] is not found");
                drag_info.exist = false;
                return;
            }
            else
            {
                item_index = inven_dict[_curr_tab].GetKey(index);
                Debug.Log("got key : "+index);
                int iter = 0;
                foreach(int key in inven_dict[_curr_tab].Keys)
                {
                    if(iter == index)
                    {
                        index = key;
                        break;
                    }
                    iter++;
                }
                Sprite icon_image = icon_dict[index/16][index%16];
                drag_info.obj.GetComponent<Image>().sprite = icon_image;
            }
        }
        else//Dictionary
        {
            item_index = index + _curr_page* _dict_slot;
        }    
        drag_info.exist = true;
        drag_info.type = _curr_type;
        drag_info.index = item_index;
        drag_info.obj.transform.position = evt.position;

        Debug.Log("drag index : "+drag_info.index);





    }

    public void OnDrag(PointerEventData evt)
    {
        if(evt.pointerPressRaycast.gameObject.transform.parent == _background)
        {
            Debug.Log("drag not deceted");
            return;
        }
        ref DragInfo drag_info = ref Managers.ui._drag_info;
        if(!drag_info.exist || !_inven_option)
            return;
        else
        {
            Debug.Log("Ondrag!");
            Managers.ui._common_UI_dict[CommonUI.Overlay].gameObject.SetActive(true);
            drag_info.obj.GetComponent<Image>().enabled = true;
            drag_info.obj.transform.position = evt.position;
        }
    }

    public void EndDrag(PointerEventData evt)
    {
        ref DragInfo drag_info = ref Managers.ui._drag_info;
        if(Time.time - _time < 0.5f)
            if(drag_info.exist)
                if(evt.pointerCurrentRaycast.gameObject == evt.pointerPressRaycast.gameObject)
                    ShowItemInfo((int)drag_info.index);
        
        if(!_inven_option)
        {
            drag_info.exist = false;
            drag_info.obj.GetComponent<Image>().sprite = null;
        }
        Managers.ui._common_UI_dict[CommonUI.Overlay].gameObject.SetActive(false);
        drag_info.obj.GetComponent<Image>().enabled = false;
        Debug.Log("droped");
    }

    //test
    public void AddItem(int count = 1)
    {
        ref DragInfo draginfo = ref Managers.ui._drag_info;
        int index = draginfo.index;
        int item_type = draginfo.type;
        Debug.Log("adding :"+index);
        //딕셔너리창에서 터치하면 해당아이템이 인벤토리에 추가, 개수반영 ㅇㅋ?
        //ref Dictionary<DBHeader,Array> item_DB = ref Managers.data._item_DB_n;
        UpdateInventory(item_type, index, count);
    }
    //테스트할것
    // 인벤토리 정상적으로 분류되서 뜨는지(설치 비설치)
    // 갯수 등등 유저데이터 수정잘되는지
    // 제이슨 잘 동작하는지
}
