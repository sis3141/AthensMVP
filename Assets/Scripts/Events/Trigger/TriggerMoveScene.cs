using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMoveScene : MonoBehaviour
{
    [SerializeField]
    Define.Scene _scene_name = Define.Scene.Library;
    public void OnTriggerEnter(Collider other) 
    {
        Managers.scene.LoadScene(_scene_name);
    }
}
