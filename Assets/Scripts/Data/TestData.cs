using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DataStructure;

public class TestData : MonoBehaviour
{
    void Start()
    {
        // UserData testdata = new UserData();
        // testdata.ID = 2400;
        // testdata.username = "김찬호";
        // testdata.password = "maruche240";
        // testdata.money = 3000000;
        // testdata.inventory.Add(0,20);

        // string JsonData = JsonUtility.ToJson(testdata);

        // Debug.Log(JsonData);

        // TextAsset text = Managers.resource.Load<TextAsset>("Data/UserData");
        // Debug.Log(text);
        // UserData data = JsonUtility.FromJson<UserData>(text.text);
        // Debug.Log(data.ID);
        // Debug.Log(data.user_name);
        // Debug.Log(data.money);
        // Debug.Log(data.inventory[0].item_code);

        MapGenerator.GenerateMap(100,100);
        
    }

    void Update()
    {
        
    }
}
