using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{	
    //setting variables
	public float _speed = 5.0f;
    public float _move_tap_delay = 0.3f;
    public float _zoom_end_delay = 0.2f;
    public float _zoom_speed = 0.02f;
    public float _move_speed = 0.02f;
//charactor object variable
    public Camera _camera = null;
    public GameObject _gameobject = null;
    public Transform _transform = null;
    public Animator _animator = null;
    Vector3 _destpos, _dir;
    RaycastHit _hit;
 //zoom variable
    public float e_angle;
    float _old_dis = 0f;
    float _cur_dis = 0f;
    float _difference;
    Touch _touch_zero, _touch_one;
    Vector2 _touch_zero_prev, _touch_one_prev;
    float _start_time,_delta_time, _end_time;

    public PlayerState _state;
    bool _activated = false;

    public void Init()
    {
        //StartCoroutine(Nextframe());
        _activated = true;
        Respawn();
        

        _camera = Camera.main;
        Debug.Log("player activated");
    }
	void Update()
    {
        if(!_activated)
            return;
        


        int _popup_count = Managers.ui._popup_count;
        if(Input.touchCount > 0 && _popup_count == 0)
        {
            switch(Input.touchCount)
            {
                case 1 :
                    Touch touch = Input.GetTouch(0);
                    switch(touch.phase)
                    {
                        case TouchPhase.Began :
                            _start_time = Time.time;
                            break;

                        case TouchPhase.Moved : 
                            _delta_time = Time.time - _end_time;
                            if(_delta_time > _zoom_end_delay)
                                MoveCamera();
                            break;

                        case TouchPhase.Ended :
                            _delta_time = Time.time - _start_time;
                            if(_delta_time < _move_tap_delay)
                                SetDestPoint();
                            break;
                    }
                    break;

                case 2 :
                    Zoom();
                    break;
            }
            // if(Input.touchCount == 1)
            // {
            //     Touch touch = Input.GetTouch(0);
            //         switch(touch.phase)
            //         {
            //             case TouchPhase.Began :
            //                 _start_time = Time.time;
            //                 break;

            //             case TouchPhase.Moved : 
            //                 _delta_time = Time.time - _end_time;
            //                 if(_delta_time > _zoom_end_delay)
            //                     MoveCamera();
            //                 break;

            //             case TouchPhase.Ended :
            //                 _delta_time = Time.time - _start_time;
            //                 if(_delta_time < _move_tap_delay)
            //                     SetDestPoint();
            //                 break;
            //         }
            // }
            // if(Input.touchCount == 2)
            // {
            //     Zoom();
            // }
        }
        MoveToPoint();

	}

    public void SetDestPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        Debug.DrawRay(Camera.main.transform.position,ray.direction,Color.red);
        if(Physics.Raycast(ray, out _hit, 100.0f, LayerMask.GetMask("Field")))
        {
            _destpos = _hit.point;
            Debug.Log($"Destpos : {_destpos}");
            _animator.SetFloat("speed", _speed);
        }
    }
	public void MoveToPoint()
    {
        //Debug.Log("Lets Move!");
        // Vector3 _destpos, _dir;
        // Raycast_Hit _hit;
        
       // Animator _animator = GetComponent<Animator>();
        
        // if(_dir.magnitude < 0.0001f)
        //     _animator.SetFloat("speed",0);
        _dir = _destpos - _transform.position;
        if(_dir.magnitude > 0.2f)
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, _dir.magnitude);
            Debug.Log($"Moveto : {_destpos}");
            _transform.position += _dir.normalized * moveDist;
            _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(_dir),10 * Time.deltaTime);
        }
        else
        {
            _animator.SetFloat("speed",0);
        }
    }

    public void Zoom()
    {
        Debug.Log("zoom speed in fun :" + _zoom_speed);
        Debug.Log("Multi touch detected!");
        _touch_zero = Input.GetTouch(0);
        _touch_one = Input.GetTouch(1);

        _touch_zero_prev = _touch_zero.position - _touch_zero.deltaPosition;
        _touch_one_prev = _touch_one.position - _touch_one.deltaPosition;

        _old_dis = (_touch_zero_prev - _touch_one_prev).magnitude;
        _cur_dis = (_touch_zero.position - _touch_one.position).magnitude;

        _difference = _old_dis - _cur_dis;
        if(_difference > 0)
            Debug.Log("expand");
        if(_difference <0)
            Debug.Log("shrink");
        if(_difference*_zoom_speed > 0)
            Debug.Log("please move");
        _camera.orthographicSize += _difference*_zoom_speed;
        _camera.fieldOfView += _difference*_zoom_speed;
        Debug.Log("orth size :"+_camera.orthographicSize);
        Debug.Log("fov :"+_camera.fieldOfView);
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize,1.0f,5.0f);
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView,10.0f,32.0f);

        _end_time = Time.time;

    }

    void MoveCamera()
    {
        e_angle = _camera.transform.eulerAngles.y;
        Quaternion v_rotation = Quaternion.Euler(0.0f,e_angle,0.0f);
        Vector2 v_screen = Input.GetTouch(0).deltaPosition;
        Vector3 v3_screen = new Vector3(v_screen.x,0,v_screen.y);
        Vector3 dir = v_rotation * v3_screen;
        dir *= _move_speed;
        dir *= _camera.fieldOfView/32;
        _camera.transform.position -= new Vector3(dir.x,0,dir.z);
    }

    

    public void Clear()
    {
        _activated = false;
        _gameobject = null;
    }

    public void Respawn()
    {
        if(_gameobject == null)
        {
            _gameobject = Resources.Load<GameObject>("Prefabs/characters/man");
            _gameobject = Instantiate(_gameobject);
            _transform = _gameobject.transform;
            _animator = _transform.GetComponent<Animator>();
            Utils.GetOrAddComponent<PlayerState>(_transform);
            //_gameobject = GameObject.FindWithTag("Player");
        }
        Debug.Log("im controller"+_gameobject.name);
        GameObject respawn = Managers.scene._world.transform.GetChild(0).GetChild(0).gameObject;
        _transform.position = respawn.transform.position;
        _transform.rotation = Quaternion.Euler(0,0,0);
        Debug.Log(_transform.rotation);
        _destpos = _transform.position;
    }

}