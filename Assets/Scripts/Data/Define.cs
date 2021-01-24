using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public static float _tap_delay = 0.5f;

    public const string DATA_PATH = "Assets/Resources/Data/";
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Library,
        Shop,
        BlockZone,
        Temp,
    }
    public enum TouchEvent
    {
        Tap,
        Drag,
        DoubleTap,
        Down,
    }

    public enum CameraMode
    {
        QuarterView,
    }
}
