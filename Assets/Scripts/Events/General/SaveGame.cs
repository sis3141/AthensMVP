using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStructure;
using UnityEngine.EventSystems;

public class SaveGame : MonoBehaviour
{
    void Start()
    {
        Utils.BindTouchEvent(gameObject,SaveUserMapData);
    }

    void SaveUserMapData(PointerEventData evt)
    {
        Managers.data.UpdateData<UserData>(Managers.data._user,"UserData");
        Managers.data.UpdateData<MapData>(Managers.data._map,"MapData");
    }
}
