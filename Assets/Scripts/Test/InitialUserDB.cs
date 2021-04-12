using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
using DataStructure;
using System.IO;

public class InitialUserDB : MonoBehaviour
{
    // Start is called before the first frame update
    UserData u = new UserData();
    void Start()
    {
        u.index = 0;
        u.user_name = "default";
        u.password = "0000";
        u.book_count = 0;
        u.time = 0;
        u.money = 0;
        u.intimacy = 0;
        u.stage = 0;
        u.stage_step = 0;
        u.character_stage = new int[]{0,0,0,0,0,0};
        u.BGM_volume = 0.5f;
        u.effect_volume = 0.5f;
        u.achievement = new Achievement(5);
        u.book = new BookInfoDict(new O_Dictionary<int, BookInfo>());
    //    u.book.Add(0,new BookInfo());

        u.planet = new PlanetInfoDict(new O_Dictionary<int, MapInfoDict>());
        u.planet.Add(0,new MapInfoDict(new O_Dictionary<int, MapData_build>()));
        u.planet[0].Add(0,new MapData_build(5,5));
        Debug.Log(u.planet[0][0].x_length);

        u.item_normal = new ItemInfoDict(new O_Dictionary<int, ItemInfo>());
     //   u.item_normal.Add(0,new ItemInfo(0,1));

        u.item_build = new ItemInfoDict(new O_Dictionary<int, ItemInfo>());
      //  u.item_build.Add(0,new ItemInfo(0,1));
        u.quest = new QuestInfoDict(new O_Dictionary<int,QuestInfo>());
       // u.quest.Add(0, new QuestInfo("dd",0,1,new ItemTypeInfo(),false));

        UpdateData<UserData>(u,"UserDB");
    }

    public void UpdateData<T>(T json_object, string name)
    {
        string sPath = string.Format("Assets/Resources/Data/ResetData/"+"{0}.json",name);
        Debug.Log(sPath);
        FileInfo file = new FileInfo(sPath);
        string jsonstring = JsonUtility.ToJson(json_object);
        //file.Directory.Create();
        File.WriteAllText(file.FullName, jsonstring);
        Debug.Log("saved"+jsonstring);
    }

    // Update is called once per frame
}
