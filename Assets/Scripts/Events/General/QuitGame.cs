using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
     void Start()
    {
        Utils.BindTouchEvent(gameObject,Quit_Game);
    }

    public void Quit_Game()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
