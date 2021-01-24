using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStructure;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    public int row;
    [SerializeField]
    public int column;
    void Start()
    {
        GenerateMap(row,column);
    }
    public static void GenerateMap(int row, int column)
    {
        MapData map = new MapData();
        map.mapinfo = new MapData.Row[row];
        for(int i = 0; i < row; i++)
        {
            map.mapinfo[i].x = new int[column];
        }
        string testlog = JsonUtility.ToJson(map); 
        DataManager.SaveJsonData(testlog);
    }
}