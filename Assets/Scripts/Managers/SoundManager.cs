using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class SoundManager
{
    public AudioSource _bgm_player;
    public AudioSource _ui_player;
    public AudioSource _effect_player;
    GameObject _audio_players;

    public AudioClip[] _ui_clips;
    string cur_bgm_name;
    public void Init()
    {
        _audio_players = new GameObject("@Audio");
        _audio_players.transform.SetParent(Managers.go.transform);
        _ui_clips = Resources.LoadAll<AudioClip>(ConstInfo.R_UIS_PATH);
        _bgm_player = _audio_players.AddComponent<AudioSource>();
        _ui_player = _audio_players.AddComponent<AudioSource>();
        _bgm_player.volume = Managers.data._user_DB.BGM_volume;
        _ui_player.volume = Managers.data._user_DB.effect_volume;
        //_bgm_player.mute = false;
        //_ui_player.mute = false;
    }

    public void LoadMapBGM()
    {

        cur_bgm_name = "i" + Managers.scene._current_map.ToString();
        Debug.Log("island scene, map name : "+cur_bgm_name);

        InitBGM();
    }

    public void LoadSceneBGM(string name)
    {
        if(name == "Island")
        {
            cur_bgm_name = "i" + Managers.scene._current_map.ToString();
            Debug.Log("island scene, map name : "+cur_bgm_name);
        }
        else
            cur_bgm_name = name;
        
        InitBGM();
    }

    public void Play_UI(int index = 0)
    {
        _ui_player.clip = _ui_clips[index];
        if(_ui_player.isPlaying)
            _ui_player.Stop();
        _ui_player.Play();
    }

    public void InitBGM()
    {
        _bgm_player.clip = null;
        string path = ConstInfo.R_BGM_PATH + cur_bgm_name;
        Debug.Log(path);
        _bgm_player.clip = Resources.Load<AudioClip>(path);
        _bgm_player.loop = true;
        _bgm_player.Play();
    }
    public void SetBGMVolume(float f)
    {
        _bgm_player.volume = f;
        Managers.data._user_DB.BGM_volume = f;
    }
    public void SetEffectVolume(float f)
    {
        _ui_player.volume = f;
        Managers.data._user_DB.effect_volume = f;
    }

    public void OnOffBGM(bool on)
    {        
        if(on)
        {
            _bgm_player.mute = false;
        }
        else
        {
            _bgm_player.mute = true;
        }

    }
    public void PlayBGM()
    {

    }

    public void OnOffEffect(bool on)
    {
        if(on)
        {
            _ui_player.mute = false;
        }
        else
        {
            _ui_player.mute = true;
        }
    }


}
