using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Define;

public abstract class BaseScene : MonoBehaviour
{
    //
    //_scene_type, Init, Clear()
    public Define.SceneType _scene_type {get; protected set;} = SceneType.Unknown;
    public GameObject _main_camera;
    public Behaviour _3d_raycaster;
    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Managers.ui.LoadSceneUI();
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
