using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Define;

public class SceneManagerEx //: SceneManager
{
    public BaseScene CurrentScene {get {return GameObject.FindObjectOfType<BaseScene>();}}
    public void LoadScene(SceneType type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(SceneType type)
    {
        string name = System.Enum.GetName(typeof(SceneType),type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
