using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager
{
    public static void SaveJsonData(string jsonstring)
    {
        string name = "MapData";
        string sPath = string.Format(Define.DATA_PATH+"{0}.json",name);
        Debug.Log(sPath);
        FileInfo file = new FileInfo(sPath);
        file.Directory.Create();
        File.WriteAllText(file.FullName, jsonstring);
        Debug.Log("saved"+jsonstring);
    }
}
