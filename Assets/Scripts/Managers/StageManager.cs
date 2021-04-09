using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
using DataStructure;
using System;
public class StageManager
{
    // Start is called before the first frame update
    // type 별로 switch로 컨트롤
    // 완료시 알림받고 진행중 스텝 클리어, 다음스텝
    bool _loaded = false;
    int _stage;
    int _step;

    int _step_length;

    public int _order;
    
    QuestInfoDict _quest_dict;
    Overlay _dialogue_ui;
    Inventory _inven;

    Dictionary<int,Action<ItemTypeInfo>> _event_dict = new Dictionary<int, Action<ItemTypeInfo>>();
    delegate void Del(ItemTypeInfo info); 
    public void Init()
    {
        if(_loaded)
            return;

        _loaded = true;
        _stage = Managers.data._user_DB.stage;
        _step = Managers.data._user_DB.stage_step;
        _step_length = Managers.data._stage_dict[DBHeader.type].Count;
        _quest_dict = Managers.data._user_DB.quest;

        _inven = Managers.ui._common_UI_dict[CommonUI.Inventory].GetComponent<Inventory>();
        _dialogue_ui = Managers.ui._common_UI_dict[Define.CommonUI.Overlay].GetComponent<Overlay>();
        LoadStep();
    }

    public void LoadStep()
    {
        int type = Managers.data._stage_dict[Define.DBHeader.type][_step];
        _order = Managers.data._stage_dict[Define.DBHeader.order][_step];
        switch(type)
        {
            case 0:
                DialogueInfo dialogue_info = Managers.data._progress_dialogue[_order];
                string[] text = dialogue_info.dialogue.Split('/');
                int character = dialogue_info.character;
                _dialogue_ui.OpenDialogue(text,character,true);
                break;
            case 1:
                Managers.scene._step_mission = true;
                break;
            case 2:
                //quest
                QuestInfo q_info = _quest_dict[_order];
                ItemTypeInfo i_info = q_info.info;
                switch(q_info.type)
                {
                    case 0:
                        Debug.Log("아이템 잘 받았습니다");
                        Managers.data.UpdateItem(_inven,i_info.type,i_info.index, i_info.count);
                        q_info.state = 3;
                        StepClear();
                        break;
                    case 1:
                        Debug.Log("퀘스트 추가요");
                        AddQuest(_order);
                        Action<ItemTypeInfo> temp = (ItemTypeInfo info) => { if(info.type == i_info.type && info.index == i_info.index)
                                                                            CheckQuest(info);
                                                                        };
                        _event_dict.Add(_order,temp);
                        Managers.ItemInvoker += temp;
                        // Managers.ItemInvoker += (ItemTypeInfo info) => { if(info.type == i_info.type && info.index == i_info.index)
                        //                                                     CheckQuest(info);
                        //                                                 }; //타겟 아이템과 일치하면 체크
                        break;

                }
                break;

        }

    }

    public void StepClear()
    {
        Debug.Log("step cleared!");
        DataManager data = Managers.data;
        if(_step+1 == _step_length)
        {
            // next stage?
            if(_stage+1% data._max_stage_in_DB == 0)
            {
                int max_stage = data._max_stage_in_DB;
                _stage += max_stage;
                ////
                if(_stage == 3)
                    return;
                data.ReLoadCSV(data.PATH_main_progress+(_stage/max_stage),ref data._main_progress_DB);
                data.ReLoadCSV(data.PATH_progress_dialogue+(_stage/max_stage),ref data._progress_dialogue_DB);
                data.LoadStage(_stage);
                Debug.Log("new db loadad");
                data._user_DB.stage = _stage;
                _step = 0;
                data._user_DB.stage_step = _step;
            }
            else
            {
                //다음 스테이지
                _stage++;
                _step = 0;
                data.LoadStage(_stage);
                data._user_DB.stage = _stage;
                Debug.Log("new stage loaded");
            }
            
        }
        else
        {
            _step++;
            data._user_DB.stage_step = _step;
        }
        LoadStep();

    }

    public void AddQuest(int index)
    {
        _quest_dict[index].state = 1;
        Debug.Log("quest added");

    }

    public void CheckQuest(ItemTypeInfo info)
    {
        Debug.Log("Checking quest");
        foreach(KeyValuePair<int, QuestInfo> pair in _quest_dict.dict)
        {
            Debug.Log("quest :"+pair.Key);
            ItemTypeInfo i_info = pair.Value.info;
            Debug.Log("quest item info :"+ i_info.type + "," + i_info.index + ", need" + i_info.count);
            Debug.Log("added item info : "+info.type + ","+info.index +", have" + info.count);
            if(i_info.type == info.type && i_info.index == info.index) // 변동된 아이템 정보가 일치하는지
            {
                switch(pair.Value.state)
                {
                    case 1:
                        Debug.Log("case 1");
                        if(i_info.count <= info.count)
                        {
                            Debug.Log("일단 클리어한걸로 치자");
                            pair.Value.state = 2;
                            ClearQuest(pair.Key);
                        }
                        break;
                    case 2:
                        Debug.Log("case 2");
                        if(i_info.count > info.count)
                            pair.Value.state = 1;
                        break;
                }
            }

        }
    }

    public void ClearQuest(int index)
    {
        QuestInfo info = _quest_dict[index];
        if(info.state == 2)
        {
            info.state = 3;
            Debug.Log("Questcleared");
            Managers.ItemInvoker -= _event_dict[index];
            _event_dict.Remove(index);
            Debug.Log("dict count : "+_event_dict.Count);
            if(Managers.ItemInvoker == null)
                Debug.Log("invoker cleared");
            if(info.duty)
                StepClear();
        }
    }

    // Update is called once per frame
    
}
