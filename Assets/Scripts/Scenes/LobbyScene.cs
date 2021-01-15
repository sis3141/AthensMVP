using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        Debug.Log("Lobby loaded");
        _scene_type = Define.Scene.Lobby;
        Toggle3DRaycast(true);
    }
    public override void Clear()
    {
    }
}
