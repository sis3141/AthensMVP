using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{	
	public PointerEventData _event_data;
    public Vector3 _destpos,dir;
    public RaycastHit hit;
	public float _speed = 10.0f;
    void Start()
    {
		// BindObjectEvent(ReceiveEventData);
	}
	void Update()
    {
		if(Input.touchCount > 0)
		{
			Debug.Log("Lets Move!");
			MoveToPoint();
		}
	}

	public void MoveToPoint()
    {
        // Debug.Log("Lets Move!");
        // Vector3 _destpos, dir;
        // RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if(Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Field")))
        {
            _destpos = hit.point;
            dir = _destpos - transform.position;
        }
        else return;
       // Animator anim = GetComponent<Animator>();
        
        // if(dir.magnitude < 0.0001f)
        //     anim.SetFloat("speed",0);
        float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
        transform.position += dir.normalized * moveDist;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir),20 * Time.deltaTime);
    }

}
