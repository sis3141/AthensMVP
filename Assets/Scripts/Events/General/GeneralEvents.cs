﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GeneralEvents : MonoBehaviour
{
    [SerializeField]
    Define.SceneType _scene_option = Define.SceneType.Lobby;

    public abstract void Start();

    public void MoveScene(PointerEventData evt)
    {
        Managers.scene.LoadScene(_scene_option);
    }

    public void QuitGame(PointerEventData evt)
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
