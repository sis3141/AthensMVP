using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IncreaseTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Utils.BindTouchEvent(gameObject,Increase);
    }

    // Update is called once per frame
    void Increase()
    {
        Managers.data._user_DB.time++;
        int time =  Managers.data._user_DB.time;
        Debug.Log("time : "+time);
        if(Managers._time_invoker != null)
        {
            Debug.Log("time Invoke!");
            Managers._time_invoker.Invoke(time);

        }
    }
}
