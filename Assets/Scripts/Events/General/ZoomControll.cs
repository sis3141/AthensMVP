using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomControll : MonoBehaviour
{
    public Camera _camera;
    public float _zoom_speed;
    public float _move_speed;

    public float e_angle;
    float _old_dis = 0f;
    float _cur_dis = 0f;
    float _difference;
    Touch _touch_zero;
    Touch _touch_one;

    public bool _lock = false;

    Vector2 _touch_zero_prev, _touch_one_prev;
    void Start()
    {
        _camera = Camera.main;
        _zoom_speed = 0.01f;
        _move_speed = 0.01f;
        Debug.Log("Hello zoom");

        // Vector3 test = new Vector3 (1.0f,0f,0f);
        // Debug.Log("befor vector :"+test);
        // Quaternion rotation = Quaternion.Euler(0f,90.0f,0f);
        // test = rotation * test;
        // Debug.Log("after rotation :"+test);
    }

    void Update()
    {
        if(!_lock)
        {
            Zoom();
            MoveCamera();
        }
    }

    void Zoom()
    {
        if(Input.touchCount == 2)
        {
            Debug.Log("Multi touch detected!");
            _touch_zero = Input.GetTouch(0);
            _touch_one = Input.GetTouch(1);

            _touch_zero_prev = _touch_zero.position - _touch_zero.deltaPosition;
            _touch_one_prev = _touch_one.position - _touch_one.deltaPosition;

            _old_dis = (_touch_zero_prev - _touch_one_prev).magnitude;
            _cur_dis = (_touch_zero.position - _touch_one.position).magnitude;

            _difference = _old_dis - _cur_dis;
            Debug.Log("Differenc3 : "+_difference);

            _camera.orthographicSize += _difference*_zoom_speed;
            Debug.Log("size :"+_camera.orthographicSize);
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize,1.0f,5.0f);

            
        }
    }

    void MoveCamera()
    {
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {   
            e_angle = _camera.transform.eulerAngles.y;
            Quaternion v_rotation = Quaternion.Euler(0.0f,e_angle,0.0f);
            Vector2 v_screen = Input.GetTouch(0).deltaPosition;
            Vector3 v3_screen = new Vector3(v_screen.x,0,v_screen.y);
            Vector3 dir = v_rotation * v3_screen;
            dir *= _move_speed;
            _camera.transform.position -= new Vector3(dir.x,0,dir.z);
        }
    }
}
