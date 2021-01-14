using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        Debug.Log("temp scene loaded!");
        //Managers.resource.Instantiate<GameObject>("Prefabs/unitychan");
        for(int i = 0; i<4; i++)
        {
            Canvas temp = Managers.ui.OpenNewUI($"UI/TestCan{i}",UIManager.UIType.Popup);
            string name = temp.ToString();
            int sort_order = temp.sortingOrder;
            Utils.GetOrAddComponent<Button>(temp);
            Debug.Log($"sort order of {name} is {sort_order}");
            
        }
        Toggle3DRaycast(true);
        

        
    }

    public void Timer()
    {
        GameObject timer = new GameObject();
        Destroy(timer, 0.5f);
    }



    
    public override void Clear()
    {

    }
}
