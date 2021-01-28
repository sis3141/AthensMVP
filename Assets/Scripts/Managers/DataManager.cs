using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Define;
using DataStructure;

public class DataManager
{
    public UserData _user;
    public MapData _map;
    public List<Dictionary<string,object>> _item_info;

    public void Init()
    {
        _user = LoadData<UserData>("UserData");
        _map = LoadData<MapData>("MapData");
        _item_info = CSVReader.Read("Data/ItemInfo");
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

    

    public T LoadData<T>(string name)
    {
        TextAsset text = Managers.resource.Load<TextAsset>("Data/"+name);
        return JsonUtility.FromJson<T>(text.text);
        
    }
}
