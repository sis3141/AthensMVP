using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Define;
using DataStructure;
using System;
using System.Text.RegularExpressions;

public class DataManager
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };
    public UserData _user_DB;
   // public MapData _map;
    public Dictionary<DBHeader,Array> _item_DB;
    public Dictionary<DBHeader,Array> _book_DB;
    public Dictionary<DBHeader,Array> _map_DB;
    

    public void Init()
    {
        _user_DB = LoadJson<UserData>("UserDB");
        _item_DB = LoadCSV("ItemDB");
        _book_DB = LoadCSV("BookDB");
        _map_DB = LoadCSV("MapDB");
    }

    public void UpdateData<T>(T json_object, string name)
    {
        string sPath = string.Format(ConstInfo.DATA_PATH+"{0}.json",name);
        Debug.Log(sPath);
        FileInfo file = new FileInfo(sPath);
        string jsonstring = JsonUtility.ToJson(json_object);
        //file.Directory.Create();
        File.WriteAllText(file.FullName, jsonstring);
        Debug.Log("saved"+jsonstring);
    }

    public T LoadJson<T>(string name)
    {
        TextAsset text = Managers.resource.Load<TextAsset>("Data/"+name);
        return JsonUtility.FromJson<T>(text.text);
        
    }

    public static Dictionary<DBHeader,Array> LoadCSV(string file)
    {
        //var list = new List<Dictionary<string, object>>();
        Dictionary<DBHeader,Array> dict = new Dictionary<DBHeader, Array>();
        TextAsset data = Resources.Load("Data/"+file) as TextAsset;
 
        var lines = Regex.Split (data.text, LINE_SPLIT_RE);
 
        if(lines.Length <= 2) return dict;
 
        var types = Regex.Split(lines[0], SPLIT_RE);
        var header = Regex.Split(lines[1],SPLIT_RE);

        DBHeader[] key_index = new DBHeader[types.Length];
        Type[] type_index = new Type[types.Length];

        for(int i = 0; i< types.Length; i++)
        {
            Type type = Type.GetType("System."+types[i]);
            type_index[i] = type;
            DBHeader header_name = (DBHeader)Enum.Parse(typeof(DBHeader),header[i]);
            key_index[i] = header_name;
            Array column = Array.CreateInstance(type,lines.Length-3);
            dict.Add(header_name,column);
        }

        for(var l = 2; l < lines.Length; l++)
        {
 
            var values = Regex.Split(lines[l], SPLIT_RE);
            if(values.Length == 0 ||values[0] == "") continue;
 
            //var entry = new Dictionary<string, object>();
            for(var f=0; f < header.Length && f < values.Length; f++ ) 
            {
                string value = values[f];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                //change to array
                if(type_index[f] == typeof(Int32))
                {
                    int n = Int32.Parse(value);
                    dict[key_index[f]].SetValue(n,l-2);
                }
                else if(type_index[f] == typeof(Single))
                {
                    float fl = Single.Parse(value);
                    dict[key_index[f]].SetValue(fl,l-2);
                }
                else if (type_index[f] == typeof(String))
                {
                    dict[key_index[f]].SetValue(value,l-2);
                }
                else
                    dict[key_index[f]].SetValue("Error",l-2);
   
            }
        }
        return dict;
    }
    
}
