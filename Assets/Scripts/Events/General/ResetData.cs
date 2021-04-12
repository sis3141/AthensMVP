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
        Managers.data.LoadJson<UserData>("Data/ResetData/UserDB",ref Managers.data._user_DB);
        string jsonstring = JsonUtility.ToJson(Managers.data._user_DB);
        Csvtest.CreateJsonFile(Application.dataPath+"/Resources/Data","UserDB",jsonstring);
    }

    
}
