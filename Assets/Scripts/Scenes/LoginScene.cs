using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoginScene : BaseScene     //SceneType, Init(), Clear()
{
    protected override void Init()
    {
        base.Init();
        _scene_type = Define.Scene.Login;
        Debug.Log("Login Scene Loaded!");
    }
    public override void Clear()
    {
        
    }
}
