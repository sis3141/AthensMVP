using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectEvents : MonoBehaviour
{
    public void Destroy(PointerEventData evt)
    {
        Managers.resource.Destroy(evt.selectedObject);
    }
}
