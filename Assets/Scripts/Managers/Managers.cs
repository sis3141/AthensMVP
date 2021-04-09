using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DataStructure;

public class Managers : MonoBehaviour
{
    //모든 매니저들을 총괄하는 클래스
    static Managers _managers;
    static Managers managers {get{return _managers;}}

    public static GameObject go;
    public static Action StageInvoker = null;

    public static Action<ItemTypeInfo> ItemInvoker = null;

 
    //InputManager _input = new InputManager();
    DataManager _data = new DataManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();
    SoundManager _sound = new SoundManager();
    StageManager _stage = new StageManager();
    //monobehavior component
    PlayerController _player;
    CoroutineHandler _coroutine;

    //public static InputManager input {get{return managers._input;}}
    public static DataManager data {get{return managers._data;}}
    public static SceneManagerEx scene {get{return managers._scene;}}
    public static UIManager ui {get{return managers._ui;}}

    public static SoundManager sound {get{return managers._sound;}}
    public static StageManager stage {get{return managers._stage;}}
    public static PlayerController player {get{return managers._player;}}
    public static CoroutineHandler coroutine {get{return managers._coroutine;}}

    

    void Awake()
    {
        if(_managers == null)
            StartCoroutine(BaseInit());
    }

    // IEnumerator FirstLoad()
    // {
    //     //StartCoroutine(BaseInit());
    //     stage.Init();
    // }
    IEnumerator BaseInit()
    {
        Init();
        yield return null;
        stage.Init();
    }

    public bool Init()
    {
        if(_managers == null)
        {
            Debug.Log("managers awake!");
            DontDestroyOnLoad(gameObject);
            go = gameObject;
            _managers = Utils.GetOrAddComponent<Managers>(go);
            _player = Utils.GetOrAddComponent<PlayerController>(go);
            _coroutine = Utils.GetOrAddComponent<CoroutineHandler>(go);
            //_managers._input.Init();
            data.Init();
            sound.Init();
            scene.Init();
            ui.Init();

            Debug.Log("manager load done!");
            return true;
        }
        return false;
    }

    public static void Clear()
    {
        ui.ClearScene();
        player.Clear();
    }
}
