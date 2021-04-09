// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using Define;
// using DataStructure;
// using System;
// using UnityEngine.EventSystems;

// //인벤토리 밑에 페이지, 페이지밑에 슬롯이있는구조, 인벤토리 위치에 스크립트 붙일것.
// public class Island_Inventory : MonoBehaviour
// {
//     Transform _inventory;
//     Transform _background;
//     Sprite[] _icon_image;
//     Transform _item_info_page;
//     //어떻게 전달할거?
//     public static int current_page = 0;
//     public static int current_item;

//     public bool testoption;

//     void Awake()
//     {
//         ref Dictionary<int,ItemInfoDict> _item_dict = ref Managers.data._user_DB.item.dict;
//         _icon_image = Resources.LoadAll<Sprite>("Images/inven_icon");
//         _inventory = transform;
//         _background = _inventory.GetChild(0);
//         _item_info_page = Managers.ui._scene_UI_dict[Define.SceneUIType.ItemInfo].transform;
//         Utils.BindTouchEvent(gameObject,ShowItemInfo);
//         Debug.Log("inven opened");
//     }

//     void OnEnable()
//     {
//         if(testoption)
//             LoadInventory();
//        // else
//             //LoadDictionary();
//     }

//     public void LoadInventory()
//     {
//         ref ItemDict user_item = ref Managers.data._user_DB.item;
        
//         int page_count = user_item.Count();
//         for(var p = 0; p< page_count; p++)
//         {
//             int item_count = user_item[p].Count;
//             Transform page = _background.transform.GetChild(p);
//             int[] item_keys = new int[user_item[p].Keys.Count];
//             user_item[p].Keys.CopyTo(item_keys, 0);
//             int slot_index = 0;
//             for(var i = 0; i<item_count;i++)
//             {
//                 int item_index = item_keys[i];
//                 bool item_have = user_item[p].ContainsKey(item_index);
//                 if(item_have)
//                 {
//                     Transform slot = page.GetChild(slot_index);
//                     Image image = Utils.GetOrAddComponent<Image>(slot);
//                     image.sprite = _icon_image[item_index];
//                     slot_index++;
//                     Debug.Log("Icon loaded : "+item_index);
//                 }
//             }
//         }
//     }

//     public void TogglePage(int index)
//     {
//         if(index == current_page)
//             return;
//         _inventory.GetChild(index).gameObject.SetActive(true);
//         if(current_page > 1)
//             return;

//         _inventory.GetChild(current_page).gameObject.SetActive(false);

//         current_page = index;
//     }
//     public void TogglePage()
//     {
//         Transform background = _inventory.transform.GetChild(0);
//         if(current_page == 0)
//         {
//             background.GetChild(0).gameObject.SetActive(false);
//             background.GetChild(1).gameObject.SetActive(true);
//             current_page = 1;
//         }
//         else
//         {
//             background.GetChild(0).gameObject.SetActive(true);
//             background.GetChild(1).gameObject.SetActive(false);
//             current_page = 0;
//         }
//     }

//     public void ShowItemInfo(PointerEventData evt)
//     {
//         Debug.Log("selected"+evt.pointerEnter.name);
//         int index = evt.pointerEnter.transform.GetSiblingIndex();
//         ref Dictionary<DBHeader,Array> item_DB = ref Managers.data._item_DB;
//         ref ItemDict user_item = ref Managers.data._user_DB.item;
//         //몇번째 차일드인지 파악
//         //해당인덱스로 유저데이터 아이템리스트에 접근
//         //아이템 인덱스 겟
//         //아이템 db에서 인덱스로 정보 겟
//         //인포 유아이는 내부에 터치하면 비활되는 스크립트만 넣자
//         //사전모드일때는 그냥 인덱스로 접근
//         //인벤토리일때는 좌표로 접근 ㅇㅋ?
//         Sprite icon;
//         int type;
//         string count;
//         string name;
//         string explain;
//         if(testoption)
//         {
//             type = current_page;
//             int[] key_index = new int[user_item[type].Keys.Count];
//             user_item[type].Keys.CopyTo(key_index,0); 
//             if(index >= key_index.Length && testoption)
//             {
//                 Debug.Log("["+type+"],["+index+"] is not found");
//                 return;
//             }
//             index = key_index[index];      //빈슬롯 선택시
//             count = user_item[type][index].count.ToString();
//         }
//         else
//         {
//             if(index >= item_DB.Keys.Count)
//             {
//                 Debug.Log("["+index+"] is not found");
//                 return;
//             }

