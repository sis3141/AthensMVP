﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;

    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -4.0f);
    [SerializeField]
    float _rotation = 2.0f; 

    [SerializeField]
    GameObject _player = null;
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuarterView)
        {
            transform.position = _player.transform.position + _delta;
            transform.LookAt(_player.transform.position + Vector3.up*_rotation);
            //RaycastHit hit;
            // if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            // {
            //     float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
            //     transform.position = _player.transform.position + _delta.normalized * dist;
            // }
            // else
            // {
			//}
		}
    }
}
