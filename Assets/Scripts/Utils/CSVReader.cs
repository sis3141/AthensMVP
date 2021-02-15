using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using Define;

public class Old_CSVReader : MonoBehaviour
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };
 
    public static Dictionary<DBHeader,Array> Read(string file)
    {
        //var list = new List<Dictionary<string, object>>();
        Dictionary<DBHeader,Array> dict = new Dictionary<DBHeader, Array>();
        TextAsset data = Resources.Load (file) as TextAsset;
 
        var lines = Regex.Split (data.text, LINE_SPLIT_RE);
 
        if(lines.Length <= 2) return dict;
 
        var types = Regex.Split(lines[0], SPLIT_RE);
        var header = Regex.Split(lines[1],SPLIT_RE);

        DBHeader[] key_index = new DBHeader[types.Length];
        Type[] type_index = new Type[types.Length];

        for(int i = 0; i< types.Length; i++)
        {
            Type type = Type.GetType("System."+types[i]);
            type_index[i] = type;
            DBHeader header_name = (DBHeader)Enum.Parse(typeof(DBHeader),header[i]);
            key_index[i] = header_name;
            Array column = Array.CreateInstance(type,lines.Length-3);
            dict.Add(header_name,column);
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
                    dict[key_index[f]].SetValue(n,l-2);
                }
                else if(type_index[f] == typeof(Single))
                {
                    float fl = Single.Parse(value);
                    dict[key_index[f]].SetValue(fl,l-2);
                }
                else if (type_index[f] == typeof(String))
                {
                    dict[key_index[f]].SetValue(value,l-2);
                }
                else
                    dict[key_index[f]].SetValue("Error",l-2);
   
            }
        }
        return dict;
    }
}
