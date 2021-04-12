using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Define;
using DataStructure;
using System;
using System.Text.RegularExpressions;
using System.Linq;
//using System.Collections.Specialized;

public class DataManager
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };
    static public string[] HEADERS = new string[]{"index","name"};
    static public Type[] TYPES = {};
    #region //path

    public string PATH_icon_item_n = "Images/Icon/item_n/";
    public string PATH_icon_item_b = "Images/Icon/item_b/";
    public string PATH_main_progress = "Data/Progress/MainProgressDB_";
    public string PATH_progress_dialogue = "Data/Progress/ProgressDialogueDB_";
    public string PATH_book = "Data/BookDB";
    public string PATH_building = "Data/BuildingDB";
    public string PATH_Combine = "Data/CombineDB";
    public string PATH_interaction_dialogue = "Data/InteractionDialogueDB";
    public string PATH_item_n = "Data/ItemDB_n";
    public string PATH_item_b = "Data/ItemDB_b";
    public string PATH_Map = "Data/MapDB";
    public string PATH_Quest = "Data/QuestDB";
    public string PATH_user = "Data/UserDB";
    #endregion
    public UserData _user_DB;
   // public MapData _map;
    // public Dictionary<DBHeader,Array> _item_DB_n;
    // public Dictionary<DBHeader,Array> _item_DB_b;
    // public Dictionary<DBHeader,Array> _book_DB;
    // public Dictionary<DBHeader,Array> _map_DB;
    public CSVDict _item_DB_n = new CSVDict();
    public CSVDict _item_DB_b = new CSVDict();
    public CSVDict _book_DB = new CSVDict();
    public CSVDict _map_DB = new CSVDict();
    public CSVDict _combine_DB = new CSVDict();
    public CSVDict _main_progress_DB = new CSVDict();
    public CSVDict  _quest_DB = new CSVDict();
    public CSVDict _interact_dialogue_DB = new CSVDict();
    public CSVDict _progress_dialogue_DB = new CSVDict();
    public CSVDict _building_DB = new CSVDict();

    public Sprite _empty_slot;
    public Sprite _empty_image;

    public Dictionary<int,Sprite[]> _icon_item_b = new Dictionary<int, Sprite[]>();
    public Dictionary<int,Sprite[]> _icon_item_n = new Dictionary<int, Sprite[]>();

    public Dictionary<int,ItemInfo> _normal_inven;
    public Dictionary<int,ItemInfo> _build_inven;
    public Dictionary<int,Dictionary<string,int?[]>> _combine_dict = new Dictionary<int, Dictionary<string, int?[]>>();
    public Dictionary<DBHeader,Dictionary<int,int>> _stage_dict;// = new Dictionary<DBHeader, Dictionary<int, int>>();
    public Dictionary<int,O_Dictionary<int,ItemInfo>> _category_inven = new Dictionary<int, O_Dictionary<int, ItemInfo>>();
    public Dictionary<int,List<string>> _interact_dialogue;// = new Dictionary<int, List<string>>();
    public Dictionary<int,DialogueInfo> _progress_dialogue;// = new Dictionary<int, DialogueInfo>();

    public Dictionary<int,Dictionary<int, GameObject>> _mapper_dict = new Dictionary<int, Dictionary<int, GameObject>>();
    public Dictionary<DBHeader,Dictionary<int, Dictionary<int,int[]>>> _building_info_dict = new Dictionary<DBHeader, Dictionary<int, Dictionary<int, int[]>>>();
    bool[] _normal = {true,true,true,true,true,true};
    bool[] _build = {true,true,true};

    public int[] _combine_level_slot = {9,12};

    public int _max_stage_in_DB = 2;

    public string[] temp_char_data = {"김형섭","페리","피피","냥사서","농줌마","검은스미스"};

    public void Init()
    {
        Debug.Log("data inited");
        LoadCSV(PATH_item_n,ref _item_DB_n);
        LoadCSV(PATH_item_b,ref _item_DB_b);
        LoadJson<UserData>(PATH_user,ref _user_DB);
        LoadCSV(PATH_book,ref _book_DB);
        LoadCSV(PATH_Map,ref _map_DB);
        LoadCSV(PATH_Combine,ref _combine_DB);
        LoadCSV(PATH_Quest,ref _quest_DB);
        LoadCSV(PATH_interaction_dialogue,ref _interact_dialogue_DB);
        LoadCSV(PATH_building,ref _building_DB);
        _empty_slot = Resources.Load<Sprite>("Images/empty_slot");
        _empty_image = Resources.Load<Sprite>("Images/empty_image");
        LoadIcon(PATH_icon_item_b,2,ref _icon_item_b);
        LoadIcon(PATH_icon_item_n,2,ref _icon_item_n);

        LoadCombine(ref _combine_dict);
        LoadBuildLevelInfo(ref _building_info_dict);
        LoadCSV(PATH_progress_dialogue+_user_DB.stage,ref _progress_dialogue_DB);
        LoadCSV(PATH_main_progress+_user_DB.stage, ref _main_progress_DB);

        LoadStage(_user_DB.stage);
    }
    public ref CSVDict ItemDB(int type)
    {
        switch(type)
        {
            case 0:
                return ref _item_DB_n;
            case 1:
                return ref _item_DB_b;
            default:
                return ref _item_DB_n;
        }

    }
    public ref ItemInfoDict UserItem(int type)
    {
        if(type == 0)
            return ref _user_DB.item_normal;
        else
            return ref _user_DB.item_build;
    }

    public ref Dictionary<int,Sprite[]> IconDB(int type)
    {
        if(type == 0)
            return ref _icon_item_n;
        else
            return ref _icon_item_b;
    }

    public void LoadIcon(string path,int page, ref Dictionary<int,Sprite[]> dict)
    {
        for(var i = 0; i<page; i++)
        {
            Sprite[] array = Resources.LoadAll<Sprite>(path+i);
            dict.Add(i,array);
        }
    }


    public void  LoadCombine(ref Dictionary<int,Dictionary<string,int?[]>> dict)
    {//                         
        ref CSVDict _comb = ref _combine_DB;
        int[] levels = _comb.I(DBHeader.type);
        string[] codes = _comb.S(DBHeader.explain);

        for(int l = 0; l< _combine_level_slot.Length; l++)
            dict.Add(l,new Dictionary<string,int?[]>());

        for(int i = 0; i < levels.Length; i++)
        {
            ref string s = ref _comb.S(DBHeader.explain,i);
            int level = levels[i];
            int length = _combine_level_slot[level];
            int?[] temp = new int?[length];
            for(int j = 1; j<length+1; j++)
            {
                if(s[j]==' ')
                    temp[j-1] = null;
                else
                    temp[j-1] = (int)s[j]-'0';

            }
            _combine_dict[level].Add(_comb.S(DBHeader.destroy,i),temp);
        }



    }
    public void LoadBuildLevelInfo(ref Dictionary<DBHeader,Dictionary<int,Dictionary<int,int[]>>> dict)
    {
        dict.Add(DBHeader.duration,new Dictionary<int, Dictionary<int, int[]>>());
        dict.Add(DBHeader.evolution, new Dictionary<int, Dictionary<int, int[]>>());
        dict.Add(DBHeader.destroy, new Dictionary<int, Dictionary<int, int[]>>());
        dict.Add(DBHeader.num, new Dictionary<int, Dictionary<int, int[]>>());

        string[] duration = _item_DB_b.S(DBHeader.duration);

        foreach(KeyValuePair<DBHeader,Dictionary<int,Dictionary<int,int[]>>> page in dict)
        {
            string[] strarr = _item_DB_b.S(page.Key);
            for(int i = 0; i< strarr.Length; i++)
            {
                string[] sub_arr = strarr[i].Split('/');
                Dictionary<int,int[]> sub_dict = new Dictionary<int, int[]>();
                for(int j = 0; j<sub_arr.Length; j++)
                {
                    sub_dict.Add(j, sub_arr[j].Split(',').Select(Int32.Parse).ToArray());
                }
                dict[page.Key].Add(i,sub_dict);
            }
        }

    }

    public void LoadStage(int input_stage)
    {
        LoadStageIntDialogue(input_stage);
        LoadStageProDialogue(input_stage);
        LoadStageMainProgress(input_stage);
        LoadStageQuest(input_stage);
        
    }

    public void LoadStageProDialogue(int input_stage)
    {
        _progress_dialogue = new Dictionary<int, DialogueInfo>();

        int[] order =_progress_dialogue_DB.I(DBHeader.order);
        int[] character = _progress_dialogue_DB.I(DBHeader.character);
        int[] stage = _progress_dialogue_DB.I(DBHeader.stage);
        string[] dialogue = _progress_dialogue_DB.S(DBHeader.dialogue);

        int length = stage.Length;

        for(int i = 0; i< length; i++)
        {
            if(stage[i] == input_stage)
                _progress_dialogue.Add(order[i], new DialogueInfo(character[i],dialogue[i]));
            if(stage[i] > input_stage )
                break;
        }

    }
    public void LoadStageIntDialogue(int input_stage)
    {
        _interact_dialogue = new Dictionary<int, List<string>>();

        if(_interact_dialogue.Count < _user_DB.character_stage.Length)
            for(int i = _interact_dialogue.Count; i<_user_DB.character_stage.Length; i++)
            {
                _interact_dialogue.Add(i,new List<string>());
            }

        int[] stage = _interact_dialogue_DB.I(DBHeader.stage);
        int[] character = _interact_dialogue_DB.I(DBHeader.character);
        string[] dialogue = _interact_dialogue_DB.S(DBHeader.dialogue);
        int length = stage.Length;
        int[] user_character_stage = _user_DB.character_stage;

        for(int i = 0; i<length; i++)
        {
            if(stage[i] == user_character_stage[character[i]])
                _interact_dialogue[character[i]].Add(dialogue[i]);
        }
    }
    public void LoadStageMainProgress(int input_stage)
    {
        _stage_dict = new Dictionary<DBHeader, Dictionary<int, int>>();

        int[] stage = _main_progress_DB.I(DBHeader.stage);
        int[] step = _main_progress_DB.I(DBHeader.step);
        int[] type = _main_progress_DB.I(DBHeader.type);
        int[] order = _main_progress_DB.I(DBHeader.order);
        int length = stage.Length;

        if(_stage_dict.Count == 0)
        {
            _stage_dict.Add(DBHeader.type, new Dictionary<int, int>());
            _stage_dict.Add(DBHeader.order, new Dictionary<int, int>());
        }
        for(int i= 0; i<length; i++)
        {
            if(stage[i]== input_stage)
            {
                _stage_dict[DBHeader.type].Add(step[i],type[i]);
                _stage_dict[DBHeader.order].Add(step[i],order[i]);
                if(stage[i] == 1)
                Debug.Log("succes added");
            }
            if(stage[i]>input_stage)
                break;
        }

    }
    public void LoadStageQuest(int input_stage)
    {
        int[] stage = _quest_DB.I(DBHeader.stage);
        int length = stage.Length;
        string[] name = _quest_DB.S(DBHeader.name);
        int[] type = _quest_DB.I(DBHeader.type);
        int[] index = _quest_DB.I(DBHeader.index);
        int[] character = _quest_DB.I(DBHeader.character);
        int[] var1 = _quest_DB.I(DBHeader.var1);
        int[] var2 = _quest_DB.I(DBHeader.var2);
        int[] num = _quest_DB.I(DBHeader.num);
        QuestInfoDict dict = Managers.data._user_DB.quest;

        for(int i = 0; i< length; i++)
        {
            if(stage[i]==input_stage && !dict.ContainsKey(index[i]))
            {
                ItemTypeInfo i_info = new ItemTypeInfo(var1[i],var2[i],num[i]);
                QuestInfo q_info = new QuestInfo(name[i],type[i],character[i],i_info,true);
                dict.Add(index[i],q_info);
            }
        }

        Debug.Log("끼릭끼릭");

    }

    public void UpdateItem(Inventory inven,int type, int index, int count)
    {
        inven.UpdateInventory(type,index,count);
    }

    

    public void UpdateData<T>(T json_object, string name)
    {
        string sPath = string.Format(ConstInfo.DATA_PATH+"{0}.json",name);
        Debug.Log(sPath);
        FileInfo file = new FileInfo(sPath);
        string jsonstring = JsonUtility.ToJson(json_object);
        //file.Directory.Create();
        File.WriteAllText(file.FullName, jsonstring);
        Debug.Log("saved"+jsonstring);
    }

    public void LoadJson<T>(string path, ref T obj)
    {
        TextAsset text = Resources.Load<TextAsset>(path);
        obj = JsonUtility.FromJson<T>(text.text);
        
    }

    public void ReLoadCSV(string file, ref CSVDict ret)
    {
        ret = new CSVDict();
        LoadCSV(file,ref ret);
    }
    public CSVDict LoadCSV(string file)
    {
        CSVDict ret = new CSVDict();
        LoadCSV(file,ref ret);
        return ret;
    }
    public void LoadCSV(string file, ref CSVDict ret)
    {
        Dictionary<DBHeader,Array> dict = new Dictionary<DBHeader, Array>();
        TextAsset data = Resources.Load(file) as TextAsset;
 
        var lines = Regex.Split (data.text, LINE_SPLIT_RE);
 
        if(lines.Length <= 2) return;
 
        var types = Regex.Split(lines[0], SPLIT_RE);
        var header = Regex.Split(lines[1],SPLIT_RE);
        int[] text = new int[5];

        DBHeader[] key_index = new DBHeader[types.Length];
        Type[] type_index = new Type[types.Length];

        for(int i = 0; i< types.Length; i++)
        {
            Type type = Type.GetType("System."+types[i]);
            type_index[i] = type;
            DBHeader header_name = (DBHeader)Enum.Parse(typeof(DBHeader),header[i]);
            key_index[i] = header_name;
           //Type[] column = new typeof(Type)[lines.Length-3];
            Array column = Array.CreateInstance(type,lines.Length-3);
            dict.Add(header_name,column);
            if(type_index[i] == typeof(Int32))
            {
                ret.key_dict.Add(key_index[i],i*3);
                int[] arr = new int[lines.Length-3];
                ret.int_dict.Add(i*3,arr);
            }
            else if(type_index[i] == typeof(Single))
            {
                ret.key_dict.Add(key_index[i],i*3+1);
                float[] arr = new float[lines.Length-3];
                ret.float_dict.Add(i*3+1,arr);
            }
            else if(type_index[i] == typeof(String))
            {
                ret.key_dict.Add(key_index[i],i*3+2);
                string[] arr = new string[lines.Length-3];
                ret.string_dict.Add(i*3+2,arr);
            }

        }

        for(var l = 2; l < lines.Length; l++)
        {
 
            var values = Regex.Split(lines[l], SPLIT_RE);
            if(values.Length == 0 ||values[0] == "") continue;
 
            //var entry = new Dictionary<string, object>();
            for(var f=0; f < header.Length && f < values.Length; f++ ) 
            {
                string value = values[f];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                //change to array
                if(type_index[f] == typeof(Int32))
                {
                    int n = Int32.Parse(value);
                    //dic[key_index[f]].SetValue(n,l-2);
                    ret.I(key_index[f],l-2) = n;
                }
                else if(type_index[f] == typeof(Single))
                {
                    float fl = Single.Parse(value);
                    ret.F(key_index[f],l-2) = fl;
                    // dic[key_index[f]].SetValue(fl,l-2);
                }
                else if (type_index[f] == typeof(String))
                {
                    ret.S(key_index[f],l-2) = value;
                    // dic[key_index[f]].SetValue(value,l-2);
                }
                else
                    ret.S(key_index[f],l-2) = "Error";
                    // dic[key_index[f]].SetValue("Error",l-2);
   
            }
        }
    }
    
}
