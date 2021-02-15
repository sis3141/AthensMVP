using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DataStructure;
using System.IO;
using System.Text.RegularExpressions;

public class ResetData : MonoBehaviour
{
    void Start()
    {
        Utils.BindTouchEvent(gameObject,Reset_Data);
    }

    public void Reset_Data()
    {
        Managers.data._user_DB = Managers.data.LoadJson<UserData>("ResetData/UserDB");
        string jsonstring = JsonUtility.ToJson(Managers.data._user_DB);
        Csvtest.CreateJsonFile(Application.dataPath+"/Resources/Data","UserDB",jsonstring);
    }

    
}
