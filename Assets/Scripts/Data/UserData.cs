using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class UserData
{
    [Serializable]
    public class ItemInfo
    {
        //[SerializeField]
        public int item_code;
        //[SerializeField]
        public int item_count;
    }
    public int ID;
    public string password;
    public string user_name;
    public int money;
    [SerializeField]
    public List<ItemInfo> inventory = new List<ItemInfo>();



    public static UserData CreateFromJson(string jsonstring)
    {
        return JsonUtility.FromJson<UserData>(jsonstring);
    }
}