//             type = (int)item_DB[DBHeader.type].GetValue(index);
//             count = "0";
//         }

//         icon = _icon_image[index];
//         name = (string)item_DB[DBHeader.name].GetValue(index);
//         explain = (string)item_DB[DBHeader.explain].GetValue(index);

//         Transform panel = _item_info_page.GetChild(0);
//         panel.GetChild(0).GetComponent<Image>().sprite = icon;
//         panel.GetChild(1).GetComponent<Text>().text = "이름 : "+name;
//         panel.GetChild(2).GetComponent<Text>().text = "개수 : "+count;
//         panel.GetChild(3).GetComponent<Text>().text = "설명 : "+explain;

//         _item_info_page.gameObject.SetActive(true);

//         current_item = index;
//         Debug.Log("current item :"+current_item);
//     }

//     // public void LoadDictionary()
//     // {
//     //     //csv에서 모든 아이템로드, 터치하면 아이템 추가등의 동작으로 테스트용도
//     //     ref Dictionary<DBHeader,Array> _item_DB_b = ref Managers.data._item_DB_b;
//     //     ref Dictionary<DBHeader,Array> _item_DB_n  = ref Managers.data._item_DB_n;
//     //     if(bu)
//     //     int count = _item_DB[DBHeader.index].Length;
//     //     Transform background = _inventory.transform.GetChild(0);
//     //     Transform page = background.GetChild(0);
//     //     for(int i = 0; i<count; i++)
//     //     {
//     //         Transform child = page.GetChild(i);
//     //         Image image = Utils.GetOrAddComponent<Image>(child);
//     //         image.sprite = _icon_image[i];
//     //     }
//     // }

//     public static void TAddItem(int index, int count = 1)
//     {
//         //딕셔너리창에서 터치하면 해당아이템이 인벤토리에 추가, 개수반영 ㅇㅋ?
//         ref Dictionary<DBHeader,Array> item_DB = ref Managers.data._item_DB;
//         ref ItemDict _user_item = ref Managers.data._user_DB.item;
        
//         int type = (int)item_DB[DBHeader.type].GetValue(index);
//         if(!_user_item.dict.ContainsKey(type))
//             _user_item.dict.Add(type,new ItemInfoDict(new Dictionary<int,ItemInfo>()));
//         if(!_user_item[type].ContainsKey(index))
//             _user_item[type].Add(index,new ItemInfo(count));
//         else
//         {
//             int current_count = _user_item[type][index].count;
//             _user_item[type][index] = new ItemInfo(current_count+count);
//         }
//         Debug.Log("and dictionary :" +_user_item[type][index].count);
//     }
//     //test
//     public void TAddItem(int count = 1)
//     {
//         int index = current_item;
//         Debug.Log("adding :"+index);
//         //딕셔너리창에서 터치하면 해당아이템이 인벤토리에 추가, 개수반영 ㅇㅋ?
//         ref Dictionary<DBHeader,Array> item_DB = ref Managers.data._item_DB;
//         ref ItemDict _user_item = ref Managers.data._user_DB.item;
        
//         int type = (int)item_DB[DBHeader.type].GetValue(index);
//         if(!_user_item.dict.ContainsKey(type))
//             _user_item.dict.Add(type,new ItemInfoDict(new Dictionary<int,ItemInfo>()));
//         if(!_user_item[type].ContainsKey(index))
//             _user_item[type].Add(index,new ItemInfo(count));
//         else
//         {
//             int current_count = _user_item[type][index].count;
//             _user_item[type][index] = new ItemInfo(current_count+count);
//         }
//         Debug.Log("and dictionary :" +_user_item[type][index].count);
//     }

//     public int TRemoveItem(int index, int count)
//     {
//         ref Dictionary<DBHeader,Array> item_DB = ref Managers.data._item_DB;
//         int type = (int)item_DB[DBHeader.type].GetValue(index);
//         // int remaining = _user_item[type].item_list[]
//         return 0;
//     }
//     //테스트할것
//     // 인벤토리 정상적으로 분류되서 뜨는지(설치 비설치)
//     // 갯수 등등 유저데이터 수정잘되는지
//     // 제이슨 잘 동작하는지
// }
