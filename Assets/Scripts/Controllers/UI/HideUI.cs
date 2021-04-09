using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideUI : MonoBehaviour
{
    // Start is called before the first frame update
    int _popup_count;
    int _my_index;

    bool _appeared = true;
    Transform _base_ui;
    void Start()
    {
        _base_ui = Managers.ui._scene_UI_dict[Define.SceneUI.Base];
        _my_index = transform.GetSiblingIndex();
        Utils.BindTouchEvent(gameObject,Hide);
    }

    // Update is called once per frame
    void Hide()
    {
        _popup_count = Managers.ui._popup_count;
        if(_popup_count == 0)
        {
            Debug.Log("Do something");
                //transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "나와";
            foreach(Transform ch in _base_ui)
            {
                ch.gameObject.SetActive(!_appeared);
            }
            _base_ui.GetChild(_my_index).gameObject.SetActive(true);
            _appeared = !_appeared;
        }
    }
}
