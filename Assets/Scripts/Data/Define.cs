using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define
{
    public struct ConstInfo
    {
    public static float _tap_delay = 0.5f;
    public const string DATA_PATH = "Assets/Resources/Data/";
    }
    public enum SceneType
    {
        Unknown,
        Login,
        Lobby,
        Library,
        Shop,
        Island,
        Temp,
    }
    public enum TouchEvent
    {
        Tap,
        Drag,
        DoubleTap,
        Down,
        Up,
    }

    public enum CameraMode
    {
        QuarterView,
    }

    public enum CommonUI
    {

    }

    public enum SceneUIType
    {
        Base,
        Inventory,
        Info,
        Dictionary,
    }

    public enum ItemType
    {
        None,
        Building,
    }

    public enum BookTarget
    {
        Child,
        Adult,
    }

    public enum DBHeader
    {
        index,
        name,
        type,
        size,
        explain,

    }

}
