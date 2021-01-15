using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{	
	public PointerEventData _event_data;
    public Vector3 _destpos, _dir;
    public RaycastHit _hit;
	public float _speed = 5.0f;
    Animator _animator;

    
    void Start()
    {
		// BindObjectEvent(ReceiveEventData);
        _animator = GetComponent<Animator>();
	}
	void Update()
    {
		if(Input.touchCount > 0)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if(Physics.Raycast(ray, out _hit, 100.0f, LayerMask.GetMask("Field")))
            {
                _destpos = _hit.point;
                Debug.Log($"Destpos : {_destpos}");
                _animator.SetFloat("speed", _speed);
            }
            else return;
		}
        MoveToPoint();

	}

	public void MoveToPoint()
    {
        //Debug.Log("Lets Move!");
        // Vector3 _destpos, _dir;
        // Raycast_Hit _hit;
        
       // Animator _animator = GetComponent<Animator>();
        
        // if(_dir.magnitude < 0.0001f)
        //     _animator.SetFloat("speed",0);
        _dir = _destpos - transform.position;
        if(_dir.magnitude > 0.01f)
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, _dir.magnitude);
            Debug.Log($"Movedist : {moveDist}");
            transform.position += _dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir),10 * Time.deltaTime);
        }
        else
        {
            _animator.SetFloat("speed",0);
        }
    }

}
