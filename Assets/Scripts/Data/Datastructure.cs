using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DataStructure
{
    [Serializable]
    public struct UserData
    {
        [Serializable]
        public struct ItemInfo
        {
            //[SerializeField]
            public int item_code;
            //[SerializeField]
            public int item_count;
        }
        public struct BookInfo
        {
            public int book_code;
            public int activity_code;
        }
        [Serializable]
        public struct MapInfo
        {
            public int x;
            public int z;
        }
        public int ID;
        public string password;
        public string user_name;
        public int money;
        public int book_count;
        public int item_count;
        [SerializeField]
        public MapInfo map_info;

        [SerializeField]
        public List<ItemInfo> inventory;
        [SerializeField]
        public List<BookInfo> book_history;
    }

    [Serializable]
    public struct MapData
    {  
        [Serializable]
        public struct Row
        {
            [SerializeField]
            public int[] x;
        }

        [SerializeField]
        public Row[] mapinfo;
    }
}