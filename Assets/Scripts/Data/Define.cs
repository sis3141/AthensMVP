using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Define
{
    
    public struct ConstInfo
    {
        public static float _tap_delay = 0.5f;
        public const string DATA_PATH = "Assets/Resources/Data/";

        public const string R_BGM_PATH = "Sounds/BGM/";
        public const string R_UIS_PATH = "Sounds/UI/";
        
    }
    public struct DragInfo
    {
        public bool exist;
        public int type;
        public int index;

        public GameObject obj;

        public DragInfo(bool exist = false,int t = 0 ,int i = 0 )
        {
            this.exist = exist;
            this.type = t;
            this.index = i;
            this.obj = new GameObject("_drag_obj",typeof(Image));
            this.obj.GetComponent<Image>().enabled = false;
            this.obj.GetComponent<Image>().raycastTarget = false;
        }
    }
    public struct DialogueInfo
    {
        public int character;
        public string dialogue;
        public DialogueInfo(int character, string dialogue)
        {
            this.character = character;
            this.dialogue = dialogue;
        }
    }

    public struct InvenType
    {
        //normal, tool, food, crop, starfish, starsong
        bool[] normal;// [true,false,true,true,true,true];
    }

    public enum SceneType
    {
        Unknown,
        Login,
        Library,
        Island,
        Temp,
    }
    public enum TouchEvent
    {
        Tap,
        StartDrag,
        Drop,
        EndDrag,
        OnDrag,
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
        Inventory,
        UserInfo,
        ItemInfo,
        Dictionary,
        Minimap,
        Community,
        Tools,
        Settings,
        Overlay,
    }

    public enum SceneUI
    {
        Base, 
        BookReader,
        Test,
        Manufacture,
        
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
        price,
        duration,
        evolution,
        destroy,
        writer,
        KDC,
        prefab,
        explain_1,
        character,
        stage,
        step,
        var1,
        var2,
        num,
        dialogue,
        order,


    }


    public enum Map
    {
        Main,
        BlackSmith,
        Farm,
    }

}
