using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DataStructure;

public class ShowData : MonoBehaviour
{
    void Start()
    {
        Utils.BindTouchEvent(gameObject,Showdat);
    }

    void Showdat()
    {
        //Managers.data.UpdateData<UserData>(Managers.data._user_DB,"UserDB");
        TextAsset text = Resources.Load<TextAsset>("Data/UserDB");
        Debug.Log(text.text);
    }
}
