using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveInGrid : MonoBehaviour
{
    public GameObject _building;

    Vector3 _destpos;
    RaycastHit _hit;
    public int _height = 0;
    bool _dragging = false;
    void Start()
    {
        _building = transform.parent.gameObject;
        //1. move in grid when drag
        Utils.BindTouchEvent(_building,StartDrag,Define.TouchEvent.Down);
        Utils.BindTouchEvent(_building,EndDrag,Define.TouchEvent.Up);
        //2. show direction ui when touched, until touch ends
    }

    void Update()
    {
        if(_dragging)
            moveInGrid();
    }

    void moveInGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if(Physics.Raycast(ray, out _hit, 100.0f, LayerMask.GetMask("Field")))
            {
                //Debug.DrawRay(_tempcamera, _hit.point -_tempcamera,Color.red);
                if((_hit.point.x + 0.45f) % 1.0f < 0.9f || (_hit.point.z + 0.45f) % 1.0f < 0.9f )
                {
                    _building.transform.position = new Vector3Int((int)_hit.point.x, _height, (int)_hit.point.z);
                    Debug.Log("move to "+_building.transform.position);
                }
            }
    }

    void StartDrag(PointerEventData evt)
    {
        _dragging = true;
        Debug.Log("zoom locked!");
        Camera.main.GetComponent<ZoomControll>()._lock = true;
    }

    void EndDrag(PointerEventData evt)
    {
        _dragging = false;
        Debug.Log("zoom unlocked!");
        Camera.main.GetComponent<ZoomControll>()._lock = false;
    }
}
