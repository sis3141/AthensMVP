using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Overlay : PopupUI
{
    // Start is called before the first frame update
    public Transform _dialogue;
    public Transform _base;
    public TextMeshProUGUI _dialogue_text;
    public TextMeshProUGUI _name_text;
    string[] _name_list;
    Transform _close;
    Transform _prev;
    Transform _next;
   
    string[] _texts;
    int _curr_index;
    int _dialogue_length;
    bool _stage_option;
    void Awake()
    {
        _dialogue = transform.GetChild(0);
        _base = _dialogue.GetChild(0);
        _dialogue.gameObject.SetActive(false);
        _close = _base.GetChild(0);
        _close.gameObject.SetActive(false);
        _prev = _base.GetChild(1);
        _prev.gameObject.SetActive(false);
        _next = _base.GetChild(2);
        _next.gameObject.SetActive(false);
        _dialogue_text = _base.GetChild(3).GetComponent<TextMeshProUGUI>();
        _name_text = _base.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>();
        Utils.BindTouchEvent(gameObject,DialogueControll);
        _name_list = Managers.data.temp_char_data;
    }

    void DialogueControll(PointerEventData evt)
    {
        Transform target = evt.pointerCurrentRaycast.gameObject.transform;
        Debug.Log(target.name);
        if(target == _close)
        {
            gameObject.SetActive(false);
            _dialogue.gameObject.SetActive(false);
            if(_stage_option)
                Managers.stage.StepClear();
        }
        else if(target == _prev)
        {
            _dialogue_text.text = _texts[_curr_index-1];
            _curr_index--;
            if(_curr_index == 0)
                _prev.gameObject.SetActive(false);
        }
        else if(target == _next)
        {
            _dialogue_text.text = _texts[_curr_index +1];
            _curr_index++;
            if(_curr_index+1 == _dialogue_length)
            {
                _close.gameObject.SetActive(true);
                _next.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("empty touch!");
            return;
        }
    }

    public void OpenDialogue(string[] text,int character,bool stage_option = false,int page = 0)
    {
        gameObject.SetActive(true);
        _stage_option = stage_option;
        _texts = text;
        _curr_index = page;
        _dialogue_length = text.Length;
        _dialogue_text.text = _texts[_curr_index];
        _name_text.text = _name_list[character];
        _dialogue.gameObject.SetActive(true);
        if(_curr_index != 0)
            _prev.gameObject.SetActive(true);
        if(_curr_index+1 == _dialogue_length)
            _close.gameObject.SetActive(true);
        else
            _next.gameObject.SetActive(true);

    }

    // Update is called once per frame
}
