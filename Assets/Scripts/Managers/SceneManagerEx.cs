using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Define;

public class SceneManagerEx //: SceneManager
{
    public BaseScene _current_scene {get; set;}
    public void Init()
    {
        _current_scene = GameObject.FindObjectOfType<BaseScene>();
    }
    public void LoadScene(SceneType type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
        GameObject scene = GameObject.Find("@Scene");
        _current_scene = scene.GetComponent<BaseScene>();
        //_current_scene = GameObject.FindObjectOfType<BaseScene>();
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
