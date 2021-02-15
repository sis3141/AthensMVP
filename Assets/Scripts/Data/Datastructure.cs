using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DataStructure
{
    [Serializable]
    public class SDictionary<Tkey,Tvalue> : ISerializationCallbackReceiver
    {
        [SerializeField]
        public Tkey[] keys;
        [SerializeField]
        public Tvalue[] values;
        public Dictionary<Tkey,Tvalue> dict;
        public Dictionary<Tkey,Tvalue> ToDictionary() {return dict;}
        
        public SDictionary(Dictionary<Tkey,Tvalue> dictionary)
        {
            this.dict = dictionary;
        }

        public Tvalue this[Tkey key]
        {
            get{return dict[key];}
            set{dict[key] = value;}
        }

        public int Count()
        {
            return dict.Count;
        }


        public void OnBeforeSerialize()
        {
            // if(dict == null)
            // {
            //     dict = new Dictionary<Tkey,Tvalue>();
            // }
            keys = new Tkey[dict.Keys.Count];
            dict.Keys.CopyTo(keys,0);
            values = new Tvalue[dict.Values.Count];
            dict.Values.CopyTo(values,0);
        }
        
        public void OnAfterDeserialize()
        {
           int count;
            // if(keys == null || values ==null)
            //     count = 0;
            // else
            count = Math.Min(keys.Length, values.Length);
            dict = new Dictionary<Tkey, Tvalue>(count);
            for (var i = 0; i < count; ++i)
            {
                dict.Add(keys[i],values[i]);
            }
        }
    }
    [Serializable]
    public class BookInfoDict : SDictionary<int,BookInfo>
    {
        public BookInfoDict(Dictionary<int,BookInfo> dictionary) : base(dictionary) {}
    }
    [Serializable]
    public class MapInfoDict : SDictionary<int,MapInfo>
    {
        public MapInfoDict(Dictionary<int,MapInfo> dictionary) : base(dictionary) {}
    }

    [Serializable]
    public class ItemInfoDict: SDictionary<int,ItemInfo>
    {
        public ItemInfoDict(Dictionary<int,ItemInfo> dictionary) : base(dictionary){}  
    }
    [Serializable]
    public class ItemDict: SDictionary<int,ItemInfoDict>
    {
        public ItemDict(Dictionary<int,ItemInfoDict> dictionary) : base(dictionary){}

        public new Dictionary<int,ItemInfo> this[int type]
        {
            get{return dict[type].dict;}
            set{dict[type].dict = value;}
        }
    }

    [Serializable]
    public struct ActivityCount
    {
        public int act_1;
        public int act_2;

        public ActivityCount(int act1, int act2)
        {
            this.act_1 = act1;
            this.act_2 = act2;
        }
    }
    [Serializable]
    public struct BookInfo
    {
        public ActivityCount count;

        public BookInfo(int index, ActivityCount act_count)
        {
            this.count = act_count;
        }

    }
    
    [Serializable]       
    public struct ItemInfo
    {
        public int count;

        public ItemInfo(int count)
        {
            this.count = count;
        }
    }
    [Serializable]
    public struct MapData
    {  
        public struct Row
        {
            [SerializeField]
            public int[] x;

            public Row(int x)
            {
                this.x = new int[x];
            }
        }

        [SerializeField]
        public Row[] z;

        public MapData(int x, int z)
        {
            this.z = new Row[z];
            for(int i = 0; i<z; i++)
            {
                this.z[i].x = new int[x];
            }
        }
    }
    [Serializable]
    public struct MapInfo
    {
        public int progress;
        public MapData data;

        public MapInfo(int index, int progress, MapData data)
        {
            this.progress = progress;
            this.data = data;
        }
    }
    [Serializable]
    public class UserData
    {
        public int index;
        public string user_name;
        public string password;
        public int book_count;
        public int money;

        // [SerializeField]
        public BookInfoDict book;
        public MapInfoDict map;
        [SerializeField]
        public ItemDict item;
    }

}