using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoginScene : BaseScene     //SceneType, Init(), Clear()
{
   // MyTest mytest = new MyTest();
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;
        Debug.Log("Login Scene Loaded!");
       // mytest.Delay(2.0f);
        Managers.scene.LoadScene(Define.Scene.Temp);
    }
    public override void Clear()
    {
        
    }
}
