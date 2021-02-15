using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Define;

public class SceneManagerEx //: SceneManager
{
    public BaseScene _current_scene {get{return GameObject.FindObjectOfType<BaseScene>();} set{_current_scene = value;}}
    public void LoadScene(SceneType type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
        _current_scene = GameObject.FindObjectOfType<BaseScene>();
    }

    string GetSceneName(SceneType type)
    {
        string name = System.Enum.GetName(typeof(SceneType),type);
        return name;
    }

    public void Clear()
    {
        _current_scene.Clear();
    }
}
