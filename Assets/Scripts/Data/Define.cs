using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public static float _tap_delay = 0.5f;
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        BuildBlocks,
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
