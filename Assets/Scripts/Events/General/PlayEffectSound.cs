using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayEffectSound : MonoBehaviour
{
    // Start is called before the first frame update
    public int sound_index = 0;
    void Start()
    {
        Utils.BindTouchEvent(gameObject,Play);
    }

    // Update is called once per frame
    void Play()
    {
        Managers.sound.Play_UI(sound_index);
    }
}
