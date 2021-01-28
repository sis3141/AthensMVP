using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
using System;

public class UIManager
{
    public Dictionary<CommonUI,GameObject> _common_UI_dict = new Dictionary<CommonUI,GameObject>();
    public Dictionary<SceneUIType,GameObject> _scene_UI_dict = new Dictionary<SceneUIType, GameObject>();
    public void LoadSceneUI()
    {
        GameObject UI_parent = GameObject.FindWithTag("UIObject");
        GameObject UI_child;
        int child_count = UI_parent.transform.childCount;
        Debug.Log("Child 2? :"+child_count);
        SceneUIType key;
        string[] UI_name;
        for(int i = 0; i < child_count; i++)
        {
            UI_child = UI_parent.transform.GetChild(i).gameObject;
            UI_name = UI_child.name.Split('_');
            Debug.Log("name : "+UI_name[0]);
            key = (SceneUIType)Enum.Parse(typeof(SceneUIType),UI_name[0]);
            _scene_UI_dict.Add(key,UI_child);
        }
        _scene_UI_dict[SceneUIType.Base].SetActive(true);
    }
    
    public void ClearSceneUI()
    {
        _scene_UI_dict.Clear();
        int count = _scene_UI_dict.Count;
        Debug.Log("clear ui, count :"+count);
    }

}
