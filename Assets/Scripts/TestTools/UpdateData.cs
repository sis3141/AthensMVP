using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DataStructure;
public class UpdateData : MonoBehaviour
{
    void Start()
    {
        Utils.BindTouchEvent(gameObject,UpdataData);
    }

    void UpdataData()
    {
        Managers.data.UpdateData<UserData>(Managers.data._user_DB,"UserDB");
    }
}
