using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffPlanetUI : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject _ui;
    public int _index;
    void Start()
    {
        _ui = Managers.ui._planet_ui.GetChild(_index).gameObject;
        Debug.Log("specal ui name : "+_ui.name);
        Utils.BindTouchEvent(gameObject,OnOff);
    }

    // Update is called once per frame
    void OnOff()
    {
        Debug.Log("special ui decee click");
        bool is_active = _ui.gameObject.activeSelf;
        if(is_active)
        {
            _ui.SetActive(false);
        }
        else
        {
            _ui.SetActive(true);
        }
        Managers.sound.Play_UI();
    }
}
