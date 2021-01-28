using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //모든 매니저들을 총괄하는 클래스
    static Managers _managers;

    static Managers managers {get{Init();return _managers;}}

    //InputManager _input = new InputManager();
    DataManager _data = new DataManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();
    //public static InputManager input {get{return managers._input;}}
    public static DataManager data {get{return managers._data;}}
    public static ResourceManager resource {get{return managers._resource;}}
    public static SceneManagerEx scene {get{return managers._scene;}}
    public static UIManager ui {get{return managers._ui;}}
    

    void Start()
    {
        Init();
    }

    void Update()
    {
        //_input.Update();
    }

    static void Init()
    {
        if(_managers == null)   //매니저 중복호출 방지
        {
            GameObject go  = GameObject.Find("@Managers"); //Find함수 비싸지만 최초 이후 안쓰이니 ㄱㅊ
            if(go == null)
            {
                go = new GameObject {name = "@Managers"};
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            _managers = go.GetComponent<Managers>();
            Debug.Log("Managers on load!");
            //_managers._input.Init();
            _managers._data.Init();
        }
    }

    public static void Clear()
    {
        _managers._ui.ClearSceneUI();
    }
}
