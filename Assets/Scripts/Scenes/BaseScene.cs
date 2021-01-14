using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    //
    //SceneType, Init, Clear()
    public Define.Scene SceneType {get; protected set;} = Define.Scene.Unknown;
    public GameObject _main_camera;
    public Behaviour _3d_raycaster;
    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _main_camera = GameObject.FindWithTag("MainCamera");
        _3d_raycaster = Utils.GetOrAddComponent<PhysicsRaycaster>(_main_camera);
        _3d_raycaster.enabled = false;
    }


    public abstract void Clear();

    public void Toggle3DRaycast(bool toggle)
    {
        if(toggle)
            _3d_raycaster.enabled = true;
        else
            _3d_raycaster.enabled = false;

    }
}
