using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DataStructure;
public class UserInfo : PopupUI
{
    // Start is called before the first frame update
    int _image_index = 0;
    string _name;
    Sprite _image;
    Transform _panel;
    Transform _image_ui;
    Transform _name_ui;

    Sprite[] _image_list;
    void Start()
    {
        
        _panel = transform.GetChild(0);
        _image_ui = _panel.GetChild(0);
        _name_ui = _panel.GetChild(1);
        Load();
    }

    void Load()
    {
        ref UserData user_DB = ref Managers.data._user_DB;
        _image_list = Resources.LoadAll<Sprite>("Images/Icon/character");
        _image = _image_list[_image_index];
        _name = user_DB.user_name;

        _image_ui.GetComponent<Image>().sprite = _image;
        _name_ui.GetComponent<Text>().text = _name;

    }

    
}
