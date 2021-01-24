using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
public class InputManager
{
    public Action<Define.TouchEvent> TouchAction = null;
    float _tap_delay = Define._tap_delay;
    float _begin_time;
   
    public void Update()
    { 
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                //TouchAction.Invoke(Define.TouchEvent.Began);
                _begin_time = Time.time;
                Debug.Log("Touch Began!"+touch.position);
            }
            if(touch.phase == TouchPhase.Moved)
            {
                if(Time.time - _begin_time > _tap_delay)
                {
                  // TouchAction.Invoke(Define.TouchEvent.Drag);
                    Debug.Log("Draging!");
                }
            
            }
            if(touch.phase == TouchPhase.Ended)
            {
                if(Time.time - _begin_time < _tap_delay)
                {
                    Debug.Log("Single Tap!"+touch.position);
                }
            }
            
            /*
            if(touch.phase == TouchPhase.Began)
            {
                TouchAction.Invoke(Define.TouchEvent.Began);
                Debug.Log("Touch Began!"+touch.position);
            }
            if(touch.phase == TouchPhase.Began)
            {
                TouchAction.Invoke(Define.TouchEvent.Began);
                Debug.Log("Touch Began!"+touch.position);
            }
            if(touch.phase == TouchPhase.Began)
            {
                TouchAction.Invoke(Define.TouchEvent.Began);
                Debug.Log("Touch Began!"+touch.position);
            }
            */
            

        }
        

    }
}
