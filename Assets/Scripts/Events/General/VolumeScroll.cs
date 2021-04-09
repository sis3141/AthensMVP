using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeScroll : MonoBehaviour
{
    // Start is called before the first frame update
    Scrollbar _scrollbar;
    public bool BGM_option;
    void Start()
    {
        _scrollbar = GetComponent<Scrollbar>();
        if(BGM_option)
            _scrollbar.value = Managers.data._user_DB.BGM_volume;
        else
            _scrollbar.value = Managers.data._user_DB.effect_volume;
        Utils.BindTouchEvent(gameObject,Volume,Define.TouchEvent.Down);
    }

    // Update is called once per frame
    void Volume()
    {
        float volume = _scrollbar.value;
        if(BGM_option)
        {
            Managers.sound.SetBGMVolume(volume);
        }
        else
            Managers.sound.SetEffectVolume(volume);
    }
}
