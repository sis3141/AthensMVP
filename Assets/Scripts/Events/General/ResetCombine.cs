using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetCombine : MonoBehaviour
{
    // Start is called before the first frame update
    Manufact func;
    void Start()
    {
        func = GetComponentInParent<Manufact>();
        Utils.BindTouchEvent(gameObject,RS);
        
    }

    // Update is called once per frame
    void RS()
    {
        func.Reset();
    }
}
