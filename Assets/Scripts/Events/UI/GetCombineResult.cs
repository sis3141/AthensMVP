using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GetCombineResult : MonoBehaviour
{
    // Start is called before the first frame update
    Manufact func;
    void Start()
    {
        func = GetComponentInParent<Manufact>();
        Debug.Log("check?"+func.gameObject.name);
        Utils.BindTouchEvent(gameObject,GetResult);
    }

    // Update is called once per frame
    void GetResult()
    {
        func.GetResult();
    }
}
