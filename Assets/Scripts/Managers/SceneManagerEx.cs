using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Define;
using System;
    
public class SceneManagerEx //: SceneManager
{
    public int _current_map;
    CoroutineHandler _coroutine_handler;
    public GameObject _world;
    public SceneType _curr_scene;
    //////////////////////////////////
    public GameObject _main_camera;
    public Behaviour _3d_raycaster;
    public string _name;

    public int _max_finger_id;
    public bool _step_mission;

    public void Init()
    {
        _coroutine_handler = Managers.coroutine;
        SceneManager.sceneLoaded += InitScene;
    }
    public void Toggle3DRaycast(bool toggle)
    {
        if(toggle)
            _3d_raycaster.enabled = true;
        else
            _3d_raycaster.enabled = false;

    }
    //////////////////////////////
    public void LoadScene(SceneType type)
    {
        string name = GetSceneName(type);
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));

    }
    public void InitScene(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("scene name :" + scene.name + ", mode : "+mode);
        _curr_scene = GetCurSceneType();
        if(_curr_scene == SceneType.Island)
            LoadMap(2,true);
        else
        {
            _main_camera = GameObject.FindWithTag("MainCamera");
            _3d_raycaster = Utils.GetOrAddComponent<PhysicsRaycaster>(_main_camera);
            _3d_raycaster.enabled = false;
            _current_map = SceneManager.GetActiveScene().buildIndex;
        }
        _max_finger_id = 1;
        Managers.sound.LoadSceneBGM(GetCurSceneName());
        Managers.ui.LoadScene();
        if(_step_mission)
        {
            if(_current_map == Managers.stage._order)
            {
                Managers.stage.StepClear();
                _step_mission = false;
            }
        }

    }

    public string GetSceneName(SceneType type)
    {
        string name = System.Enum.GetName(typeof(SceneType),type);
        return name;
    }

    public SceneType GetCurSceneType()
    {
        string name = SceneManager.GetActiveScene().name;
        return (SceneType)Enum.Parse(typeof(SceneType), name);
    }
    public string GetCurSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }


    public void LoadMap(int index, bool firstload = false)
    {
        if(_world == null)
            _world = GameObject.Find("@World");
        if(!firstload)
        {
            if(_current_map == index)
                return;
            GameObject.Destroy(_world.transform.GetChild(0).gameObject);
        }
        GameObject go = Resources.Load<GameObject>("Prefabs/Maps/"+index);
        _current_map = index;
        GameObject new_map = GameObject.Instantiate(go,_world.transform);
        Resources.UnloadUnusedAssets();
        Managers.ui._planet_ui = new_map.transform.GetChild(1);
        Debug.Log("UI?"+Managers.ui._planet_ui.name);


        Managers.sound.LoadMapBGM();
        if(_coroutine_handler == null)
            Debug.Log("error erro eooorrorororor");
        _coroutine_handler.Start_Coroutine(WFEF_player_init());
        
        foreach(Transform ch in Managers.ui._planet_ui)
             ch.gameObject.SetActive(false);

        _main_camera = GameObject.FindWithTag("MainCamera");
        _3d_raycaster = Utils.GetOrAddComponent<PhysicsRaycaster>(_main_camera);
        _3d_raycaster.enabled = true;
        Debug.Log("map loaded");
        
    }

    IEnumerator WFEF_player_init()
    {
        yield return new WaitForEndOfFrame();
        Managers.player.Init();
       // yield break;
    }

    public void Clear()
    {
        _world = null;
    }
}
