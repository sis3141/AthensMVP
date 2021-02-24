using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class MoveBookPage : MonoBehaviour
{
    public bool _direction;
    GameObject _book = Managers.ui._scene_UI_dict[SceneUIType.BookReader];
    void Start()
    {
    }

}
