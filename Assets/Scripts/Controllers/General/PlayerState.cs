using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class PlayerState : MonoBehaviour
{
    // Start is called before the first frame update
    public int _tool = 0;

    public string _enter_tag;
    void Start()
    {
        Debug.Log("playstate, name is : "+gameObject.name);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other) 
    {
        Debug.Log("trigger deteced!");
        Interaction.EnterInteraction(_tool, other.tag);
        _tool++;
    }

    void OnTriggerExit(Collider other) 
    {
        Debug.Log("trigger leave!");
        Interaction.ExitInteraction(_tool,other.tag);

    }

}
