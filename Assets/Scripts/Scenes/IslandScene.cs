using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        Debug.Log("Island Loaded!");
        _scene_type = Define.SceneType.Island;
    }
    public override void Clear()
    {
    }
}
