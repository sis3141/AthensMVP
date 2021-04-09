using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public int character;
    System.Random rand = new System.Random();

    Overlay ui; 
    void Start()
    {
        ui =  Managers.ui._common_UI_dict[Define.CommonUI.Overlay].GetComponent<Overlay>();
        Utils.BindTouchEvent(gameObject,OpenDialogue);
    }

    // Update is called once per frame
    void OpenDialogue()
    {
        List<string> dialogues = Managers.data._interact_dialogue[character];
        Debug.Log("dial count : "+dialogues.Count);
        int random_index = rand.Next(dialogues.Count);
        Debug.Log("random :"+random_index);
        string[] text = dialogues[random_index].Split('/');
        ui.OpenDialogue(text,character);

    }
}
