using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MuteSound : MonoBehaviour
{
    // Start is called before the first frame update
    public bool _on;
    public bool _BGM_option;
    public GameObject _mute_image;
    void Start()
    {
        _mute_image = transform.GetChild(0).gameObject;
        if(_BGM_option)
        {
            _on = !Managers.sound._bgm_player.mute;
            Utils.BindTouchEvent(gameObject,OnOffBGM);
        }
        else
        {
            _on = !Managers.sound._ui_player.mute;
            Utils.BindTouchEvent(gameObject,OnOffEffect);
        }

        if(_on)
            _mute_image.SetActive(false);
        else
            _mute_image.SetActive(true);
    }

    // Update is called once per frame
    void OnOffBGM()
    {
        if(_on)
        {
            _on = false;
            Managers.sound.OnOffBGM(false);
            _mute_image.SetActive(true);
            Debug.Log("BGM muted!");
        }
        else
        {
            _on = true;
            Managers.sound.OnOffBGM(true);
            _mute_image.SetActive(false);
            Debug.Log("BGM unmuted!");
        }

    }

    void OnOffEffect()
    {
        if(_on)
        {
            _on = false;
            Managers.sound.OnOffEffect(false);
            _mute_image.SetActive(true);
            Debug.Log("effect muted!");
        }
        else
        {
            _on = true;
            Managers.sound.OnOffEffect(true);
            _mute_image.SetActive(false);
            Debug.Log("effect unmuted!");
        }
    }
}
