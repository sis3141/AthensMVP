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
        Temp,
    }
    public enum TouchEvent
    {
        Tap,
        Drag,
        DoubleTap,
    }
}
