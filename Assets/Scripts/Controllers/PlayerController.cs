using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{	
	public PointerEventData _event_data;
    public Vector3 _destpos,dir;
    public RaycastHit hit;
	public float _speed = 5.0f;
    Animator anim;

    
    void Start()
    {
		// BindObjectEvent(ReceiveEventData);
        anim = GetComponent<Animator>();
	}
	void Update()
    {
		if(Input.touchCount > 0)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if(Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Field")))
            {
                _destpos = hit.point;
                Debug.Log($"Destpos : {_destpos}");
                anim.SetFloat("speed", _speed);
            }
            else return;
		}
        MoveToPoint();

	}

	public void MoveToPoint()
    {
        //Debug.Log("Lets Move!");
        // Vector3 _destpos, dir;
        // RaycastHit hit;
        
       // Animator anim = GetComponent<Animator>();
        
        // if(dir.magnitude < 0.0001f)
        //     anim.SetFloat("speed",0);
        dir = _destpos - transform.position;
        if(dir.magnitude > 0.01f)
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            Debug.Log($"Movedist : {moveDist}");
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir),10 * Time.deltaTime);
        }
        else
        {
            anim.SetFloat("speed",0);
        }
    }

}
