using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
using System;
using UnityEngine.UI;

public class UIManager
{
    public Dictionary<CommonUI,Transform> _common_UI_dict = new Dictionary<CommonUI,Transform>();
    public Dictionary<SceneUI,Transform> _scene_UI_dict = new Dictionary<SceneUI, Transform>();
    public Transform _planet_ui;
    public GameObject _common_ui;


    public int _popup_count = 0;

    public DragInfo _drag_info;
    public bool Init()
    {
        _common_ui = new GameObject("@CommonUI");
        _common_ui.transform.SetParent(Managers.go.transform);
        InitCommonUI();
        return true;
    }
    public bool LoadScene()
    {
        // Transform _common_parent = GameObject.FindWithTag("CommonUI").transform;
        Transform _scene_parent = GameObject.FindWithTag("SceneUI").transform;
        //load scene
        SceneUI key;
        string[] UI_name_sep;
        foreach(Transform s_ui in _scene_parent.transform)
        {
            UI_name_sep = s_ui.name.Split('_');
            key = (SceneUI)Enum.Parse(typeof(SceneUI),UI_name_sep[0]);
            _scene_UI_dict.Add(key,s_ui);
        }

        foreach(Transform s_ui in _scene_UI_dict.Values)
        {
            s_ui.gameObject.SetActive(true);
            s_ui.gameObject.SetActive(false);
        }
        _scene_UI_dict[SceneUI.Base].gameObject.SetActive(true);    

        // foreach(Transform c_ui in _common_UI_dict.Values)
        // {
        //     c_ui.SetParent(_common_parent);
        // }
        Debug.Log(Managers.scene.GetCurSceneName()+", ui load scene done");
        return true;

    }
    
    public void ClearScene()
    {
        _scene_UI_dict.Clear();
        // foreach(Transform c_ui in _common_UI_dict.Values)
        // {
        //     c_ui.SetParent(_ui_buffer.transform);
        // }
        int count = _scene_UI_dict.Count;
        Debug.Log("clear ui, count :"+count);
    }

    public void InitCommonUI()
    {
        _drag_info = new DragInfo(false);
        GameObject[] UI_list = Resources.LoadAll<GameObject>("UI/Common/");
        CommonUI key;
        string[] UI_name_sep;
        foreach(GameObject origin_ui in UI_list)
        {
            string ui_name = origin_ui.name;
            UI_name_sep = origin_ui.name.Split('_');
            key = (CommonUI)Enum.Parse(typeof(CommonUI),UI_name_sep[0]);
            GameObject new_ui = GameObject.Instantiate(origin_ui,_common_ui.transform);
            new_ui.name = ui_name;
            new_ui.gameObject.SetActive(false);
            _common_UI_dict.Add(key,new_ui.transform);
        }
        // for(int i = 0; i < child_count; i++)
        // {
        //     UI_child = UI_parent.transform.GetChild(i);
        //     // UI_child.SetActive(true);
        //     // Debug.Log("activate "+UI_child.name);
        //     // UI_child.SetActive(false);
        //     UI_name = UI_child.name.Split('_');
        //     //Debug.Log("name : "+UI_name[0]);
        //     key = (CommonUI)Enum.Parse(typeof(CommonUI),UI_name[0]);
        //     _common_UI_dict.Add(key,UI_child);
        // }
        // for(int i = 0; i<key_count; i++)
        // {
        //     Transform ui = _common_UI_dict[UI_key_index[i]];
        //     ui.gameObject.SetActive(true);
        //     ui.gameObject.SetActive(false);
        // }   
       // _common_UI_dict[CommonUI.Overlay].gameObject.SetActive(true);  
        _drag_info.obj.transform.SetParent( Managers.ui._common_UI_dict[CommonUI.Overlay] );

    }

}
