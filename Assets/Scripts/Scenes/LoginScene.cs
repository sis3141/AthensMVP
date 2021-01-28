using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;


public class LoginScene : BaseScene    //SceneType, Init(), Clear()
{
    protected override void Init()
    {
        base.Init();
        _scene_type = SceneType.Login;
        Debug.Log("Login Scene Loaded!");
    }
    public override void Clear()
    {
        
    }
}
