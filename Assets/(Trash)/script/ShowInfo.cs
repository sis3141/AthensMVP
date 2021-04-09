using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowInfo : MonoBehaviour
{
    void Start()
    {
        
    }

    void GetIndex(PointerEventData evt)
    {
        int index = evt.selectedObject.transform.GetSiblingIndex();
        
    }
}
