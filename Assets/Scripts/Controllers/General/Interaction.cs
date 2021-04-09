using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Interaction 
{
    // Start is called before the first frame update
    
    public static void EnterInteraction(int tool, string tag)
    {
        switch(tool)
        {
            case 0:
                Interaction.InterTest(tag);
                break;
        }

    }
    public static void ExitInteraction(int tool, string tag)
    {
        switch(tool)
        {
            case 0:
                Interaction.ExitTest(tag);
                break;
        }

    }

    public static void InterTest(string tag)
    {
        Transform ui = Managers.ui._scene_UI_dict[Define.SceneUI.Test];
        ui.GetComponentInChildren<Text>().text = tag;
        ui.gameObject.SetActive(true);
    }
    public static void ExitTest(string tag)
    {
        Transform ui = Managers.ui._scene_UI_dict[Define.SceneUI.Test];
        ui.GetComponentInChildren<Text>().text = "none";
        ui.gameObject.SetActive(false);
    }

    
}
