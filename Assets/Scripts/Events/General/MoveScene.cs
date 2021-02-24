using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
    public Define.SceneType _scene_option;
    void Start()
    {
        Utils.BindTouchEvent(gameObject,Move_Scene);
    }


    public void Move_Scene()
    {
        Managers.scene.LoadScene(_scene_option);
    }
}
