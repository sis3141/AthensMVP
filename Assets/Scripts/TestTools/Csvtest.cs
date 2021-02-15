using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Define;
using UnityEngine.EventSystems;
using DataStructure;
using Newtonsoft.Json;
using System.IO;
using System.Text;

public class Csvtest : MonoBehaviour
{
    void Start()
    {
        Utils.BindTouchEvent(gameObject,TestSave);
        Managers.data._user_DB.item.dict.Add(0,new ItemInfoDict(new Dictionary<int,ItemInfo>()));
        //Managers.data.UpdateData<UserData>(Managers.data._user_DB,"TestDB");
    }

    public void Reset_Data()
    {
        // Managers.data._user_DB = LoadJsonFile<UserData>(Application.dataPath+"/Data/ResetData","UserDB");
        // string jsonstring = JsonToString(Application.dataPath+"/Resources/Data/ResetData","UserDB");
        // CreateJsonFile(Application.dataPath+"/Resources/Data","UserDB",jsonstring);

        // UserData user = LoadJsonFile<UserData>(Application.dataPath+"/Resources/Data/ResetData","UserDB");
        // string jsonstring = ObjectToJson(user);
        // CreateJsonFile(Application.dataPath+"/Resources/Data","TestDB",jsonstring);

    }

    public void TestSave()
    {
        //Managers.data._user_DB.item_dict.dict.Add(0,new ItemInfoDict(new Dictionary<int,ItemInfo>()));
        //Managers.data._user_DB.item_dict.dict.Add(1,new ItemInfoDict(new Dictionary<int,ItemInfo>()));
        Island_Inventory.TAddItem(0,2);
        Island_Inventory.TAddItem(1,3);
    }

    public T JsonToObject<T>(string path,string name)
    {
        TextAsset text = Managers.resource.Load<TextAsset>(path+"/"+name);
        return JsonConvert.DeserializeObject<T>(text.text);
        
    }

    public string ObjectToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
        
        Debug.Log("json created in"+createPath+"/"+fileName);
        Debug.Log("내용"+jsonData);
    }

    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    public string JsonToString(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return jsonData;
    }




    
}
