using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Csvtest : MonoBehaviour
{
    void Start()
    {
        List<Dictionary<string,object>> data = CSVReader.Read ("Data/ItemData");
        Debug.Log("data count :"+data.Count);
        for(var i=0; i<data.Count; i++)
        {
            Debug.Log(data[i]["Index"]+ " " + "Name: "+ data[i]["Name"] + " " + "desc: " + data[i]["Description"]);
        }
    }

    
}
